using System;
using System.Collections.Generic;

namespace CqmSolution.Models.Extensions
{
    public static class CqmSolutionEntityExtensions
    {
        public static Dictionary<string, string> GetDynamicParameters(this CqmSolutionEntity entity)
        {
            dynamic specificEntity = Convert.ChangeType(entity, entity.GetType());

            return GetDynamicParameters(specificEntity);
        }

        public static Dictionary<string, string> GetDynamicParameters(this Client client)
        {
            if (client == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", client.LastName},
                {"D002", client.FirstName},
                {"D003", client.MiddleName},
                {"D004", client.DateOfBirth?.Value},
                {"D005", client.Gender?.Value},
                {"D006", client.Gender?.DisplayName},
                {"D007", client.Address?.Type},
                {"D008", client.Address?.Street1},
                {"D009", client.Address?.Street2},
                {"D010", client.Address?.City},
                {"D011", client.Address?.State},
                {"D012", client.Address?.Zip},
                {"D013", client.Address?.Country},
                {"D014", client.Phone?.Type},
                {"D015", client.Phone?.Number},
                {"D016", client.Language?.Value},
                {"D017", client.Race?.Value},
                {"D018", client.Race?.DisplayName},
                {"D019", client.Ethnicity?.Value},
                {"D020", client.Ethnicity?.DisplayName},
                {"D021", client.Religion?.Value},
                {"D022", client.Religion?.DisplayName},
                {"D023", client.Language?.DisplayName},
                {"D024", client.AccountNumber},
                {"D035", client.PatientIdentifier},
                {"D037", client.PatientIdentifierRootId?.Value},
                {"D040", client.PatientIdentifierNumber},
                {"D041", client.PatientIdentifierNumberOid?.Value}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this AllergyAdverseEvent allergyAdverseEvent)
        {
            if (allergyAdverseEvent == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", allergyAdverseEvent.DataSubType},
                {"D002", allergyAdverseEvent.Cause?.Value},
                {"D003", allergyAdverseEvent.Cause?.DisplayName},
                {"D004", allergyAdverseEvent.Cause?.CodeSystem?.Value},
                {"D005", allergyAdverseEvent.Cause?.CodeSystemName},
                {"D006", allergyAdverseEvent.DateRange?.DateLow?.Value},
                {"D007", allergyAdverseEvent.DateRange?.DateHigh?.Value},
                {"D010", allergyAdverseEvent.AllergyType?.Value},
                {"D011", allergyAdverseEvent.AllergyType?.DisplayName},
                {"D012", allergyAdverseEvent.AllergyType?.CodeSystem?.Value},
                {"D013", allergyAdverseEvent.AllergyType?.CodeSystemName}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Assessment assessment)
        {
            if (assessment == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D002", assessment.Code?.Value},
                {"D003", assessment.Code?.DisplayName},
                {"D004", assessment.Code?.CodeSystem?.Value},
                {"D005", assessment.Code?.CodeSystemName},
                {"D006", assessment.DateRange?.DateLow?.Value},
                {"D007", assessment.DateRange?.DateHigh?.Value},
                {"D008", assessment.DataSubType},
                {"D009", assessment.ResultValue?.Value},
                {"D010", assessment.ResultValue?.Unit},
                {"D011", assessment.ResultValue?.Code?.Value},
                {"D012", assessment.ResultValue?.Code?.CodeSystem?.Value},
                {"D013", assessment.ResultValue?.Type.ToString()},
                {"D014", assessment.NegationRationale?.Value},
                {"D015", assessment.NegationRationale?.CodeSystem?.Value},
                {"D016", assessment.NegationRationale?.DisplayName}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Communication communication)
        {
            if (communication == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D002", communication.Code?.Value},
                {"D003", communication.Code?.DisplayName},
                {"D004", communication.Code?.CodeSystem?.Value},
                {"D005", communication.Code?.CodeSystemName},
                {"D006", communication.DateRange?.DateLow?.Value},
                {"D007", communication.DateRange?.DateHigh?.Value},
                {"D008", communication.DataSubType},
                {"D009", communication.NegationRationale?.Value},
                {"D010", communication.NegationRationale?.CodeSystem?.Value},
                {"D011", communication.NegationRationale?.DisplayName}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Device device)
        {
            if (device == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D002", device.Code?.Value},
                {"D003", device.Code?.DisplayName},
                {"D004", device.Code?.CodeSystem?.Value},
                {"D005", device.Code?.CodeSystemName},
                {"D006", device.DateRange?.DateLow?.Value},
                {"D007", device.DateRange?.DateHigh?.Value},
                {"D008", device.DataSubType}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this DiagnosisProblem diagnosisProblem)
        {
            if (diagnosisProblem == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", diagnosisProblem.ProblemResult?.Code?.Value},
                {"D002", diagnosisProblem.ProblemResult?.Code?.DisplayName},
                {"D003", diagnosisProblem.DateRange?.DateLow?.Value},
                {"D004", diagnosisProblem.DateRange?.DateHigh?.Value},
                {"D005", diagnosisProblem.DataSubType},
                {"D011", diagnosisProblem.ProblemResult?.Code?.CodeSystem?.Value},
                {"D012", diagnosisProblem.ProblemResult?.Code?.CodeSystemName},
                {"D018", diagnosisProblem.Severity?.Value},
                {"D019", diagnosisProblem.Severity?.DisplayName},
                {"D020", diagnosisProblem.Severity?.CodeSystem?.Value},
                {"D031", diagnosisProblem.ProblemResult?.Type.ToString()},
                {"D032", diagnosisProblem.Status?.Value},
                {"D033", diagnosisProblem.TargetSite?.Value},
                {"D034", diagnosisProblem.TargetSite?.DisplayName},
                {"D035", diagnosisProblem.TargetSite?.CodeSystem?.Value},
                {"D036", diagnosisProblem.TargetSite?.SdtcValueSet?.Value}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this DiagnosticStudy diagnosticStudy)
        {
            if (diagnosticStudy == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D002", diagnosticStudy.Code?.Value},
                {"D003", diagnosticStudy.Code?.DisplayName},
                {"D004", diagnosticStudy.Code?.CodeSystem?.Value},
                {"D005", diagnosticStudy.Code?.CodeSystemName},
                {"D006", diagnosticStudy.DateRange?.DateLow?.Value},
                {"D007", diagnosticStudy.DateRange?.DateHigh?.Value},
                {"D008", diagnosticStudy.DataSubType},
                {"D009", diagnosticStudy.ResultValue?.Value},
                {"D010", diagnosticStudy.ResultValue?.Unit},
                {"D011", diagnosticStudy.ResultValue?.Code?.Value},
                {"D012", diagnosticStudy.ResultValue?.Code?.CodeSystem?.Value},
                {"D013", diagnosticStudy.ResultValue?.DisplayName},
                {"D014", diagnosticStudy.ReasonNegationRationale?.Value},
                {"D015", diagnosticStudy.ReasonNegationRationale?.CodeSystem?.Value},
                {"D016", diagnosticStudy.ReasonNegationRationale?.DisplayName},
                {"D017", diagnosticStudy.ResultValue?.Type.ToString()}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this DiagnosticStudyComponent diagnosticStudyComponent)
        {
            if (diagnosticStudyComponent == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", diagnosticStudyComponent.ReferencedIdRoot?.Value},
                {"D002", diagnosticStudyComponent.ReferencedIdExtension},
                {"D003", diagnosticStudyComponent.Code?.Value},
                {"D004", diagnosticStudyComponent.Code?.DisplayName},
                {"D005", diagnosticStudyComponent.Code?.CodeSystem?.Value},
                {"D006", diagnosticStudyComponent.Code?.CodeSystemName},
                {"D007", diagnosticStudyComponent.Code?.SdtcValueSet.Value},
                {"D008", diagnosticStudyComponent.DateRange?.DateLow?.Value},
                {"D009", diagnosticStudyComponent.DateRange?.DateHigh?.Value},
                {"D010", diagnosticStudyComponent.ResultValue?.Type.ToString()},
                {"D011", diagnosticStudyComponent.ResultValue?.Value},
                {"D012", diagnosticStudyComponent.ResultValue?.Unit},
                {"D013", diagnosticStudyComponent.ResultValue?.Code?.Value},
                {"D014", diagnosticStudyComponent.ResultValue?.Code?.DisplayName},
                {"D015", diagnosticStudyComponent.ResultValue?.Code?.CodeSystem?.Value},
                {"D016", diagnosticStudyComponent.ResultValue?.Code?.CodeSystemName},
                {"D017", diagnosticStudyComponent.ResultValue?.Code?.SdtcValueSet.Value}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Encounter encounter)
        {
            if (encounter == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D002", encounter.Code?.Value},
                {"D003", encounter.Code?.DisplayName},
                {"D004", encounter.Code?.CodeSystem?.Value},
                {"D005", encounter.Code?.CodeSystemName},
                {"D006", encounter.VisitDateRange?.DateLow?.Value},
                {"D007", encounter.VisitDateRange?.DateHigh?.Value},
                {"D008", encounter.DataSubType},
                {"D013", encounter.FacilityLocation?.Code?.Value},
                {"D014", encounter.FacilityLocation?.Code?.DisplayName},
                {"D015", encounter.FacilityLocation?.DateRange?.DateLow?.Value},
                {"D016", encounter.FacilityLocation?.DateRange?.DateHigh?.Value},
                {"D017", encounter.FacilityLocation?.Code?.CodeSystem?.Value},
                {"D018", encounter.FacilityLocation?.Code?.CodeSystemName},
                {"D019", encounter.DischargeDisposition?.Value},
                {"D020", encounter.DischargeDisposition?.CodeSystem?.Value},
                {"D022", encounter.PrincipalDiagnosis?.Value},
                {"D023", encounter.PrincipalDiagnosis?.DisplayName},
                {"D024", encounter.PrincipalDiagnosis?.CodeSystem?.Value},
                {"D025", encounter.PrincipalDiagnosis?.CodeSystemName},
                {"D026", encounter.EncounterDiagnosis?.Code?.Value},
                {"D027", encounter.EncounterDiagnosis?.Code?.DisplayName},
                {"D028", encounter.EncounterDiagnosis?.Code?.CodeSystem?.Value},
                {"D029", encounter.EncounterDiagnosis?.Code?.CodeSystemName},
                {"D030", encounter.EncounterDiagnosis?.DateRange?.DateLow?.Value},
                {"D031", encounter.EncounterDiagnosis?.DateRange?.DateHigh?.Value}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Immunization immunization)
        {
            if (immunization == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", immunization.Product?.Value},
                {"D002", immunization.GenericName},
                {"D003", immunization.Product?.DisplayName},
                {"D012", immunization.AdministeredDateRange?.DateLow?.Value},
                {"D013", immunization.AdministeredDateRange?.DateHigh?.Value},
                {"D014", immunization.DataSubType},
                {"D021", immunization.Reason?.Value},
                {"D022", immunization.Reason?.CodeSystem?.Value},
                {"D023", immunization.Reason?.DisplayName},
                {"D024", immunization.Product?.CodeSystem?.Value},
                {"D025", immunization.Product?.CodeSystemName}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Intervention intervention)
        {
            if (intervention == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D004", intervention.Code?.Value},
                {"D005", intervention.Code?.DisplayName},
                {"D006", intervention.Code?.CodeSystem?.Value},
                {"D007", intervention.Code?.CodeSystemName},
                {"D008", intervention.DateRange?.DateLow?.Value},
                {"D009", intervention.DateRange?.DateHigh?.Value},
                {"D010", intervention.DataSubType},
                {"D011", intervention.Reason?.Value},
                {"D012", intervention.Reason?.CodeSystem?.Value},
                {"D013", intervention.Reason?.DisplayName}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this LaboratoryTest laboratoryTest)
        {
            if (laboratoryTest == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D003", laboratoryTest.Code?.Value},
                {"D004", laboratoryTest.Code?.DisplayName},
                {"D005", laboratoryTest.ResultValue?.Value},
                {"D006", laboratoryTest.ResultValue?.Unit},
                {"D012", laboratoryTest.DateRange?.DateLow?.Value},
                {"D013", laboratoryTest.DateRange?.DateHigh?.Value},
                {"D014", laboratoryTest.Code?.CodeSystem?.Value},
                {"D015", laboratoryTest.Code?.CodeSystemName},
                {"D016", laboratoryTest.DataSubType},
                {"D017", laboratoryTest.ResultValue?.Code?.Value},
                {"D018", laboratoryTest.ResultValue?.Code?.DisplayName},
                {"D019", laboratoryTest.ResultValue?.Type.ToString()},
                {"D020", laboratoryTest.ResultValue?.Code?.CodeSystem?.Value},
                {"D022", laboratoryTest.Reason?.Value},
                {"D023", laboratoryTest.Reason?.CodeSystem?.Value}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Medication medication)
        {
            if (medication == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", medication.Product?.Value},
                {"D002", medication.Generic?.DisplayName},
                {"D003", medication.Product?.DisplayName},
                {"D012", medication.AdministeredDateRange?.DateLow?.Value},
                {"D013", medication.AdministeredDateRange?.DateHigh?.Value},
                {"D014", medication.DataSubType},
                {"D021", medication.NegationRationale?.Value},
                {"D022", medication.NegationRationale?.CodeSystem?.Value},
                {"D023", medication.NegationRationale?.DisplayName},
                {"D024", medication.Product?.CodeSystem?.Value},
                {"D025", medication.Product?.CodeSystemName},
                {"D026", medication.Refills}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this PatientCharacteristic patientCharacteristic)
        {
            if (patientCharacteristic == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", patientCharacteristic.Payer?.Value},
                {"D002", patientCharacteristic.Payer?.DisplayName},
                {"D012", patientCharacteristic.ExpiredDate?.Value},
                {"D021", patientCharacteristic.ExpiredReason?.Value},
                {"D023", patientCharacteristic.ExpiredReason?.DisplayName}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this PhysicalExam physicalExam)
        {
            if (physicalExam == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D002", physicalExam.Code?.Value},
                {"D003", physicalExam.Code?.DisplayName},
                {"D004", physicalExam.Code?.CodeSystem?.Value},
                {"D005", physicalExam.Code?.CodeSystemName},
                {"D006", physicalExam.DateRange?.DateLow?.Value},
                {"D007", physicalExam.DateRange?.DateHigh?.Value},
                {"D008", physicalExam.DataSubType},
                {"D009", physicalExam.Reason?.Value},
                {"D010", physicalExam.Reason?.DisplayName},
                {"D011", physicalExam.Reason?.CodeSystem?.Value},
                {"D013", physicalExam.ResultValue?.Value},
                {"D014", physicalExam.ResultValue?.Unit},
                {"D015", physicalExam.ResultValue?.Code?.Value},
                {"D016", physicalExam.ResultValue?.Code?.CodeSystem?.Value},
                {"D017", physicalExam.ResultValue?.Type.ToString()}
            };
        }

        public static Dictionary<string, string> GetDynamicParameters(this Procedure procedure)
        {
            if (procedure == null) return new Dictionary<string, string>();

            return new Dictionary<string, string>
            {
                {"D001", procedure.Code?.Value},
                {"D002", procedure.Code?.DisplayName},
                {"D005", procedure.DateRange?.DateLow?.Value},
                {"D016", procedure.DateRange?.DateHigh?.Value},
                {"D017", procedure.NegationRationale?.Value},
                {"D018", procedure.NegationRationale?.CodeSystem?.Value},
                {"D019", procedure.NegationRationale?.DisplayName},
                {"D020", procedure.DataSubType},
                {"D021", procedure.Code?.CodeSystem?.Value},
                {"D022", procedure.Code?.CodeSystemName},
                {"D027", procedure.Ordinality?.Value},
                {"D031", procedure.ResultFinding?.Value},
                {"D032", procedure.ResultFinding?.Code?.CodeSystem?.Value},
                {"D033", procedure.ResultFinding?.Type.ToString()},
                {"D037", procedure.AnatomicalLocationSite?.Value},
                {"D038", procedure.AnatomicalLocationSite?.DisplayName},
                {"D039", procedure.AnatomicalLocationSite?.CodeSystem?.Value},
                {"D040", procedure.AnatomicalLocationSite?.CodeSystemName}
            };
        }
    }
}
