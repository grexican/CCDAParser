 namespace CqmSolution.Models
{
    public abstract class CqmSolutionEntity
    {
        public virtual string RecordType { get; set; } = "CQM";
        public abstract string SectionType { get; }
        public virtual Oid ValueSetOid { get; protected set; }
        public virtual string AccountNumber { get; protected set; } //TODO: Should this be settable?
        public virtual Oid IdRoot { get; protected set; }
        public virtual string IdExtension { get; protected set; }
    }

    public abstract class CqmSolutionClientEntity : CqmSolutionEntity
    {
        public Client Client { get; }

        public abstract string DataSubType { get; }

        protected CqmSolutionClientEntity(Client client)
        {
            Client = client;
        }
    }
}
