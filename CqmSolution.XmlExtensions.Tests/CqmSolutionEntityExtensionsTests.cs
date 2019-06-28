using CqmSolution.Models;
using Xunit;

namespace CqmSolution.XmlExtensions.Tests
{
    public class CqmSolutionEntityExtensionsTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetClients), MemberType = typeof(TestDataGenerator))]
        public void GetDynamicParametersReturnsCorrectPropertyValuesForClient(string testName, Client client)
        {
            var dynamicParameters = client.GetDynamicParameters();

            Assert.Equal(client.LastName, dynamicParameters["D001"]);
            Assert.Equal(client.FirstName, dynamicParameters["D002"]);
            Assert.Equal(client.MiddleName, dynamicParameters["D003"]);
            Assert.Equal(client.DateOfBirth?.Value, dynamicParameters["D004"]);
            Assert.Equal(client.Gender?.Value, dynamicParameters["D005"]);
            Assert.Equal(client.Gender?.DisplayName, dynamicParameters["D006"]);
            Assert.Equal(client.Address?.Type, dynamicParameters["D007"]);
            Assert.Equal(client.Address?.Street1, dynamicParameters["D008"]);
            Assert.Equal(client.Address?.Street2, dynamicParameters["D009"]);
            Assert.Equal(client.Address?.City, dynamicParameters["D010"]);
            Assert.Equal(client.Address?.State, dynamicParameters["D011"]);
            Assert.Equal(client.Address?.Zip, dynamicParameters["D012"]);
            Assert.Equal(client.Address?.Country, dynamicParameters["D013"]);
            Assert.Equal(client.Phone?.Type, dynamicParameters["D014"]);
            Assert.Equal(client.Phone?.Number, dynamicParameters["D015"]);
            Assert.Equal(client.Language?.Value, dynamicParameters["D016"]);
            Assert.Equal(client.Race?.Value, dynamicParameters["D017"]);
            Assert.Equal(client.Race?.DisplayName, dynamicParameters["D018"]);
            Assert.Equal(client.Ethnicity?.Value, dynamicParameters["D019"]);
            Assert.Equal(client.Ethnicity?.DisplayName, dynamicParameters["D020"]);
            Assert.Equal(client.Religion?.Value, dynamicParameters["D021"]);
            Assert.Equal(client.Religion?.DisplayName, dynamicParameters["D022"]);
            Assert.Equal(client.Language?.DisplayName, dynamicParameters["D023"]);
            Assert.Equal(client.AccountNumber, dynamicParameters["D024"]);
            Assert.Equal(client.PatientIdentifier, dynamicParameters["D035"]);
            Assert.Equal(client.PatientIdentifierRootId?.Value, dynamicParameters["D037"]);
            Assert.Equal(client.PatientIdentifierNumber, dynamicParameters["D040"]);
            Assert.Equal(client.PatientIdentifierNumberOid?.Value, dynamicParameters["D041"]);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetAllergyAdverseEvents), MemberType = typeof(TestDataGenerator))]
        public void GetDynamicParametersReturnsCorrectPropertyValuesForAllergyAdverseEvent(string testName, AllergyAdverseEvent allergyAdverseEvent)
        {
            var dynamicParameters = allergyAdverseEvent.GetDynamicParameters();

            Assert.Equal(allergyAdverseEvent.DataSubType, dynamicParameters["D001"]);
            Assert.Equal(allergyAdverseEvent.Cause?.Value, dynamicParameters["D002"]);
            Assert.Equal(allergyAdverseEvent.Cause?.DisplayName, dynamicParameters["D003"]);
            Assert.Equal(allergyAdverseEvent.Cause?.CodeSystem?.Value, dynamicParameters["D004"]);
            Assert.Equal(allergyAdverseEvent.Cause?.CodeSystemName, dynamicParameters["D005"]);
            Assert.Equal(allergyAdverseEvent.DateRange?.DateLow?.Value, dynamicParameters["D006"]);
            Assert.Equal(allergyAdverseEvent.DateRange?.DateHigh?.Value, dynamicParameters["D007"]);
            Assert.Equal(allergyAdverseEvent.AllergyType?.Value, dynamicParameters["D010"]);
            Assert.Equal(allergyAdverseEvent.AllergyType?.DisplayName, dynamicParameters["D011"]);
            Assert.Equal(allergyAdverseEvent.AllergyType?.CodeSystem?.Value, dynamicParameters["D012"]);
            Assert.Equal(allergyAdverseEvent.AllergyType?.CodeSystemName, dynamicParameters["D013"]);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetDiagnosisProblems), MemberType = typeof(TestDataGenerator))]
        public void GetDynamicParametersReturnsCorrectPropertyValuesForDiagnosisProblem(string testName, DiagnosisProblem diagnosisProblem)
        {
            var dynamicParameters = diagnosisProblem.GetDynamicParameters();

            Assert.Equal(diagnosisProblem.ProblemResult?.Code?.Value, dynamicParameters["D001"]);
            Assert.Equal(diagnosisProblem.ProblemResult?.Code?.DisplayName, dynamicParameters["D002"]);
            Assert.Equal(diagnosisProblem.DateRange?.DateLow?.Value, dynamicParameters["D003"]);
            Assert.Equal(diagnosisProblem.DateRange?.DateHigh?.Value, dynamicParameters["D004"]);
            Assert.Equal(diagnosisProblem.DataSubType, dynamicParameters["D005"]);
            Assert.Equal(diagnosisProblem.ProblemResult?.Code?.CodeSystem?.Value, dynamicParameters["D011"]);
            Assert.Equal(diagnosisProblem.ProblemResult?.Code?.CodeSystemName, dynamicParameters["D012"]);
            Assert.Equal(diagnosisProblem.Severity?.Value, dynamicParameters["D018"]);
            Assert.Equal(diagnosisProblem.Severity?.DisplayName, dynamicParameters["D019"]);
            Assert.Equal(diagnosisProblem.Severity?.CodeSystem?.Value, dynamicParameters["D020"]);
            Assert.Equal(diagnosisProblem.ProblemResult?.Type.ToString(), dynamicParameters["D031"]);
            Assert.Equal(diagnosisProblem.Status?.Value, dynamicParameters["D032"]);
            Assert.Equal(diagnosisProblem.TargetSite?.Value, dynamicParameters["D033"]);
            Assert.Equal(diagnosisProblem.TargetSite?.DisplayName, dynamicParameters["D034"]);
            Assert.Equal(diagnosisProblem.TargetSite?.CodeSystem?.Value, dynamicParameters["D035"]);
            Assert.Equal(diagnosisProblem.TargetSite?.SdtcValueSet?.Value, dynamicParameters["D036"]);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetEncounters), MemberType = typeof(TestDataGenerator))]
        public void GetDynamicParametersReturnsCorrectPropertyValuesForEncounter(string testName, Encounter encounter)
        {
            var dynamicParameters = encounter.GetDynamicParameters();

            Assert.Equal(encounter.Code?.Value, dynamicParameters["D002"]);
            Assert.Equal(encounter.Code?.DisplayName, dynamicParameters["D003"]);
            Assert.Equal(encounter.Code?.CodeSystem?.Value, dynamicParameters["D004"]);
            Assert.Equal(encounter.Code?.CodeSystemName, dynamicParameters["D005"]);
            Assert.Equal(encounter.VisitDateRange?.DateLow?.Value, dynamicParameters["D006"]);
            Assert.Equal(encounter.VisitDateRange?.DateHigh?.Value, dynamicParameters["D007"]);
            Assert.Equal(encounter.DataSubType, dynamicParameters["D008"]);
            Assert.Equal(encounter.FacilityLocation?.Code?.Value, dynamicParameters["D013"]);
            Assert.Equal(encounter.FacilityLocation?.Code?.DisplayName, dynamicParameters["D014"]);
            Assert.Equal(encounter.FacilityLocation?.DateRange?.DateLow?.Value, dynamicParameters["D015"]);
            Assert.Equal(encounter.FacilityLocation?.DateRange?.DateHigh?.Value, dynamicParameters["D016"]);
            Assert.Equal(encounter.FacilityLocation?.Code?.CodeSystem?.Value, dynamicParameters["D017"]);
            Assert.Equal(encounter.FacilityLocation?.Code?.CodeSystemName, dynamicParameters["D018"]);
            Assert.Equal(encounter.DischargeDisposition?.Value, dynamicParameters["D019"]);
            Assert.Equal(encounter.DischargeDisposition?.CodeSystem?.Value, dynamicParameters["D020"]);
            Assert.Equal(encounter.PrincipalDiagnosis?.Value, dynamicParameters["D022"]);
            Assert.Equal(encounter.PrincipalDiagnosis?.DisplayName, dynamicParameters["D023"]);
            Assert.Equal(encounter.PrincipalDiagnosis?.CodeSystem?.Value, dynamicParameters["D024"]);
            Assert.Equal(encounter.PrincipalDiagnosis?.CodeSystemName, dynamicParameters["D025"]);
            Assert.Equal(encounter.EncounterDiagnosis?.Code?.Value, dynamicParameters["D026"]);
            Assert.Equal(encounter.EncounterDiagnosis?.Code?.DisplayName, dynamicParameters["D027"]);
            Assert.Equal(encounter.EncounterDiagnosis?.Code?.CodeSystem?.Value, dynamicParameters["D028"]);
            Assert.Equal(encounter.EncounterDiagnosis?.Code?.CodeSystemName, dynamicParameters["D029"]);
            Assert.Equal(encounter.EncounterDiagnosis?.DateRange?.DateLow?.Value, dynamicParameters["D030"]);
            Assert.Equal(encounter.EncounterDiagnosis?.DateRange?.DateHigh?.Value, dynamicParameters["D031"]);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetMedications), MemberType = typeof(TestDataGenerator))]
        public void GetDynamicParametersReturnsCorrectPropertyValuesForMedication(string testName, Medication medication)
        {
            var dynamicParameters = medication.GetDynamicParameters();

            Assert.Equal(medication.Product?.Value, dynamicParameters["D001"]);
            Assert.Equal(medication.Generic?.DisplayName, dynamicParameters["D002"]);
            Assert.Equal(medication.Product?.DisplayName, dynamicParameters["D003"]);
            Assert.Equal(medication.AdministeredDateRange?.DateLow?.Value, dynamicParameters["D012"]);
            Assert.Equal(medication.AdministeredDateRange?.DateHigh?.Value, dynamicParameters["D013"]);
            Assert.Equal(medication.DataSubType, dynamicParameters["D014"]);
            Assert.Equal(medication.NegationRationale?.Value, dynamicParameters["D021"]);
            Assert.Equal(medication.NegationRationale?.CodeSystem?.Value, dynamicParameters["D022"]);
            Assert.Equal(medication.NegationRationale?.DisplayName, dynamicParameters["D023"]);
            Assert.Equal(medication.Product?.CodeSystem?.Value, dynamicParameters["D024"]);
            Assert.Equal(medication.Product?.CodeSystemName, dynamicParameters["D025"]);
            Assert.Equal(medication.Refills, dynamicParameters["D026"]);
        }
    }
}
