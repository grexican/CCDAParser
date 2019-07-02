using System.Collections.Generic;
using System.Linq;
using CqmSolution.Models;
using CqmSolution.Models.Constants;

namespace CqmSolution.CcdaExtensions.R2
{
    public static class DiagnosisProblemCcdaExtensions
    {
        public static List<DiagnosisProblem> GetDiagnosisProblems(this POCD_MT000040ClinicalDocument cda, Client client)
        {
            var structuredBody = cda?.component?.Item as POCD_MT000040StructuredBody;

            return structuredBody?.component?.FirstOrDefault(c => c.section?.code?.code == LoincConstants.PROBLEMLIST)
                ?.section?.GetDiagnosisProblemsFromComponentSection(client);
        }

        public static List<DiagnosisProblem> GetDiagnosisProblemsFromComponentSection(this POCD_MT000040Section componentSection, Client client)
        {
            var diagnosisProblems = new List<DiagnosisProblem>();

            var actEntries = componentSection?.entry?.Where(e => e.Item is POCD_MT000040Act);

            if (actEntries != null)
            {
                foreach (var actEntry in actEntries)
                {
                    diagnosisProblems.Add(GetDiagnosisProblemFromDiagnosisProblemAct(actEntry.Item as POCD_MT000040Act, client));
                }
            }

            return diagnosisProblems;
        }

        public static DiagnosisProblem GetDiagnosisProblemFromDiagnosisProblemAct(this POCD_MT000040Act act, Client client)
        {
            //TODO: Can there be many observations under an act?  The schema definition says there can, but I can't find any examples?
            var observation = act?.entryRelationship?.FirstOrDefault(r =>
                    r.Item is POCD_MT000040Observation)
                ?.Item as POCD_MT000040Observation;

            var diagnosisProblem = new DiagnosisProblem(client)
            {
                //TODO: ProblemResult = observation?.value?.GetResultValue(),
                DateRange = observation?.effectiveTime?.GetDateRange(), //use the date on the observation (which is the date of biological onset),
                                                                        //NOT the date on the act (which is the date the concern was authored in the patient's chart)
                                                                        //TODO: but does that ^ make sense if there are multiple observations under the same concern act??
                Status = act?.statusCode?.GetCode(),                    //use the status code on the act (which is the overall concern status),
                                                                        //NOT the statusCode on the observation
                //TODO: TargetSite = diagnosisProblemAct.SelectSingleNode(".//targetsitecode/qualifier/value").GetCode(),
                //TODO: Severity = diagnosisProblemAct
                //    .SelectSingleNode(
                //        @".//entryrelationship[@typecode='SUBJ' and @inversionind='true']/observation[@classcode='OBS' and code[@code='SEV']]/value")
                //    .GetCode()
                NotPresent = observation != null && observation.negationInd
            };

            return diagnosisProblem;
        }
    }
}
