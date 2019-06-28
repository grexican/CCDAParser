namespace CqmSolution.Models
{
    public class Procedure : CqmSolutionClientEntity
    {
        public Procedure(Client client) : base(client) { }

        public override string SectionType => "PROCEDURE";

        /// <summary>
        /// Data Sub-Type:
        /// Order: StatusCode = ORD	
        /// Performed: StatusCode = PRF
        /// Not Performed: StatusCode = PRFND or ND
        ///
        /// The four(?) data sub-types in D020 represent the following distinctions:
        /// * Order: An order for a procedure that has not necessarily been performed.
        /// * Performed/Performed Not Done: A procedure can be recorded as completed or not.
        ///   If not completed, there is typically an accompanying reason. 
        /// </summary>
        public override string DataSubType { get; } //TODO: if MoodCode==ENV, DataSubType=PRF else if ModeCode==INT then DataSubType=ORD, else PRFND(?)

        public Code Code { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code NegationRationale { get; set; }

        public Code Ordinality { get; set; }

        //TODO: Do we really need a ResultValue here, or just a Code?
        // The spec lists D033 as ResultFindingValueType, but the only example value is CD.
        // Also, the Unit of the ResultFinding is never mentioned in the spec. If it can
        // never be a String or Physical Quantity, it would be simpler to just use Code,
        // and hard-code D033 to CD in CqmSolutionEntityExtensions.cs.
        public ResultValue ResultFinding { get; set; }

        public Code AnatomicalLocationSite { get; set; }
    }
}
