using CqmSolution.CcdaExtensions.R2;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Xunit;

namespace CqmSolution.CcdaExtensions.Tests.R2
{
    /// Note: The AllergyAdverseEvents section in test data files 6.xml through 9.xml is identical to that in 5.xml,
    /// so we are only running these tests on 1.xml through 5.xml.
    public class AllergyAdverseEventCcdaExtensionsTests
    {
        // This doesn't vary by test file, so no need to test all of them.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Client (1.xml)")]
        public void GetAllergyAdverseEventsSetsClient(string path, string testName)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
            Assert.Equal(client, allergyAdverseEvent?.Client);
        }

        //TODO: uncomment these tests once the parsing is all done
        ////TODO: add Adverse Event test data
        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "SectionType: Allergy (1.xml)", "ALLERGY")]
        //[XmlDocumentData("TestData/CCD/4.xml", "SectionType: Allergy (4.xml)", "ALLERGY")]
        //[XmlDocumentData("TestData/CCD/5.xml", "SectionType: Allergy (5.xml)", "ALLERGY")]
        //public void GetAllergyAdverseEventsSetsSectionType(string path, string testName, string expected)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
        //    Assert.Equal(expected, allergyAdverseEvent?.SectionType);
        //}

        ////TODO: add Adverse Event test data
        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "DataSubType: Allergy (1.xml)", "ALG")]
        //[XmlDocumentData("TestData/CCD/4.xml", "DataSubType: Allergy (4.xml)", "ALG")]
        //[XmlDocumentData("TestData/CCD/5.xml", "DataSubType: Allergy (5.xml)", "ALG")]
        //public void GetAllergyAdverseEventsSetsDataSubType(string path, string testName, string expected)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
        //    Assert.Equal(expected, allergyAdverseEvent?.DataSubType);
        //}

        //TODO: add test data with a count > 1
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Count: 1 (1.xml)", 1)]
        [XmlDocumentData("TestData/CCD/4.xml", "Count: 1 (4.xml)", 1)]
        [XmlDocumentData("TestData/CCD/5.xml", "Count: 1 (5.xml)", 1)]
        public void GetAllergyAdverseEventsReturnsCorrectCount(string path, string testName, int expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();

            var allergyAdverseEvents = doc.GetAllergyAdverseEvents(client);
            Assert.Equal(expected, allergyAdverseEvents?.Count);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Cause: null (1.xml)", null, "NA")]
        [XmlDocumentData("TestData/CCD/4.xml", "Cause: Codeine (4.xml)", "2670", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "Cause: Penicillin (5.xml)", "7980", null)]
        public void GetAllergyAdverseEventsSetsCause(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
            Assert.Equal(expected, allergyAdverseEvent?.Cause?.Value);
            Assert.Equal(nullFlavor, allergyAdverseEvent?.Cause?.NullFlavor);
        }

        //TODO: uncomment these tests once the parsing is all done
        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "AllergyType: Breathing Difficulty (1.xml)", "230145002", null)]
        //[XmlDocumentData("TestData/CCD/4.xml", "AllergyType: Drug Intolerance (disorder) (4.xml)", "59037007", null)]
        //[XmlDocumentData("TestData/CCD/5.xml", "AllergyType: Drug Allergy (5.xml)", "416098002", null)]
        //public void GetAllergyAdverseEventsSetsAllergyType(string path, string testName, string expected, string nullFlavor)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
        //    Assert.Equal(expected, allergyAdverseEvent?.AllergyType?.Value);
        //    Assert.Equal(nullFlavor, allergyAdverseEvent?.AllergyType?.NullFlavor);
        //}

        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "DateLow: Oct 24 2018 (1.xml)", "20181024", null)]
        //[XmlDocumentData("TestData/CCD/4.xml", "DateLow: Jan 03 2018 (4.xml)", "20140103", null)]
        //[XmlDocumentData("TestData/CCD/5.xml", "DateLow: Jan 04 2014 12:35:06 EST (5.xml)", "20140104123506-0500", null)]
        //public void GetAllergyAdverseEventsSetsDateRangeLow(string path, string testName, string expected, string nullFlavor)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
        //    Assert.Equal(expected, allergyAdverseEvent?.DateRange?.DateLow?.Value);
        //    Assert.Equal(nullFlavor, allergyAdverseEvent?.DateRange?.DateLow?.NullFlavor);
        //}

        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "DateHigh: null (1.xml)", null, "UNK")]
        //[XmlDocumentData("TestData/CCD/4.xml", "DateHigh: Jan 03 2019 (4.xml)", "20190103", null)]
        //[XmlDocumentData("TestData/CCD/5.xml", "DateHigh: null (5.xml)", null, null)]
        //public void GetAllergyAdverseEventsSetsDateRangeHigh(string path, string testName, string expected, string nullFlavor)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
        //    Assert.Equal(expected, allergyAdverseEvent?.DateRange?.DateHigh?.Value);
        //    Assert.Equal(nullFlavor, allergyAdverseEvent?.DateRange?.DateHigh?.NullFlavor);
        //}

        //[Theory]
        //[XmlDocumentData("TestData/CCD/1.xml", "NotPresent: false (1.xml)", false)]
        //[XmlDocumentData("TestData/CCD/4.xml", "NotPresent: false (4.xml)", false)]
        //[XmlDocumentData("TestData/CCD/5.xml", "NotPresent: false (5.xml)", false)]
        //public void GetAllergyAdverseEventsSetsNotPresent(string path, string testName, bool expected)
        //{
        //    var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
        //    var reader = new StreamReader(path);

        //    var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
        //    var client = doc.GetClient();
        //    var allergyAdverseEvent = doc.GetAllergyAdverseEvents(client).FirstOrDefault();
        //    Assert.Equal(expected, allergyAdverseEvent?.NotPresent);
        //}
    }
}
