using System.Collections.Generic;
using System.Linq;
using CqmSolution.Models;
using CqmSolution.Models.Constants;

namespace CqmSolution.CcdaExtensions.R2
{
    public static class AllergyAdverseEventCcdaExtensions
    {
        public static List<AllergyAdverseEvent> GetAllergyAdverseEvents(this POCD_MT000040ClinicalDocument cda, Client client)
        {
            var structuredBody = cda?.component?.Item as POCD_MT000040StructuredBody;

            return structuredBody?.component?.FirstOrDefault(c => c.section?.code?.code == LoincConstants.ALLERGIES)
                ?.section?.GetAllergyAdverseEventsFromComponentSection(client);
        }

        public static List<AllergyAdverseEvent> GetAllergyAdverseEventsFromComponentSection(this POCD_MT000040Section componentSection, Client client)
        {
            var allergyAdverseEvents = new List<AllergyAdverseEvent>();

            var actEntries = componentSection?.entry?.Where(e => e.Item is POCD_MT000040Act);

            if (actEntries != null)
            {
                foreach (var actEntry in actEntries)
                {
                    allergyAdverseEvents.Add(GetAllergyAdverseEventFromAct(actEntry.Item as POCD_MT000040Act, client));
                }
            }

            return allergyAdverseEvents;
        }

        public static AllergyAdverseEvent GetAllergyAdverseEventFromAct(this POCD_MT000040Act act, Client client)
        {
            var allergyAdverseEvent = new AllergyAdverseEvent(client)
            {
                Cause = act?.participant?.FirstOrDefault(p => p.typeCode=="CSM" &&  p.participantRole?.Item is POCD_MT000040PlayingEntity)
                    ?.participantRole?.code?.GetCode(), //TODO: handle variable path? (see XML extension code)
                DateRange = act?.effectiveTime?.GetDateRange()
            };

            //TODO: figure out allergy type and negation ind parsing
            //var observationValues = act.SelectNodes(@".//entryrelationship/observation[code[@code='ASSERTION']]/value");

            //foreach (var observationValue in observationValues)
            //{
            //    allergyAdverseEvent.AllergyType = observationValue.GetCode();

            //    if (!string.IsNullOrWhiteSpace(allergyAdverseEvent.AllergyType?.Value))
            //    {
            //        var negationInd = observationValue.ParentNode?.Attributes["negationind"]?.Value;

            //        if (bool.TryParse(negationInd, out var notPresent))
            //        {
            //            allergyAdverseEvent.NotPresent = notPresent;
            //        }

            //        break;
            //    }
            //}

            return allergyAdverseEvent;
        }
    }
}

