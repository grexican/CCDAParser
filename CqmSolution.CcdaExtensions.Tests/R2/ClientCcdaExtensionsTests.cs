using CqmSolution.CcdaExtensions.R2;
using System.IO;
using System.Xml.Serialization;
using Xunit;

namespace CqmSolution.CcdaExtensions.Tests.R2
{
    /// Note: The Client section in test data files 4.xml through 9.xml is identical to that in 1.xml,
    /// so we are only running these tests on 1.xml through 3.xml.
    public class ClientCcdaExtensionsTests
    {
        //
        // TODO: Get some more Client test data that's in CCDA v2.x format.  2.xml and 3.xml are both v1.1, and the Client data in 4.xml - 9.xml is all copied from 1.xml.
        //

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "ClientID: F7137007-6385-4DE4-AFE3-32B93799221B (1.xml)", "F7137007-6385-4DE4-AFE3-32B93799221B")]
        public void GetClientSetsClientIdentifier(string path, string testName, string expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.ClientIdentifier);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "FirstName (1.xml)", "FirstName")]
        public void GetClientSetsFirstName(string path, string testName, string expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.FirstName);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "LastName (1.xml)", "LastName")]
        public void GetClientSetsLastName(string path, string testName, string expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.LastName);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Date Of Birth: Jan 01 1954 (1.xml)", "19540101")]
        public void GetClientSetsDateOfBirth(string path, string testName, string expected)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.DateOfBirth?.Value);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Gender: Male (1.xml)", "2.16.840.1.113883.5.1", null, "M", null, "Male")]
        public void GetClientSetsGender(string path, string testName, string cs, string csName, string code, string nullFlavor, string displayName)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(cs, client.Gender.CodeSystem?.Value);
            Assert.Equal(csName, client.Gender.CodeSystemName);
            Assert.Equal(code, client.Gender.Value);
            Assert.Equal(nullFlavor, client.Gender.NullFlavor);
            Assert.Equal(displayName, client.Gender.DisplayName);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Address: Zip only 92691 (1.xml)", null, null, null, null, "92691", "US")]
        public void GetClientSetsAddress(string path, string testName, string street1, string street2, string city, string state, string zip, string country)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(street1, client.Address.Street1);
            Assert.Equal(street2, client.Address.Street2);
            Assert.Equal(city, client.Address.City);
            Assert.Equal(state, client.Address.State);
            Assert.Equal(zip, client.Address.Zip);
            Assert.Equal(country, client.Address.Country);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Language: null (1.xml)", null, "UNK")]
        public void GetClientSetsLanguage(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.Language.Value);
            Assert.Equal(nullFlavor, client.Language.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Race: null (1.xml)", null, "UNK")]
        public void GetClientSetsRace(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.Race.Value);
            Assert.Equal(nullFlavor, client.Race.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Ethnicity: null (1.xml)", null, "UNK")]
        public void GetClientSetsEthnicity(string path, string testName, string expected, string nullFlavor)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.Ethnicity.Value);
            Assert.Equal(nullFlavor, client.Ethnicity.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Phone: null (1.xml)", null, null)]
        public void GetClientSetsPhone(string path, string testName, string expected, string type)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);

            var client = doc.GetClient();
            Assert.Equal(expected, client.Phone.Number);
            Assert.Equal(type, client.Phone.Type);
        }
    }
}