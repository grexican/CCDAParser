namespace CqmSolution.Models
{
    public class Immunization : CqmSolutionClientEntity
    {
        public Immunization(Client client) : base(client) { }

        public override string SectionType => "IMMUNIZATION";

        /// <summary>
        /// Data Sub-Type:	
        /// Administered: StatusCode = ADM
        /// Administered Not Done: StatusCode = ADMND
        /// Allergy: StatusCode = ALG	"Immunization, Allergy" and "Immunization, Intolerance" have been replaced with new data type "Allergy-Intolerance"
        /// Intolerance: StatusCode = INT
        ///
        /// The three data sub-types found in D014 represent the following distinctions:
        /// * Administered/Administered Not Done: A vaccine can be recorded as given to the patient or not.
        ///   If ‘Administered Not Done,’ there is typically an accompanying reason.
        /// * Allergy:  May include type 1 hypersensitivity reactions and other allergy-like reactions, including pseudo-allergy.
        ///   If this status code is used, information in the section will be related to observations about a patient's specific allergy intolerance.
        /// * Intolerance: Intolerance is a record of a clinical assessment of a propensity, or a potential risk to an individual,
        ///   to have a non-immune mediated adverse reaction on future exposure to the specified substance, or class of substance.
        ///   If this status code is used, information in the section will be related to observations about a patient's substance intolerance.
        /// </summary>
        public override string DataSubType
        {
            get => string.IsNullOrWhiteSpace(AdministeredDateRange?.DateHigh?.Value) ? "ADMND" : "ADM"; //TODO: is this correct?
        }

        public Code Product { get; set; }

        public string GenericName { get; set; }

        public CqmSolutionDateRange AdministeredDateRange { get; set; }

        public Code Reason { get; set; }
    }
}
