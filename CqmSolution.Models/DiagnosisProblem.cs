namespace CqmSolution.Models
{
    public class DiagnosisProblem : CqmSolutionClientEntity
    {
        public DiagnosisProblem(Client client) : base(client) { }

        public override string SectionType => "PROBLEM";

        /// <summary>
        /// Data Sub-Type:	
        /// Diagnosis: StatusCode = DIAG
        /// </summary>
        public override string DataSubType { get => "DIAG"; }

        //TODO: Do we really need a ResultValue here, or just a Code?
        //The spec lists D031 as ProblemCodeType, but the only example value is CD.
        // Also, the Unit of the ProblemCode is never mentioned in the spec. If it can
        // never be a String or Physical Quantity, it would be simpler to just use Code,
        // and hard-code D031 to CD in CqmSolutionEntityExtensions.cs.
        public ResultValue ProblemResult { get; set; }

        public Code Severity { get; set; }

        public Code TargetSite { get; set; }

        /// <summary>
        /// The statusCode represents whether this is an active (active), inactive (suspended) or resolved (completed) diagnosis.
        /// http://www.hl7.org/documentcenter/public_temp_FBCF567B-1C23-BA17-0C9D61BADEF603A1/standards/vocabulary/vocabulary_tables/infrastructure/vocabulary/vs_ActStatus.html#ActStatus
        /// </summary>
        public Code Status { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        //TODO: If NotPresent is TRUE (from negationInd="true" on the observation node),
        // should we just ignore or filter out this DiagnosisProblem when we parse out ECQMs?
        public bool NotPresent { get; set; }
    }
}
