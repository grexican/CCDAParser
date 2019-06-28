namespace CqmSolution.Models
{
    public class Intervention : CqmSolutionClientEntity
    {
        public Intervention(Client client) : base(client) { }

        public override string SectionType => "INTERVENTION";

        /// <summary>
        /// Data Sub-Type:	
        /// Order: StatusCode = ORD
        /// Not Ordered: StatusCode = ORDND
        /// Performed: StatusCode = PRF
        /// Not Performed: StatusCode = PRFND or ND
        ///
        /// The three data sub-types found in D016 represent the following distinctions:
        /// * Order/Order Not Done: An intervention order is a request by a clinician to an appropriate provider
        ///   or facility to perform a service and/or other type of action necessary for care.
        /// * Performed: Interventions that have been completed.
        /// </summary>
        public override string DataSubType { get; }

        public Code Code { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code Reason { get; set; }
    }
}
