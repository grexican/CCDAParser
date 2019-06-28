namespace CqmSolution.Models
{
    public class Device : CqmSolutionClientEntity
    {
        public Device(Client client) : base(client) { }

        public override string SectionType => "DEVICE";

        /// <summary>
        /// Data Sub-Type:	
        /// Applied: StatusCode = APL
        /// </summary>
        public override string DataSubType { get => "APL"; }

        public Code Code { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }
    }
}
