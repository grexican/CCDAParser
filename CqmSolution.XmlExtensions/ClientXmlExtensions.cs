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
            var client = new Client
            {
                ClientIdentifier = patientRole.SelectSingleNode("id")?.Attributes["extension"]?.Value,
                FirstName = patientRole.SelectSingleNode("patient/name/given")?.InnerText,
                LastName = patientRole.SelectSingleNode("patient/name/family")?.InnerText,
                DateOfBirth = patientRole.SelectSingleNode("patient/birthtime").GetDate(),
                Gender = patientRole.SelectSingleNode("patient/administrativegendercode").GetCode(),
                Address = patientRole.SelectSingleNode("addr").GetAddress(),
                Phone = patientRole.SelectSingleNode("telecom").GetPhone(),
                Language = patientRole.SelectSingleNode("patient/languagecommunication/languagecode").GetCode(),
                Ethnicity = patientRole.SelectSingleNode("patient/ethnicgroupcode").GetCode(),
                Race = patientRole.SelectSingleNode("patient/racecode").GetCode()
            };

            return client;
        }
    }
}
