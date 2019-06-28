namespace CqmSolution.Models
{
    public class PatientCharacteristic : CqmSolutionClientEntity
    {
        public PatientCharacteristic(Client client) : base(client) { }

        public override string SectionType => "PATIENTCHARACTERISTIC";

        public override string DataSubType { get; } //TODO: should this really be empty?

        public Code Payer { get; set; }

        public CqmSolutionDate ExpiredDate { get; set; }

        public Code ExpiredReason { get; set; }
    }
}
