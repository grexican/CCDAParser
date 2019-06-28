using System.Collections.Generic;
using HtmlAgilityPack;
using CqmSolution.Models;

namespace CqmSolution.XmlExtensions
{
    public static class AllergyAdverseEventXmlExtensions
    {
        public static List<AllergyAdverseEvent> GetAllergyAdverseEvents(this HtmlDocument cda, Client client)
        {
            return cda.DocumentNode.SelectSingleNode(
                $"/clinicaldocument/component/structuredbody/component/section[code[@code='{LoincConstants.ALLERGIES}']]")
                .GetAllergyAdverseEventsFromComponentSection(client);
        }

        public static List<AllergyAdverseEvent> GetAllergyAdverseEventsFromComponentSection(this HtmlNode componentSection, Client client)
        {
            var allergyAdverseEvents = new List<AllergyAdverseEvent>();

            var acts = componentSection.SelectNodes(@"entry/act[@classcode='ACT']");

            foreach (var act in acts)
            {
                allergyAdverseEvents.Add(GetAllergyAdverseEventFromAct(act, client));
            }

            return allergyAdverseEvents;
        }

        public static AllergyAdverseEvent GetAllergyAdverseEventFromAct(this HtmlNode act, Client client)
        {
            var allergyAdverseEvent = new AllergyAdverseEvent(client)
            {
                Cause = act.SelectSingleNode(@".//participant[@typecode='CSM']//code").GetCode(),
                DateRange = act.GetDateRange()
            };

            var observationValues = act.SelectNodes(@".//entryrelationship/observation[code[@code='ASSERTION']]/value");

            foreach (var observationValue in observationValues)
            {
                allergyAdverseEvent.AllergyType = observationValue.GetCode();

                if (!string.IsNullOrWhiteSpace(allergyAdverseEvent.AllergyType?.Value))
                {
                    var negationInd = observationValue.ParentNode?.Attributes["negationind"]?.Value;

                    if (bool.TryParse(negationInd, out var notPresent))
                    {
                        allergyAdverseEvent.NotPresent = notPresent;
                    }

                    break;
                }
            }

            return allergyAdverseEvent;
        }
    }
}
