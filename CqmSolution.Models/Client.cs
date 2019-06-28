namespace CqmSolution.Models
{
    public class Client : CqmSolutionEntity
    {
        public override string SectionType { get; } = "CLIENT";

        public string ClientIdentifier { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public CqmSolutionDate DateOfBirth { get; set; }

        public Code Gender { get; set; }

        public Address Address { get; set; }
        
        public Phone Phone { get; set; }

        public Code Language { get; set; }

        /// <summary>
        /// "In addition to CDC race codes, you may also pass applicable nullFlavor indicators: 
        ///  D => ASKU
        ///  U => Unknown 
        /// blank => nullFlavor = ""UNK"""
        /// </summary>
        public Code Race { get; set; }

        /// <summary>
        /// "In addition to CDC race codes, you may also pass applicable nullFlavor indicators: 
        ///  D => ASKU
        ///  U => Unknown 
        /// blank => nullFlavor = ""UNK"""
        /// </summary>
        public Code Ethnicity { get; set; }

        public Code Religion { get; set; }

        /// <summary>
        /// Provider’s organization OID or other non-null value different than the OID for the Medicare HIC Number; Should be any OID other than 2.16.840.1.113883.4.572
        /// </summary>
        public Oid PatientIdentifierRootId { get; set; }

        /// <summary>
        /// Required Patient ID in QPP and HQR
        /// </summary>
        public string PatientIdentifier { get; set; }

        /// <summary>
        /// Patient Identification Number, optional in HQR and should be Medicare HIC Number (HQR)
        /// </summary>
        public string PatientIdentifierNumber { get; set; }

        /// <summary>
        /// OID must be 2.16.840.1.113883.4.572 if in use
        /// </summary>
        public Oid PatientIdentifierNumberOid { get; } = new Oid("2.16.840.1.113883.4.572");

        /// <summary>
        /// Root ID for MBI; Supported by HQR - not required
        /// </summary>
        public Oid PatientIdentifierMbiRootId { get; set; }

        /// <summary>
        /// CMS Master Beneficiary Identifier; Supported by HQR - not required
        /// </summary>
        public string PatientIdentifierMbi { get; protected set; }
    }
}