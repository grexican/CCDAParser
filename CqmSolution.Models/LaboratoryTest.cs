namespace CqmSolution.Models
{
    public class LaboratoryTest : CqmSolutionClientEntity
    {
        public LaboratoryTest(Client client) : base(client) { }

        public override string SectionType => "TESTRESULT";

        /// <summary>
        /// Data Sub-Type:	
        /// Order: StatusCode = ORD
        /// Not Ordered: StatusCode = ORDND
        /// Performed: StatusCode = PRF
        ///
        /// The four data sub-types in D010 represent the following distinctions:
        /// * Order/Order Not Done: A lab study can be recorded as ordered or not.
        ///   If not ordered, there is typically an accompanying reason.
        /// * Performed/Performed Not Done: A lab can be recorded as completed or not by its
        ///   performance status. If not completed, there is typically an accompanying reason.
        /// </summary>
        public override string DataSubType { get;}

        public Code Code { get; set; }

        public ResultValue ResultValue { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code Reason { get; set; }
    }
}
