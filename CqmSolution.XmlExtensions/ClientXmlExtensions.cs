using HtmlAgilityPack;
using CqmSolution.Models;

namespace CqmSolution.XmlExtensions
{
    public static class ClientXmlExtensions
    {
        public static Client GetClient(this HtmlDocument cda)
        {
            return cda.DocumentNode.SelectSingleNode("/clinicaldocument/recordtarget/patientrole").GetClientFromPatientRole();
        }

        public static Client GetClientFromPatientRole(this HtmlNode patientRole)
        {
            var client = new Client();

            client.ClientIdentifier = patientRole.SelectSingleNode("id")?.Attributes["extension"]?.Value;
            client.FirstName        = patientRole.SelectSingleNode("patient/name/given")?.InnerText;
            client.LastName         = patientRole.SelectSingleNode("patient/name/family")?.InnerText;
            client.DateOfBirth      = patientRole.SelectSingleNode("patient/birthtime").GetDate();
            client.Gender           = patientRole.SelectSingleNode("patient/administrativegendercode").GetCode();
            client.Address          = patientRole.SelectSingleNode("addr").GetAddress();
            client.Phone            = patientRole.SelectSingleNode("telecom").GetPhone();
            client.Language         = patientRole.SelectSingleNode("patient/languagecommunication/languagecode").GetCode();
            client.Ethnicity        = patientRole.SelectSingleNode("patient/ethnicgroupcode").GetCode();
            client.Race             = patientRole.SelectSingleNode("patient/racecode").GetCode();

            return client;
        }
    }
}
