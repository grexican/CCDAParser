namespace CqmSolution.Models
{
    /// <summary>
    /// The author of the document in question. NOTE: we don't need to parse this out; we generate it.
    /// I'm not even sure if we need this as an entity, or if it's just something we map/generate from existing data. TBD
    /// </summary>
    public class Author : CqmSolutionEntity
    {
        public override string SectionType { get; } = "AUTHOR";
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameSuffix { get; set; }

        public Phone Telecom { get; set; }

        public Address Address { get; set; }

        public string RepresentedOrganizationName { get; set; }

        // ...
    }
}
