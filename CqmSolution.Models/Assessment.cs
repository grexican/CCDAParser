namespace CqmSolution.Models
{
    public class Assessment : CqmSolutionClientEntity
    {
        public Assessment(Client client) : base(client) { }

        public override string SectionType => "ASSESSMENT";

        /// <summary>
        /// Data Sub-Type:	
        /// Assessment Performed: StatusCode = PRF
        /// Assessment Not Performed: StatusCode = PRFND
        ///
        /// The subtype for Assessment is defined in D008:
        /// * Performed: Will apply assessments that have been completed; if an assessment
        ///   has not been performed, negation rationale (D014 – D016) can be populated 
        /// </summary>
        public override string DataSubType
        {
            get => string.IsNullOrWhiteSpace(NegationRationale?.Value) ? "PRF" : "PRFND"; //TODO: is this correct?
        }

        public Code Code { get; set; }

        public ResultValue ResultValue { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code NegationRationale { get; set; }
    }
}
