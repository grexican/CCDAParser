using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using CqmSolution.Models;

namespace CqmSolution.XmlExtensions
{
    public static class EncounterXmlExtensions
    {
        public static List<Encounter> GetEncounters(this HtmlDocument cda, Client client)
        {
            return cda.DocumentNode.SelectSingleNode(
                $"/clinicaldocument/component/structuredbody/component/section[code[@code='{LoincConstants.ENCOUNTERS}']]")
                .GetEncountersFromComponentSection(client);
        }

        public static List<Encounter> GetEncountersFromComponentSection(this HtmlNode componentSection, Client client)
        {
            var encounters = new List<Encounter>();

            var encounterEntries = componentSection.SelectNodes(@"entry/encounter[@classcode='ENC']");

            foreach (var encounterEntry in encounterEntries)
            {
                encounters.Add(GetEncounterFromEncounterEntry(encounterEntry, client));
            }

            return encounters;
        }

        public static Encounter GetEncounterFromEncounterEntry(this HtmlNode encounterEntry, Client client)
        {
            var moodCode = encounterEntry.Attributes["moodcode"]?.Value;
            var performed = !string.IsNullOrWhiteSpace(moodCode) &&
                             moodCode.Equals("EVN", StringComparison.InvariantCultureIgnoreCase);

            var encounter = new Encounter(client, performed ? "PRF" : "ORD")
            {
                Code = encounterEntry.SelectSingleNode(@"code").GetCode(),
                VisitDateRange = encounterEntry.GetDateRange(),
                //
                //TODO: I can't find any examples that have an effectiveTime associated with the Service Delivery Location.
                // Officially, that template does not contain a date, or contain any other templates that might contain a date.
                // There is usually an effectiveTime above it in the data file, at the same level as the participant, but
                // that is where we are getting the VisitDateRange from.  If the FacilityLocation DateRange is the same as
                // the top-level DateRange for this Encounter, what's the point of having it at the FacilityLocation level?
                FacilityLocation = encounterEntry.SelectSingleNode(@"participant[@typecode='LOC']/participantrole[@classcode='SDLOC']/code").GetCodeWithDateRange(),
                //
                DischargeDisposition = encounterEntry.SelectSingleNode(@"./*[name()='sdtc:dischargedispositioncode']").GetCode(),
                PrincipalDiagnosis = encounterEntry.SelectSingleNode($"entryrelationship/act[@classcode='ACT' and code[@code='{LoincConstants.HOSPITAL_DISCHARGE_DIAGNOSIS}']]/entryrelationship/observation/value").GetCode(),
                EncounterDiagnosis = encounterEntry.SelectSingleNode($"entryrelationship/act[@classcode='ACT' and code[@code='{LoincConstants.ENCOUNTER_DIAGNOSIS}']]/entryrelationship/observation/value").GetCodeWithDateRange()
            };

            //TODO: distinguish between Principal Diagnosis and Encounter Diagnosis using priorityCode?  Does this only apply to HospitalDischargeDiagnosis?
            /*
					<!-- This denotes that the diagnosis is the principal diagnosis. There is generally only a single diagnosis for coded bill.-->
					<!-- This current modeling aligns with QRDA. Substantial discussion occurred on the task force regarding this method. --> 
					<!-- Additional comments on this approach should be included in comments on the QRDA Implementation Guide. We will revisit if QRDA changes their standard approach.  --> 
					<priorityCode code="63161005" codeSystem="2.16.840.1.113883.6.96" codeSystemName="SNOMED CT" displayName="Principal"/>

					<!-- This denotes that the diagnosis is a secondary diagnosis. There may be multiple secondary diagnoses on coded bills.-->
					<!-- This current modeling aligns with QRDA. Substantial discussion occurred on the task force regarding this method. --> 
					<!-- Additional comments on this approach should be included in comments on the QRDA Implementation Guide. We will revisit if QRDA changes their standard approach.  --> 
					<priorityCode code="2603003" codeSystem="2.16.840.1.113883.6.96" codeSystemName="SNOMED CT" displayName="Secondary"/>

                    https://github.com/HL7/C-CDA-Examples/blob/master/Encounters/Hospital%20Discharge%20Encounter%20with%20Billable%20Diagnoses/Hospital%20Discharge%20Encounter%20with%20Billable%20Diagnoses(C-CDA2.1).xml
             */

            return encounter;
        }
    }
}
