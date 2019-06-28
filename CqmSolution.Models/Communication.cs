namespace CqmSolution.Models
{
    public class Communication : CqmSolutionClientEntity
    {
        public Communication(Client client) : base(client) { }

        public override string SectionType => "COMMUNICATION";

        /// <summary>
        /// Data Sub-Type:	
        /// Patient to Provider: StatusCode = PatToProv
        /// Provider to Provider: StatusCode = ProvToProv
        /// Provider to Provider Not Done: StatusCode = ProvToProvND
        /// </summary>
        public override string DataSubType { get; }

        public Code Code { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code NegationRationale { get; set; }
    }
}