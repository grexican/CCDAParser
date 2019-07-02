using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CqmSolution.CcdaExtensions.R2;
using CqmSolution.Models;
using CqmSolution.Models.Extensions;
using Xunit;

namespace CqmSolution.CcdaExtensions.Tests.R2
{
    public class StoredProcStagingDataPrepTests
    {
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Staging Data Inserts (1.xml)")]
        [XmlDocumentData("TestData/CCD/4.xml", "Staging Data Inserts (4.xml)")]
        [XmlDocumentData("TestData/CCD/5.xml", "Staging Data Inserts (5.xml)")]
        [XmlDocumentData("TestData/CCD/6.xml", "Staging Data Inserts (6.xml)")]
        [XmlDocumentData("TestData/CCD/7.xml", "Staging Data Inserts (7.xml)")]
        [XmlDocumentData("TestData/CCD/8.xml", "Staging Data Inserts (8.xml)")]
        [XmlDocumentData("TestData/CCD/9.xml", "Staging Data Inserts (9.xml)")]
        public void GenerateStagingTableInsertsHandlesAllEntities(string path, string testName)
        {
            var serializer = new XmlSerializer(typeof(POCD_MT000040ClinicalDocument));
            var reader = new StreamReader(path);

            var doc = (POCD_MT000040ClinicalDocument)serializer.Deserialize(reader);
            var client = doc.GetClient();
            var allergyAdverseEvents = doc.GetAllergyAdverseEvents(client);
            var diagnosisProblems = doc.GetDiagnosisProblems(client);
            var encounters = doc.GetEncounters(client);
            //TODO: var medications = doc.GetMedications(client);

            var entities = new List<CqmSolutionEntity>();

            entities.Add(client);
            entities.AddRange(allergyAdverseEvents);
            entities.AddRange(diagnosisProblems);
            entities.AddRange(encounters);
            //TODO: entities.AddRange(medications);

            var sql = entities.GenerateStagingTableInserts();
            Assert.False(string.IsNullOrWhiteSpace(sql));
        }
    }
}
