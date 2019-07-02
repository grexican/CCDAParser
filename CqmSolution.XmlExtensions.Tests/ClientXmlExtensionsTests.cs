using HtmlAgilityPack;
using Xunit;

namespace CqmSolution.XmlExtensions.Tests
{
    /// <summary>
    /// Note: The Client section in test data files 4.xml through 9.xml is identical to that in 1.xml,
    /// so we are only running these tests on 1.xml through 3.xml.
    /// </summary>
    public class ClientXmlExtensionsTests
    {
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "F7137007-6385-4DE4-AFE3-32B93799221B")]
        [XmlDocumentData("TestData/CCD/2.xml", "f887b894-6ab0-4c9d-833e-450b34ee4fe5")]
        [XmlDocumentData("TestData/CCD/3.xml", "051d58bb-2345-47a9-b7f7-41bff9f06f5e")]
        public void GetClientSetsClientIdentifier(HtmlDocument doc, string expected)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.ClientIdentifier);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "FirstName")]
        [XmlDocumentData("TestData/CCD/2.xml", "Kelsey")]
        [XmlDocumentData("TestData/CCD/3.xml", "Malaysia")]
        public void GetClientSetsFirstName(HtmlDocument doc, string expected)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.FirstName);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "LastName")]
        [XmlDocumentData("TestData/CCD/2.xml", "Smith")]
        [XmlDocumentData("TestData/CCD/3.xml", "Smith")]
        public void GetClientSetsLastName(HtmlDocument doc, string expected)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.LastName);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "19540101")]
        [XmlDocumentData("TestData/CCD/2.xml", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "19281201131415")]
        public void GetClientSetsDateOfBirth(HtmlDocument doc, string expected)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.DateOfBirth?.Value);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "2.16.840.1.113883.5.1", null, "M", null, "Male")]
        [XmlDocumentData("TestData/CCD/2.xml", "2.16.840.1.113883.5.1", null, "F", null, null)]
        [XmlDocumentData("TestData/CCD/3.xml", null, null, null, "UNK", "Unknown")]
        public void GetClientSetsGender(HtmlDocument doc, string cs, string csName, string code, string nullFlavor, string displayName)
        {
            var client = doc.GetClient();
            Assert.Equal(cs, client.Gender.CodeSystem?.Value);
            Assert.Equal(csName, client.Gender.CodeSystemName);
            Assert.Equal(code, client.Gender.Value);
            Assert.Equal(nullFlavor, client.Gender.NullFlavor);
            Assert.Equal(displayName, client.Gender.DisplayName);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", null, null, null, null, "92691", "US")]
        [XmlDocumentData("TestData/CCD/2.xml", "101 Lindenwood Dr", "Suite 123", "Malvern", "PA", "78503", "US")]
        [XmlDocumentData("TestData/CCD/3.xml", "101 Lindenwood Dr", null, "Malvern", "PA", "78577", "US")]
        public void GetClientSetsAddress(HtmlDocument doc, string street1, string street2, string city, string state, string zip, string country)
        {
            var client = doc.GetClient();
            Assert.Equal(street1, client.Address.Street1);
            Assert.Equal(street2, client.Address.Street2);
            Assert.Equal(city, client.Address.City);
            Assert.Equal(state, client.Address.State);
            Assert.Equal(zip, client.Address.Zip);
            Assert.Equal(country, client.Address.Country);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", null, "UNK")]
        [XmlDocumentData("TestData/CCD/2.xml", "spa", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "spa", null)]
        public void GetClientSetsLanguage(HtmlDocument doc, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.Language.Value);
            Assert.Equal(nullFlavor, client.Language.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", null, "UNK")]
        [XmlDocumentData("TestData/CCD/2.xml", "2106-3", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "2106-3", null)]
        public void GetClientSetsRace(HtmlDocument doc, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.Race.Value);
            Assert.Equal(nullFlavor, client.Race.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", null, "UNK")]
        [XmlDocumentData("TestData/CCD/2.xml", "2135-2", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "2135-2", null)]
        public void GetClientSetsEthnicity(HtmlDocument doc, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.Ethnicity.Value);
            Assert.Equal(nullFlavor, client.Ethnicity.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", null, null)]
        [XmlDocumentData("TestData/CCD/2.xml", "tel:+1-6105555567", "HP")]
        [XmlDocumentData("TestData/CCD/3.xml", "tel:+1-6105555555", "WP")]
        public void GetClientSetsPhone(HtmlDocument doc, string expected, string type)
        {
            var client = doc.GetClient();
            Assert.Equal(expected, client.Phone.Number);
            Assert.Equal(type, client.Phone.Type);
        }
    }
}
