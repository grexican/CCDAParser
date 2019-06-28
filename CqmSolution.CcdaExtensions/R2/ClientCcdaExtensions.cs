using CqmSolution.Models;
using System.Linq;

namespace CqmSolution.CcdaExtensions.R2
{
    public static class ClientCcdaExtensions
    {
        public static Client GetClient(this POCD_MT000040ClinicalDocument cda)
        {
            return cda?.recordTarget?.FirstOrDefault()?.patientRole?.GetClientFromPatientRole();
        }

        public static Client GetClientFromPatientRole(this POCD_MT000040PatientRole patientRole)
        {
            var client = new Client
            {
                ClientIdentifier = patientRole?.id?.FirstOrDefault()?.extension,
                FirstName = patientRole?.patient?.name?.FirstOrDefault()?.Items?.FirstOrDefault(n => n.partType == "given")?.Text.FirstOrDefault(), //TODO: figure this out
                LastName = patientRole?.patient?.name?.FirstOrDefault()?.Items?.FirstOrDefault(n => n.partType == "family")?.Text.FirstOrDefault(), //TODO: figure this out
                DateOfBirth = patientRole?.patient?.birthTime.GetDate(),
                Gender = patientRole?.patient?.administrativeGenderCode.GetCode(),
                Address = patientRole?.addr?.FirstOrDefault()?.GetAddress(), //TODO: finish implementing GetAddress
                Phone = patientRole?.telecom?.FirstOrDefault()?.GetPhone(),
                Language = patientRole?.patient?.languageCommunication?.FirstOrDefault()?.languageCode.GetCode(),
                Ethnicity = patientRole?.patient?.ethnicGroupCode.GetCode(),
                Race = patientRole?.patient?.raceCode.GetCode()
            };

            return client;
        }
    }
}
