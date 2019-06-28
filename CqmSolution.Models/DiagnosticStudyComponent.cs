namespace CqmSolution.Models
{
    public class DiagnosticStudyComponent : CqmSolutionClientEntity
    {
        public DiagnosticStudyComponent(Client client) : base(client) { }

        public override string SectionType => "DIAGNOSTICSTUDY_COMPONENT";

        public override string DataSubType { get; } //TODO: should this really be empty?

        /// <summary>
        /// Fields D001 & D002(Referenced.IdRoot & Referenced.IdExtension) refer to the Id element
        /// of the diagnostic study row to which these components belong
        /// </summary>
        public Oid ReferencedIdRoot { get; set;  }
        public string ReferencedIdExtension { get; set;  }

        /// <summary>
        /// The Code element(fields D003 to D007) are usually populated with the same value as the
        /// Code element from the referenced Diagnostic Study
        /// </summary>
        public Code Code { get; set; }

        /// <summary>
        /// The Value element(fields D010 to D017) represent the "Component.Result", and are typically
        /// populated with a CD type value.
        /// </summary>
        public ResultValue ResultValue { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }
    }
}
