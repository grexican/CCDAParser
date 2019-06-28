namespace CqmSolution.Models
{
    public class Encounter : CqmSolutionClientEntity
    {
        public Encounter(Client client, string dataSubType) : base(client)
        {
            DataSubType = dataSubType;
        }

        public override string SectionType => "ENCOUNTER";

        /// <summary>
        /// Data Sub-Type:	
        /// Performed: StatusCode = PRF
        ///
        /// The two data sub-types used in D008 represent the following distinctions:
        /// * Order: ordered interactions between the patient and clinician(s) that are not necessarily completed
        /// * Performed: interactions between the patient and clinician(s) that have been completed 
        /// </summary>
        public override string DataSubType { get; }

        public Code Code { get; set; }

        public CqmSolutionDateRange VisitDateRange { get; set; }

        public CodeWithDateRange FacilityLocation { get; set; }

        public Code DischargeDisposition { get; set; }

        public Code PrincipalDiagnosis { get; set; }

        public CodeWithDateRange EncounterDiagnosis { get; set; }
    }
}
