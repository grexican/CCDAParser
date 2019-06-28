using System.Linq;
using HtmlAgilityPack;
using Xunit;

namespace CqmSolution.XmlExtensions.Tests
{
    public class MedicationXmlExtensionsTests
    {
        // This doesn't vary by test file, so no need to test all of them.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Client (1.xml)")]
        public void GetMedicationsSetsClient(HtmlDocument doc, string testName)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(client, medication?.Client);
        }

        // The SectionType for Medication is always "MEDICATION", so there's no need to run this theory against all of our test data files.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "SectionType: Medication (1.xml)", "MEDICATION")]
        public void GetMedicationsSetsSectionType(HtmlDocument doc, string testName, string expected)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.SectionType);
        }

        //TODO: add/modify test data for "ADV", "DSCND", "ORD", "ORDND"
        //https://docs.google.com/spreadsheets/d/1d0RJoyzVQISK4ai3zbjKT6PCuOEQzgWBCqarDHyD-JE/edit#gid=798164205
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DataSubType: Dispensed (1.xml)", "DISP")]
        [XmlDocumentData("TestData/CCD/2.xml", "DataSubType: Dispensed (2.xml)", "DISP")]
        [XmlDocumentData("TestData/CCD/3.xml", "DataSubType: Active (3.xml)", "ACT")]
        [XmlDocumentData("TestData/CCD/4.xml", "DataSubType: Active (4.xml)", "ACT")]
        [XmlDocumentData("TestData/CCD/5.xml", "DataSubType: Administered Not Done (5.xml)", "ADMND")]
        [XmlDocumentData("TestData/CCD/6.xml", "DataSubType: Administered (6.xml)", "ADM")]
        [XmlDocumentData("TestData/CCD/7.xml", "DataSubType: Discharge (7.xml)", "DSC")]
        [XmlDocumentData("TestData/CCD/8.xml", "DataSubType: Discharge (8.xml)", "DSC")]
        [XmlDocumentData("TestData/CCD/9.xml", "DataSubType: Allergy (9.xml)", "ALG")]
        public void GetMedicationsSetsDataSubType(HtmlDocument doc, string testName, string expected)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.DataSubType);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Count: 1 (1.xml)", 1)]
        [XmlDocumentData("TestData/CCD/2.xml", "Count: 41 (2.xml)", 41)]
        [XmlDocumentData("TestData/CCD/3.xml", "Count: 20 (3.xml)", 20)]
        [XmlDocumentData("TestData/CCD/4.xml", "Count: 1 (4.xml)", 1)]
        [XmlDocumentData("TestData/CCD/5.xml", "Count: 1 (5.xml)", 1)]
        [XmlDocumentData("TestData/CCD/6.xml", "Count: 1 (6.xml)", 1)]
        [XmlDocumentData("TestData/CCD/7.xml", "Count: 1 (7.xml)", 1)]
        [XmlDocumentData("TestData/CCD/8.xml", "Count: 1 (8.xml)", 1)]
        [XmlDocumentData("TestData/CCD/9.xml", "Count: 3 (9.xml)", 3)]
        public void GetMedicationsReturnsCorrectCount(HtmlDocument doc, string testName, int expected)
        {
            var client = doc.GetClient();

            var medications = doc.GetMedications(client);
            Assert.Equal(expected, medications?.Count);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Product: null (1.xml)", null, "UNK")]
        [XmlDocumentData("TestData/CCD/2.xml", "Product: LASIX, 40MG (Oral Tablet) (2.xml)", "200809", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "Product: CLONIDINE HCL, 0.1MG (Oral Tablet) (3.xml)", "884173", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "Product: Ibuprofen 600mg Oral Tablet (4.xml)", "197806", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "Product: Lisninopril 10mg Oral Tablet (5.xml)", "314076", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "Product: Aspirin 81 MG Oral Tablet (6.xml)", "243670", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "Product: Sudafed 30 MG Oral Tablet (7.xml)", "1049529", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "Product: 3 ML Insulin Glargine 100 UNT/ML Pen Injector [Lantus] (8.xml)", "847232", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "Product: penicillin (9.xml)", "7980", null)]
        public void GetMedicationsSetsProduct(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.Product?.Value);
            Assert.Equal(nullFlavor, medication?.Product?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Generic: null (1.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/2.xml", "Generic: null (2.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/3.xml", "Generic: null (3.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/4.xml", "Generic: Ibuprofen 600mg Oral Tablet (4.xml)", "Ibuprofen 600mg Oral Tablet", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "Generic: null (5.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/6.xml", "Generic: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "Generic: PSEUDOEPHEDRINE HCL 30MG TAB UD (7.xml)", "PSEUDOEPHEDRINE HCL 30MG TAB UD", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "Generic: null (8.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/9.xml", "Generic: null", null, null)]
        public void GetMedicationsSetsGeneric(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.Generic?.DisplayName);
            Assert.Equal(nullFlavor, medication?.Generic?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "AdministeredDateLow: Oct 24 2018 (1.xml)", "20181024", null)]
        [XmlDocumentData("TestData/CCD/2.xml", "AdministeredDateLow: May 08 2012 (2.xml)", "20120508", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "AdministeredDateLow: Sep 04 2012 (3.xml)", "20120904", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "AdministeredDateLow: Dec 18 2013 (4.xml)", "20131218", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "AdministeredDateLow: Mar 15 2018 11:23:05 EST (5.xml)", "20180315112305-0500", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "AdministeredDateLow: Sep 11 2013 16:03:00 PDT (6.xml)", "201309111603-0700", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "AdministeredDateLow: Jan 18 2014 (7.xml)", "20140118", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "AdministeredDateLow: Jan 09 2009 (8.xml)", "20090109", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "AdministeredDateLow: Mar 10 2013 (9.xml)", "20130310", null)]
        public void GetMedicationsSetsAdministeredDateLow(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.AdministeredDateRange?.DateLow?.Value);
            Assert.Equal(nullFlavor, medication?.AdministeredDateRange?.DateLow?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "AdministeredDateHigh: null (1.xml)", null, "UNK")]
        [XmlDocumentData("TestData/CCD/2.xml", "AdministeredDateHigh: Jan 31 2013 (2.xml)", "20130131", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "AdministeredDateHigh: null (3.xml)", null, "NI")]
        [XmlDocumentData("TestData/CCD/4.xml", "AdministeredDateHigh: null (4.xml)", null, "NI")]
        [XmlDocumentData("TestData/CCD/5.xml", "AdministeredDateHigh: Mar 15 2018 11:23:05 EST (5.xml)", "20180315112305-0500", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "AdministeredDateHigh: Sep 11 2013 16:03:00 PDT (6.xml)", "201309111603-0700", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "AdministeredDateHigh: null (7.xml)", null, "NI")]
        [XmlDocumentData("TestData/CCD/8.xml", "AdministeredDateHigh: null (8.xml)", null, "NI")]
        [XmlDocumentData("TestData/CCD/9.xml", "AdministeredDateHigh: Mar 10 2013 (9.xml)", "20130310", null)]
        public void GetMedicationsSetsAdministeredDateHigh(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.AdministeredDateRange?.DateHigh?.Value);
            Assert.Equal(nullFlavor, medication?.AdministeredDateRange?.DateHigh?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "NegationRationale: null (1.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/2.xml", "NegationRationale: null (2.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/3.xml", "NegationRationale: null (3.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/4.xml", "NegationRationale: null (4.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/5.xml", "NegationRationale: Refusal of treatment by patient (5.xml)", "105480006", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "NegationRationale: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "NegationRationale: null (7.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/8.xml", "NegationRationale: null (8.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/9.xml", "NegationRationale: null (9.xml)", null, null)]
        public void GetMedicationsSetsNegationRationale(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.NegationRationale?.Value);
            Assert.Equal(nullFlavor, medication?.NegationRationale?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Refills: 1 (1.xml)", "1")]
        [XmlDocumentData("TestData/CCD/2.xml", "Refills: 2 (2.xml)", "2")]
        [XmlDocumentData("TestData/CCD/3.xml", "Refills: 7 (3.xml)", "7")]
        [XmlDocumentData("TestData/CCD/4.xml", "Refills: null (4.xml)", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "Refills: null (5.xml)", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "Refills: null (6.xml)", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "Refills: null (7.xml)", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "Refills: null (8.xml)", null)]
        [XmlDocumentData("TestData/CCD/9.xml", "Refills: null (9.xml)", null)]
        public void GetMedicationsSetsRefills(HtmlDocument doc, string testName, string expected)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.Refills);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "NotPresent: false (1.xml)", false)]
        [XmlDocumentData("TestData/CCD/2.xml", "NotPresent: false (2.xml)", false)]
        [XmlDocumentData("TestData/CCD/3.xml", "NotPresent: false (3.xml)", false)]
        [XmlDocumentData("TestData/CCD/4.xml", "NotPresent: false (4.xml)", false)]
        [XmlDocumentData("TestData/CCD/5.xml", "NotPresent: TRUE (5.xml)", true)]
        [XmlDocumentData("TestData/CCD/6.xml", "NotPresent: false (6.xml)", false)]
        [XmlDocumentData("TestData/CCD/7.xml", "NotPresent: false (7.xml)", false)]
        [XmlDocumentData("TestData/CCD/8.xml", "NotPresent: false (8.xml)", false)]
        [XmlDocumentData("TestData/CCD/9.xml", "NotPresent: false (9.xml)", false)]
        public void GetMedicationsSetsNotPresent(HtmlDocument doc, string testName, bool expected)
        {
            var client = doc.GetClient();
            var medication = doc.GetMedications(client).FirstOrDefault();
            Assert.Equal(expected, medication?.NotPresent);
        }
    }
}
