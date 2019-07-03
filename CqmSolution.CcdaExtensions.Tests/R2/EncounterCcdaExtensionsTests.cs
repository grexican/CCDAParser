using CqmSolution.CcdaExtensions.R2;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Xunit;

namespace CqmSolution.CcdaExtensions.Tests.R2
{
    public class EncounterCcdaExtensionsTests
    {
        // This doesn't vary by test file, so no need to test all of them.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Client (1.xml)")]
        public void GetEncountersSetsClient(string path, string testName)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(client, encounter?.Client);
        }

        // The SectionType for Encounter is always "ENCOUNTER", so there's no need to run this theory against all of our test data files.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "SectionType: Encounter (1.xml)", "ENCOUNTER")]
        public void GetEncountersSetsSectionType(string path, string testName, string expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.SectionType);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DataSubType: Performed (1.xml)", "PRF")]
        [XmlDocumentData("TestData/CCD/4.xml", "DataSubType: Performed (4.xml)", "PRF")]
        [XmlDocumentData("TestData/CCD/5.xml", "DataSubType: Performed (5.xml)", "PRF")]
        [XmlDocumentData("TestData/CCD/6.xml", "DataSubType: Performed (6.xml)", "PRF")]
        [XmlDocumentData("TestData/CCD/7.xml", "DataSubType: Performed (7.xml)", "PRF")]
        [XmlDocumentData("TestData/CCD/8.xml", "DataSubType: Performed (8.xml)", "PRF")]
        [XmlDocumentData("TestData/CCD/9.xml", "DataSubType: Performed (9.xml)", "PRF")]
        public void GetEncountersSetsDataSubType(string path, string testName, string expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.DataSubType);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Count: 5 (1.xml)", 5)]
        [XmlDocumentData("TestData/CCD/4.xml", "Count: 5 (4.xml)", 5)]
        [XmlDocumentData("TestData/CCD/5.xml", "Count: 5 (5.xml)", 5)]
        [XmlDocumentData("TestData/CCD/6.xml", "Count: 1 (6.xml)", 1)]
        [XmlDocumentData("TestData/CCD/7.xml", "Count: 1 (7.xml)", 1)]
        [XmlDocumentData("TestData/CCD/8.xml", "Count: 1 (8.xml)", 1)]
        [XmlDocumentData("TestData/CCD/9.xml", "Count: 1 (9.xml)", 1)]
        public void GetEncountersReturnsCorrectCount(string path, string testName, int expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();

            var encounters = doc.GetEncounters(client);
            Assert.Equal(expected, encounters?.Count);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Code: Exam Comp. New (1.xml)", "92004", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "Code: Exam Comp. New (4.xml)", "92004", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "Code: Exam Comp. New (5.xml)", "92004", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "Code: ambulatory (6.xml)", "AMB", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "Code: Discharge day management services (7.xml)", "99238", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "Code: Initial hospital care, per day, for the evaluation and management of a patient - 50 minutes (8.xml)", "99221", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "Code: Office outpatient visit 15 minutes (9.xml)", "99213", null)]
        public void GetEncountersSetsCode(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.Code?.Value);
            Assert.Equal(nullFlavor, encounter?.Code?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DateLow: Oct 05 2018 (1.xml)", "20181005", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "DateLow: Oct 05 2018 (4.xml)", "20181005", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "DateLow: Oct 05 2018 (5.xml)", "20181005", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "DateLow: Sep 18 2017 09:39:00 EDT (6.xml)", "20170918093900-0400", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "DateLow: Oct 28 2014 12:22:00 EST (7.xml)", "201410281222-0500", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "DateLow: Sep 27 2012 13:00:00 EST (8.xml)", "201209271300-0500", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "DateLow: Aug 15 2012 10:00:00 PST (9.xml)", "201208151000-0800", null)]
        public void GetEncountersSetsVisitDateLow(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.VisitDateRange?.DateLow?.Value);
            Assert.Equal(nullFlavor, encounter?.VisitDateRange?.DateLow?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DateHigh: Oct 05 2018 (1.xml)", "20181005", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "DateHigh: Oct 05 2018 (4.xml)", "20181005", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "DateHigh: Oct 05 2018 (5.xml)", "20181005", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "DateHigh: Sep 18 2017 09:51:00 EDT (6.xml)", "20170918095100-0400", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "DateHigh: Oct 31 2014 15:04:00 EST (7.xml)", "201410311504-0500", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "DateHigh: Sep 27 2012 13:00:00 EST (8.xml)", "201209271300-0500", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "DateHigh: Aug 15 2012 10:00:00 PST (9.xml)", "201208151000-0800", null)]
        public void GetEncountersSetsVisitDateHigh(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.VisitDateRange?.DateHigh?.Value);
            Assert.Equal(nullFlavor, encounter?.VisitDateRange?.DateHigh?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "FacilityLocationCode: Location 1 (1.xml)", "1124-7", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "FacilityLocationCode: Location 1 (4.xml)", "1124-7", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "FacilityLocationCode: Location 1 (5.xml)", "1124-7", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "FacilityLocationCode: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "FacilityLocationCode: Inpatient medical ward (7.xml)", "1061-1", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "FacilityLocationCode: Inpatient Medical Ward (8.xml)", "1060-3", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "FacilityLocationCode: Urgent Care Center (9.xml)", "1160-1", null)]
        public void GetEncountersSetsFacilityLocationCode(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.FacilityLocation?.Code?.Value);
            Assert.Equal(nullFlavor, encounter?.FacilityLocation?.Code?.NullFlavor);
        }

        ////TODO: add facility location dates to at least one test data file - WHERE?? See TODO in EncounterXmlExtensions.cs...
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "FacilityLocationDateLow: null (1.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/4.xml", "FacilityLocationDateLow: null (4.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/5.xml", "FacilityLocationDateLow: null (5.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/6.xml", "FacilityLocationDateLow: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "FacilityLocationDateLow: null (7.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/8.xml", "FacilityLocationDateLow: null (8.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/9.xml", "FacilityLocationDateLow: null (9.xml)", null, null)]
        public void GetEncountersSetsFacilityLocationDateLow(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.FacilityLocation?.DateRange?.DateLow?.Value);
            Assert.Equal(nullFlavor, encounter?.FacilityLocation?.DateRange?.DateLow?.NullFlavor);
        }

        ////TODO: add facility location dates to at least one test data file - WHERE?? See TODO in EncounterXmlExtensions.cs...
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "FacilityLocationDateHigh: null (1.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/4.xml", "FacilityLocationDateHigh: null (4.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/5.xml", "FacilityLocationDateHigh: null (5.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/6.xml", "FacilityLocationDateHigh: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "FacilityLocationDateHigh: null (7.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/8.xml", "FacilityLocationDateHigh: null (8.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/9.xml", "FacilityLocationDateHigh: null (9.xml)", null, null)]
        public void GetEncountersSetsFacilityLocationDateHigh(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.FacilityLocation?.DateRange?.DateHigh?.Value);
            Assert.Equal(nullFlavor, encounter?.FacilityLocation?.DateRange?.DateHigh?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DischargeDisposition: null (1.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/4.xml", "DischargeDisposition: null (4.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/5.xml", "DischargeDisposition: null (5.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/6.xml", "DischargeDisposition: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "DischargeDisposition: Discharged/Transferred to a Facility that Provides Custodial or Supportive Care (7.xml)", "04", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "DischargeDisposition: Discharged/transferred to an intermediate-care facility (ICF) (8.xml)", "04", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "DischargeDisposition: null (9.xml)", null, null)]
        public void GetEncountersSetsDischargeDispositionCode(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var encounter = doc.GetEncounters(client).FirstOrDefault();
            Assert.Equal(expected, encounter?.DischargeDisposition?.Value);
            Assert.Equal(nullFlavor, encounter?.DischargeDisposition?.NullFlavor);
        }

        //TODO:
        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "PrincipalDiagnosis: null (1.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/4.xml", "PrincipalDiagnosis: null (4.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/5.xml", "PrincipalDiagnosis: null (5.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/6.xml", "PrincipalDiagnosis: null (6.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/7.xml", "PrincipalDiagnosis: Congestive heart failure, unspecified (7.xml)", "428.0", null)]
        //[XmlDocumentData("TestData/CCD/8.xml", "PrincipalDiagnosis: null (8.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/9.xml", "PrincipalDiagnosis: null (9.xml)", null, null)]
        //public void GetEncountersSetsPrincipalDiagnosisCode(string path, string testName, string expected, string nullFlavor)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var encounter = doc.GetEncounters(client).FirstOrDefault();
        //    Assert.Equal(expected, encounter?.PrincipalDiagnosis?.Value);
        //    Assert.Equal(nullFlavor, encounter?.PrincipalDiagnosis?.NullFlavor);
        //}

        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "EncounterDiagnosis: Primary open-angle glaucoma, bilateral, mild stage (1.xml)", "H40.1131", null)]
        //[XmlDocumentData("TestData/CCD/4.xml", "EncounterDiagnosis: Primary open-angle glaucoma, bilateral, mild stage (4.xml)", "H40.1131", null)]
        //[XmlDocumentData("TestData/CCD/5.xml", "EncounterDiagnosis: Primary open-angle glaucoma, bilateral, mild stage (5.xml)", "H40.1131", null)]
        //[XmlDocumentData("TestData/CCD/6.xml", "EncounterDiagnosis: Asymmetry of maxilla (disorder) (6.xml)", "235083001", null)]
        //[XmlDocumentData("TestData/CCD/7.xml", "EncounterDiagnosis: null (7.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/8.xml", "EncounterDiagnosis: null (8.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/9.xml", "EncounterDiagnosis: Costal Chondritis (9.xml)", "64109004", null)]
        //public void GetEncountersSetsEncounterDiagnosisCode(string path, string testName, string expected, string nullFlavor)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var encounter = doc.GetEncounters(client).FirstOrDefault();
        //    Assert.Equal(expected, encounter?.EncounterDiagnosis?.Code?.Value);
        //    Assert.Equal(nullFlavor, encounter?.EncounterDiagnosis?.Code?.NullFlavor);
        //}

        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "EncounterDiagnosisDateLow: Oct 05 2018 (1.xml)", "20181005", null)]
        //[XmlDocumentData("TestData/CCD/4.xml", "EncounterDiagnosisDateLow: Oct 05 2018 (4.xml)", "20181005", null)]
        //[XmlDocumentData("TestData/CCD/5.xml", "EncounterDiagnosisDateLow: Oct 05 2018 (5.xml)", "20181005", null)]
        //[XmlDocumentData("TestData/CCD/6.xml", "EncounterDiagnosisDateLow: Sep 18 2017 09:39:00 EDT (6.xml)", "20170918093900-0400", null)]
        //[XmlDocumentData("TestData/CCD/7.xml", "EncounterDiagnosisDateLow: null (7.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/8.xml", "EncounterDiagnosisDateLow: null (8.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/9.xml", "EncounterDiagnosisDateLow: Aug 15 2012 (9.xml)", "20120815", null)]
        //public void GetEncountersSetsEncounterDiagnosisDateLow(string path, string testName, string expected, string nullFlavor)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var encounter = doc.GetEncounters(client).FirstOrDefault();
        //    Assert.Equal(expected, encounter?.EncounterDiagnosis?.DateRange?.DateLow?.Value);
        //    Assert.Equal(nullFlavor, encounter?.EncounterDiagnosis?.DateRange?.DateLow?.NullFlavor);
        //}

        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "EncounterDiagnosisDateHigh: null (1.xml)", null, "UNK")]
        //[XmlDocumentData("TestData/CCD/4.xml", "EncounterDiagnosisDateHigh: null (4.xml)", null, "UNK")]
        //[XmlDocumentData("TestData/CCD/5.xml", "EncounterDiagnosisDateHigh: null (5.xml)", null, "UNK")]
        //[XmlDocumentData("TestData/CCD/6.xml", "EncounterDiagnosisDateHigh: null (6.xml)", null, "NI")]
        //[XmlDocumentData("TestData/CCD/7.xml", "EncounterDiagnosisDateHigh: null (7.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/8.xml", "EncounterDiagnosisDateHigh: null (8.xml)", null, null)]
        //[XmlDocumentData("TestData/CCD/9.xml", "EncounterDiagnosisDateHigh: Sep 01 2017 (9.xml)", "20170901", null)]
        //public void GetEncountersSetsEncounterDiagnosisDateHigh(string path, string testName, string expected, string nullFlavor)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var encounter = doc.GetEncounters(client).FirstOrDefault();
        //    Assert.Equal(expected, encounter?.EncounterDiagnosis?.DateRange?.DateHigh?.Value);
        //    Assert.Equal(nullFlavor, encounter?.EncounterDiagnosis?.DateRange?.DateHigh?.NullFlavor);
        //}
    }
}
