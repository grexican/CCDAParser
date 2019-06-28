namespace CqmSolution.Models
{
    /// <summary>
    /// NOTE on the relationship between DiagnosticStudy and DiagnosticStudyComponent:
    /// A single Diagnostic Study may have multiple Components. These are expressed using the
    /// DIAGNOSTICSTUDY_COMPONENT (DiagnosticStudyComponent) data type on a separate row.
    /// The Diagnostic Study containing the Components must have IDRoot and IDExtension populated.
    /// Fields D001 and D002 of the Component data element will reference this ID.
    /// </summary>
    public class DiagnosticStudy : CqmSolutionClientEntity
    {
        public DiagnosticStudy(Client client) : base(client) { }

        public override string SectionType => "DIAGNOSTICSTUDY";

        /// <summary>
        /// Data Sub-Type:	
        /// Order: StatusCode = ORD
        /// Not Ordered: StatusCode = ORDND
        /// Performed: StatusCode = PRF
        /// Not Performed: StatusCode = PRFND
        ///
        /// The four data sub-types used in D008 represent the following distinctions:
        /// * Order/Order Not Done: A diagnostic study can be recorded as ordered or not.
        ///   If not ordered, there is typically an accompanying reason.
        /// * Performed/Performed Not Done: A diagnostic study can be recorded as completed or not by its performance status.
        ///   If not completed, there is typically an accompanying reason.
        /// </summary>
        public override string DataSubType { get; }

        public Code Code { get; set; }

        public ResultValue ResultValue { get; set; }

        public CqmSolutionDateRange DateRange { get; set; }

        public Code ReasonNegationRationale { get; set; }
    }
}