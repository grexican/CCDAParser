using System.Linq;
using HtmlAgilityPack;
using Xunit;

namespace CqmSolution.XmlExtensions.Tests
{
    /// <summary>
    /// Note: The DiagnosisProblems section in test data file 9.xml is identical to that in 1.xml,
    /// so we are only running these tests on 1.xml through 8.xml.
    /// </summary>
    public class DiagnosisProblemXmlExtensionsTests
    {
        // This doesn't vary by test file, so no need to test all of them.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Client (1.xml)")]
        public void GetDiagnosisProblemsSetsClient(HtmlDocument doc, string testName)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(client, diagnosisProblem?.Client);
        }

        // The SectionType for DiagnosisProblem is always "PROBLEM", so there's no need to run this theory against all of our test data files.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "SectionType: Problem (1.xml)", "PROBLEM")]
        public void GetDiagnosisProblemsSetsSectionType(HtmlDocument doc, string testName, string expected)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.SectionType);
        }

        // The DataSubType for DiagnosisProblem is always "DIAG", so there's no need to run this theory against all of our test data files.
        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DataSubType: Diagnosis (1.xml)", "DIAG")]
        public void GetDiagnosisProblemsSetsDataSubType(HtmlDocument doc, string testName, string expected)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.DataSubType);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Count: 4 (1.xml)", 4)]
        [XmlDocumentData("TestData/CCD/2.xml", "Count: 7 (2.xml)", 7)]
        [XmlDocumentData("TestData/CCD/3.xml", "Count: 13 (3.xml)", 13)]
        [XmlDocumentData("TestData/CCD/4.xml", "Count: 1 (4.xml)", 1)]
        [XmlDocumentData("TestData/CCD/5.xml", "Count: 1 (5.xml)", 1)]
        [XmlDocumentData("TestData/CCD/6.xml", "Count: 1 (6.xml)", 1)]
        [XmlDocumentData("TestData/CCD/7.xml", "Count: 1 (7.xml)", 1)]
        [XmlDocumentData("TestData/CCD/8.xml", "Count: 1 (8.xml)", 1)]
        public void GetDiagnosisProblemsReturnsCorrectCount(HtmlDocument doc, string testName, int expected)
        {
            var client = doc.GetClient();

            var diagnosisProblems = doc.GetDiagnosisProblems(client);
            Assert.Equal(expected, diagnosisProblems?.Count);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Problem: Choroidal dystrophy (central areolar) (generalized) (peripap (1.xml)", "193468002")]
        [XmlDocumentData("TestData/CCD/2.xml", "Problem: CKD (chronic kidney disease), stage II (2.xml)", "431856006")]
        [XmlDocumentData("TestData/CCD/3.xml", "Problem: Acute kidney failure, unspecified (3.xml)", "14669001")]
        [XmlDocumentData("TestData/CCD/4.xml", "Problem: Below Knee Amputation Status (4.xml)", "V49.75")]
        [XmlDocumentData("TestData/CCD/5.xml", "Problem: Community Acquired Pneumonia (5.xml)", "385093006")]
        [XmlDocumentData("TestData/CCD/6.xml", "Problem: Acute pharyngitis (6.xml)", "363746003")]
        [XmlDocumentData("TestData/CCD/7.xml", "Problem (Not Present, aka None Known): Problem (7.xml)", "55607006")]
        [XmlDocumentData("TestData/CCD/8.xml", "Problem (Specific, Not Present): Diabetes Mellitus Type 2 (Disorder) (8.xml)", "44054006")]
        public void GetDiagnosisProblemsSetsProblemResult(HtmlDocument doc, string testName, string expected)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.ProblemResult?.ToString());
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DateLow: Oct 24 2018 (1.xml)", "20181024", null)]
        [XmlDocumentData("TestData/CCD/2.xml", "DateLow: null (2.xml)", null, "UNK")]
        [XmlDocumentData("TestData/CCD/3.xml", "DateLow: Oct 23 2018 (3.xml)", "20181023", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "DateLow: Apr 02 2014 (4.xml)", "20140402", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "DateLow: Feb 27 2018 (5.xml)", "20140227", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "DateLow: Apr 02 2014 (6.xml)", "20140402", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "DateLow: Jun 07 2013 16:05:06 GMT (7.xml)", "20130607160506", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "DateLow: Dec 01 2015 16:05:06 GMT (8.xml)", "20151201160506", null)]
        public void GetDiagnosisProblemsSetsDateLow(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.DateRange?.DateLow?.Value);
            Assert.Equal(nullFlavor, diagnosisProblem?.DateRange?.DateLow?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "DateHigh: null (1.xml)", null, "UNK")]
        [XmlDocumentData("TestData/CCD/2.xml", "DateHigh: null (2.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/3.xml", "DateHigh: Dec 23 2018 (3.xml)", "20181223", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "DateHigh: null (4.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/5.xml", "DateHigh: null (5.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/6.xml", "DateHigh: Apr 05 2014 23:59:59 EST (6.xml)", "20140405235959-0500", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "DateHigh: null (7.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/8.xml", "DateHigh: null (8.xml)", null, null)]
        public void GetDiagnosisProblemsSetsDateHigh(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.DateRange?.DateHigh?.Value);
            Assert.Equal(nullFlavor, diagnosisProblem?.DateRange?.DateHigh?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Status: active (1.xml)", "active", null)]
        [XmlDocumentData("TestData/CCD/2.xml", "Status: completed (2.xml)", "completed", null)]
        [XmlDocumentData("TestData/CCD/3.xml", "Status: completed (3.xml)", "completed", null)]
        [XmlDocumentData("TestData/CCD/4.xml", "Status: active (4.xml)", "active", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "Status: active (5.xml)", "active", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "Status: completed (6.xml)", "completed", null)]
        [XmlDocumentData("TestData/CCD/7.xml", "Status: active (7.xml)", "active", null)]
        [XmlDocumentData("TestData/CCD/8.xml", "Status: completed (8.xml)", "completed", null)]
        public void GetDiagnosisProblemsSetsStatus(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.Status?.Value);
            Assert.Equal(nullFlavor, diagnosisProblem?.Status?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "Severity: null (1.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/2.xml", "Severity: null (2.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/3.xml", "Severity: null (3.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/4.xml", "Severity: null (4.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/5.xml", "Severity: Moderate (5.xml)", "6736007", null)]
        [XmlDocumentData("TestData/CCD/6.xml", "Severity: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "Severity: null (7.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/8.xml", "Severity: null (8.xml)", null, null)]
        public void GetDiagnosisProblemsSetsSeverity(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.Severity?.Value);
            Assert.Equal(nullFlavor, diagnosisProblem?.Severity?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "TargetSite: null (1.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/2.xml", "TargetSite: null (2.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/3.xml", "TargetSite: null (3.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/4.xml", "TargetSite: Left (4.xml)", "7771000", null)]
        [XmlDocumentData("TestData/CCD/5.xml", "TargetSite: null (5.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/6.xml", "TargetSite: null (6.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/7.xml", "TargetSite: null (7.xml)", null, null)]
        [XmlDocumentData("TestData/CCD/8.xml", "TargetSite: null (8.xml)", null, null)]
        public void GetDiagnosisProblemsSetsTargetSite(HtmlDocument doc, string testName, string expected, string nullFlavor)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.TargetSite?.Value);
            Assert.Equal(nullFlavor, diagnosisProblem?.TargetSite?.NullFlavor);
        }

        [Theory]
        [XmlDocumentData("TestData/CCD/1.xml", "NotPresent: false (1.xml)", false)]
        [XmlDocumentData("TestData/CCD/2.xml", "NotPresent: false (2.xml)", false)]
        [XmlDocumentData("TestData/CCD/3.xml", "NotPresent: false (3.xml)", false)]
        [XmlDocumentData("TestData/CCD/4.xml", "NotPresent: false (4.xml)", false)]
        [XmlDocumentData("TestData/CCD/5.xml", "NotPresent: false (5.xml)", false)]
        [XmlDocumentData("TestData/CCD/6.xml", "NotPresent: false (6.xml)", false)]
        [XmlDocumentData("TestData/CCD/7.xml", "NotPresent: TRUE (7.xml)", true)]
        [XmlDocumentData("TestData/CCD/8.xml", "NotPresent: TRUE (8.xml)", true)]
        public void GetDiagnosisProblemsSetsNotPresent(HtmlDocument doc, string testName, bool expected)
        {
            var client = doc.GetClient();
            var diagnosisProblem = doc.GetDiagnosisProblems(client).FirstOrDefault();
            Assert.Equal(expected, diagnosisProblem?.NotPresent);
        }
    }
}
