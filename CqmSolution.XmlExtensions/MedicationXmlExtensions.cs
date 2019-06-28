using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using CqmSolution.Models;

namespace CqmSolution.XmlExtensions
{
    public static class MedicationXmlExtensions
    {
        public static List<Medication> GetMedications(this HtmlDocument cda, Client client)
        {
            var allergyAdverseEvents = cda.GetAllergyAdverseEvents(client);

            var encounters = cda.GetEncounters(client);

            var dischargeDateRange = (from encounter in encounters
                where !string.IsNullOrWhiteSpace(encounter.DischargeDisposition?.Value)
                select encounter.VisitDateRange).FirstOrDefault();

            return cda.DocumentNode.SelectSingleNode(
                $"/clinicaldocument/component/structuredbody/component/section[code[@code='{LoincConstants.MEDICATIONS}']]")
                .GetMedicationsFromComponentSection(client, allergyAdverseEvents, dischargeDateRange);
        }

        public static List<Medication> GetMedicationsFromComponentSection(this HtmlNode componentSection, Client client,
            List<AllergyAdverseEvent> allergyAdverseEvents, CqmSolutionDateRange dischargeDateRange)
        {
            var medications = new List<Medication>();

            var medicationEntries = componentSection.SelectNodes(@"entry/substanceadministration[@classcode='SBADM']");

            foreach (var medicationEntry in medicationEntries)
            {
                medications.Add(GetMedicationFromMedicationEntry(medicationEntry, client, allergyAdverseEvents, dischargeDateRange));
            }

            return medications;
        }

        public static Medication GetMedicationFromMedicationEntry(this HtmlNode medicationEntry, Client client,
            List<AllergyAdverseEvent> allergyAdverseEvents, CqmSolutionDateRange dischargeDateRange)
        {
            var productNode = medicationEntry.SelectSingleNode(@"consumable/manufacturedproduct[@classcode='MANU']/manufacturedmaterial/code");
            var productCode = productNode?.GetCode();
            var dataSubType = GetDataSubType(medicationEntry, productCode, allergyAdverseEvents, dischargeDateRange, out var notPresent);

            var medication = new Medication(client, dataSubType)
            {
                Product = productCode,
                Generic = productNode?.SelectSingleNode("translation")?.GetCode(), //TODO: handle multiple translation nodes?
                AdministeredDateRange = medicationEntry.GetDateRange(),
                Refills = medicationEntry.SelectSingleNode(@"entryrelationship/supply/repeatnumber")
                    ?.Attributes["value"]?.Value,
                NotPresent = notPresent,
                NegationRationale = notPresent ? medicationEntry.SelectSingleNode(@"entryrelationship/observation/value")?.GetCode() : null //TODO: is this correct?
            };

            return medication;
        }

        private static string GetDataSubType(HtmlNode medicationEntry, Code productCode,
            List<AllergyAdverseEvent> allergyAdverseEvents, CqmSolutionDateRange dischargeDateRange, out bool notPresent)
        {
            notPresent = false;

            if (medicationEntry == null)
            {
                return string.Empty;
            }

            var negationInd = medicationEntry.Attributes["negationind"]?.Value;
            bool.TryParse(negationInd, out notPresent);

            //TODO: is this correct? if so, should we compare the whole code, not just the value?
            if (!string.IsNullOrWhiteSpace(productCode?.Value))
            {
                //if patient has an allergy or adverse event for the same product, return the appropriate dataSubType
                var allergyAdverseEvent =
                    allergyAdverseEvents.FirstOrDefault(a => !a.NotPresent && a.Cause?.Value == productCode.Value);

                if (allergyAdverseEvent != null)
                {
                    return allergyAdverseEvent.DataSubType;
                }
            }

            //TODO: Are these the correct logic and codes for Discharge/Discharge Not Done?
            if (dischargeDateRange?.DateHigh?.DateTime.HasValue == true)
            {
                return "DSC";
            }
            //
            if (dischargeDateRange?.DateLow?.DateTime.HasValue == true)
            {
                return "DSCND";
            }
            //

            var moodCode = medicationEntry.Attributes["moodcode"]?.Value;

            var statusName = GetStatus(medicationEntry);

            switch (moodCode)
            {
                case "EVN": //administered
                    return  statusName.Equals("completed", StringComparison.InvariantCultureIgnoreCase) ? (notPresent? "ADMND" : "ADM") : "ACT";
                                                                                    //TODO: is this logic correct, and are these ^-------^
                                                                                    //the right codes for Administered and Administered Not Done?
                case "INT": //to be administered
                    return statusName.Equals("active", StringComparison.InvariantCultureIgnoreCase) ? "ACT" : "DISP";
                                                                        //TODO: is this logic correct, and is this --^
                                                                        //the right code for Dispensed?
                                                                        //Also, shouldn't there be a subtype for "No Longer Active?"
                case "RQO": //ordered
                    return notPresent ? "ORDND" : "ORD";

                //TODO: Handle notPresent in all cases?

                default:
                    return string.Empty; //TODO: default to what?
            }
        }

        private static string GetStatus(HtmlNode medicationEntry)
        {
            //first, look for an explicit status observation
            var statusCode = medicationEntry?.SelectSingleNode(
                $"entryrelationship/observation[code[@code='{LoincConstants.STATUS_OBSERVATION}']]/value")
                ?.GetCode();

            //TODO: is this logic correct?  
            /*
                http://motorcycleguy.blogspot.com/2011/03/medication-status-in-ccd.html
                One of the things that people often want to know about medications is whether they are still actively be used by the patient, or whether they are historical.
                Some look to find this in the status element of the substanceAdministation or supply act, but that is used for a different purpose.  It is used to record
                the state of the act according to the state model defined in the HL7 RIM.  For example, when dealing with a request or order for medications, the state is
                completed when the order is filled.  When dealing with an event, such as the administration of a medication, the state is completed when the med has been
                given to the patient.  That means that what that statusCode element implies about the medication depends upon the type of act (specifically its mood).
                
                -----> The problem is, most sample CCDs do NOT have a status observation under Medication! - NJS

                https://wiki.ihe.net/index.php/1.3.6.1.4.1.19376.1.5.3.1.4.7#Medication_Fields
                <statusCode code='completed'/>
                The status of all <substanceAdministration> elements must be "completed". The act has either occurred, or the request or order has been placed.
            */
            if (string.IsNullOrWhiteSpace(statusCode?.Value))
            {
                //if not found, look for supply order status
                statusCode = medicationEntry?.SelectSingleNode("entryrelationship/supply[templateid[@root='{TemplateOidConstants.MEDICATION_SUPPLY_ORDER}']]/statuscode")?.GetCode();

                if (string.IsNullOrWhiteSpace(statusCode?.Value))
                {
                    //if still not found, use substance administration status
                    statusCode = medicationEntry?.SelectSingleNode("statuscode")?.GetCode();
                }
            }

            // All of our DataSubType logic keys off of descriptive status names,
            // per the CQMSolution spec, so use DisplayName if possible.
            var statusName = statusCode?.DisplayName;

            if (string.IsNullOrWhiteSpace(statusName))
            {
                statusName = statusCode?.Value;
            }

            return statusName ?? string.Empty;
        }
    }
}
