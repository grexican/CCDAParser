namespace CqmSolution.Models
{
    public class AllergyAdverseEvent : CqmSolutionClientEntity
    {
        public AllergyAdverseEvent(Client client) : base(client) { }

        //TODO: is this correct?
        public override string SectionType => string.IsNullOrWhiteSpace(AllergyType?.Value) ? "ADVERSEEVENT" : "ALLERGY";

        public override string DataSubType
        {
            get => string.IsNullOrWhiteSpace(AllergyType?.Value) ? "ADV" : "ALG";
        }

        public Code Cause { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code AllergyType { get; set; }

        //TODO: If NotPresent is TRUE (from negationInd="true" on the observation node),
        // should we just ignore or filter out this AllergyAdverseEvent when we parse out ECQMs?
        public bool NotPresent { get; set; }
    }
}
