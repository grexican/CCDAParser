using System.Collections.Generic;
using HtmlAgilityPack;
using CqmSolution.Models;

namespace CqmSolution.XmlExtensions
{
    public static class DiagnosisProblemXmlExtensions
    {
        public static List<DiagnosisProblem> GetDiagnosisProblems(this HtmlDocument cda, Client client)
        {
            return cda.DocumentNode.SelectSingleNode(
                $"/clinicaldocument/component/structuredbody/component/section[code[@code='{LoincConstants.PROBLEMLIST}']]")
                .GetDiagnosisProblemsFromComponentSection(client);
        }

        public static List<DiagnosisProblem> GetDiagnosisProblemsFromComponentSection(this HtmlNode componentSection, Client client)
        {
            var diagnosisProblems = new List<DiagnosisProblem>();

            var diagnosisProblemActs = componentSection.SelectNodes(@"entry/act[@classcode='ACT']");

            foreach (var diagnosisProblemAct in diagnosisProblemActs)
            {
                diagnosisProblems.Add(GetDiagnosisProblemFromDiagnosisProblemAct(diagnosisProblemAct, client));
            }

            return diagnosisProblems;
        }

        public static DiagnosisProblem GetDiagnosisProblemFromDiagnosisProblemAct(this HtmlNode diagnosisProblemAct, Client client)
        {
            //TODO: Can there be many observations under an act?  The schema definition says there can, but I can't find any examples?
            var diagnosisProblemObservation = diagnosisProblemAct.SelectSingleNode(@"entryrelationship[@typecode='SUBJ']/observation[@classcode='OBS']");

            var diagnosisProblem = new DiagnosisProblem(client)
            {
                ProblemResult = diagnosisProblemObservation?.SelectSingleNode("value").GetResultValue(),
                DateRange = diagnosisProblemObservation?.GetDateRange(), //use the date on the observation (which is the date of biological onset),
                                                                         //NOT the date on the act (which is the date the concern was authored in the patient's chart)
                                                                         //TODO: but does that ^ make sense if there are multiple observations under the same concern act??
                Status = diagnosisProblemAct.SelectSingleNode("statuscode").GetCode(),  //use the status code on the act (which is the overall concern status),
                                                                                        //NOT the statusCode on the observation
                TargetSite = diagnosisProblemAct.SelectSingleNode(".//targetsitecode/qualifier/value").GetCode(),
                Severity = diagnosisProblemAct
                    .SelectSingleNode(
                        @".//entryrelationship[@typecode='SUBJ' and @inversionind='true']/observation[@classcode='OBS' and code[@code='SEV']]/value")
                    .GetCode()
            };

            var negationInd = diagnosisProblemObservation?.Attributes["negationind"]?.Value;
            if (bool.TryParse(negationInd, out var notPresent))
            {
                diagnosisProblem.NotPresent = notPresent;
            }

            return diagnosisProblem;
        }
    }
}
