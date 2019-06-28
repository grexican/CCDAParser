using System.Collections.Generic;
using HtmlAgilityPack;
using CqmSolution.Models;
using Xunit;

namespace CqmSolution.XmlExtensions.Tests
{
    public class StoredProcStagingDataPrepTests
    {
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Staging Data Inserts (1.xml)")]
        [XmlDocumentData("TestData/CCD/2.xml", "Staging Data Inserts (2.xml)")]
        [XmlDocumentData("TestData/CCD/3.xml", "Staging Data Inserts (3.xml)")]
        [XmlDocumentData("TestData/CCD/4.xml", "Staging Data Inserts (4.xml)")]
        [XmlDocumentData("TestData/CCD/5.xml", "Staging Data Inserts (5.xml)")]
        [XmlDocumentData("TestData/CCD/6.xml", "Staging Data Inserts (6.xml)")]
        [XmlDocumentData("TestData/CCD/7.xml", "Staging Data Inserts (7.xml)")]
        [XmlDocumentData("TestData/CCD/8.xml", "Staging Data Inserts (8.xml)")]
        [XmlDocumentData("TestData/CCD/9.xml", "Staging Data Inserts (9.xml)")]
        public void GenerateStagingTableInsertsHandlesAllEntities(HtmlDocument doc, string testName)
        {
            var client = doc.GetClient();
            var allergyAdverseEvents = doc.GetAllergyAdverseEvents(client);
            var diagnosisProblems = doc.GetDiagnosisProblems(client);
            var encounters = doc.GetEncounters(client);
            var medications = doc.GetMedications(client);

            var entities = new List<CqmSolutionEntity>();

            entities.Add(client);
            entities.AddRange(allergyAdverseEvents);
            entities.AddRange(diagnosisProblems);
            entities.AddRange(encounters);
            entities.AddRange(medications);

            var sql = StoredProcStagingDataPrep.GenerateStagingTableInserts(entities);
            Assert.False(string.IsNullOrWhiteSpace(sql));
        }
    }
}
