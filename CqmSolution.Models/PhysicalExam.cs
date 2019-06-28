namespace CqmSolution.Models
{
    public class PhysicalExam : CqmSolutionClientEntity
    {
        public PhysicalExam(Client client) : base(client) { }

        public override string SectionType => "PHYSICALEXAM";

        /// <summary>
        /// Data Sub-Type:	
        /// Performed: StatusCode = PRF
        /// Not Performed: StatusCode = PRFND or ND
        ///
        /// Physical exams with be either completed or not as indicated by D008 (PRF or PRFND/ND). 
        /// </summary>
        public override string DataSubType { get; } //TODO: if MoodCode is ENV, DataSubType = PRF else PRFND

        public Code Code { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code Reason { get; set; }

        public ResultValue ResultValue { get; set; }
    }
}
