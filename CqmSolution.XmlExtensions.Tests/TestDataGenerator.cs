using System.Collections.Generic;
using CqmSolution.Models;

namespace CqmSolution.XmlExtensions.Tests
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetClients()
        {
            yield return new object[]
            {
                "All Non-Null",
                new Client
                {
                    ClientIdentifier = "12345",
                    LastName = "Smith",
                    FirstName = "John",
                    MiddleName = "Henry",
                    DateOfBirth = CqmSolutionDate.TryParse("19600718"),
                    Gender = new Code(null, null, "M", null, "Male"),
                    Address = new Address("H", "123 4th St.", "Apt. 5", "Philadelphia", "PA", "19103", "US"),
                    Phone = new Phone("W", "215-555-1212"),
                    Language = new Code(null, null, "spa", null, "Spanish"),
                    Race = new Code(null, null, "2106-3", null, "White"),
                    Ethnicity = new Code(null, null, "2135-2", null, "Hispanic or Latino"),
                    Religion = new Code(null, null, "chr", null, "Christian"),
                    PatientIdentifier = "Patient.Id.01",
                    PatientIdentifierRootId = Oid.TryParse("0.00.000.0.000000.0.0"),
                    PatientIdentifierNumber = "Patient.Id.Number.01"
                }
            };


            yield return new object[]
            {
                "Half Null",
                new Client
                {
                    ClientIdentifier = "12345",
                    LastName = null,
                    FirstName = "John",
                    MiddleName = null,
                    DateOfBirth = CqmSolutionDate.TryParse("19600718"),
                    Gender = new Code(null, null, 
                        null, null, 
                        "Male"),
                    Address = new Address(
                        null,
                        "123 4th St.",
                        null,
                        "Philadelphia",
                        null,
                        "19103",
                        null),
                    Phone = new Phone(
                        "W",
                        null),
                    Language = new Code(null, null, 
                        "spa", null, 
                        null),
                    Race = new Code(null, null, 
                        "2106-3", null, 
                        null),
                    Ethnicity = new Code(null, null, 
                        "2135-2", null, 
                        null),
                    Religion = new Code(null, null, 
                        "chr", null, 
                        null),
                    PatientIdentifier = "Patient.Id.01",
                    PatientIdentifierRootId = Oid.TryParse(null),
                    PatientIdentifierNumber = "Patient.Id.Number.01"
                }
            };

            yield return new object[]
            {
                "Other Half Null",
                new Client
                {
                    ClientIdentifier = null,
                    LastName = "Smith",
                    FirstName = null,
                    MiddleName = "Henry",
                    DateOfBirth = CqmSolutionDate.TryParse(null),
                    Gender = new Code(null, null, 
                        "M", null, 
                        null),
                    Address = new Address(
                        "H",
                        null,
                        "Apt. 5",
                        null,
                        "PA",
                        null,
                        "US"),
                    Phone = new Phone(
                        null,
                        "215-555-1212"),
                    Language = new Code(null, null,
                        null, null,
                        "Spanish"),
                    Race = new Code(null, null,
                        null, null,
                        "White"),
                    Ethnicity = new Code(null, null,
                        null, null,
                        "Hispanic or Latino"),
                    Religion = new Code(null, null,
                        null, null,
                        "Christian"),
                    PatientIdentifier = null,
                    PatientIdentifierRootId = Oid.TryParse("0.00.000.0.000000.0.0"),
                    PatientIdentifierNumber = null
                }
            };
        }

        public static IEnumerable<object[]> GetAllergyAdverseEvents()
        {
            yield return new object[]
            {
                "All Non-Null",
                new AllergyAdverseEvent(
                    new Client {ClientIdentifier = "12345"})
                {
                    Cause = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Cause.Code.01", null,
                        "Cause Display Name 01"),
                    DateRange = new CqmSolutionDateRange(
                        CqmSolutionDate.TryParse("20190101"),
                        CqmSolutionDate.TryParse("20190103")),
                    AllergyType = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        "Ones",
                        "My.Allergy.Code.01", null,
                        "Allergy Display Name 01")
                }
            };

            yield return new object[]
            {
                "Half Null",
                new AllergyAdverseEvent(
                    new Client {ClientIdentifier = null})
                {
                    Cause = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        null,
                        "My.Cause.Code.01", null,
                        null),
                    DateRange = new CqmSolutionDateRange(
                        CqmSolutionDate.TryParse("20190101"),
                        CqmSolutionDate.TryParse(null)),
                    AllergyType = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        null,
                        "My.Allergy.Code.01", null,
                        null)
                }
            };

            yield return new object[]
            {
                "Other Half Null",
                new AllergyAdverseEvent(
                    new Client { ClientIdentifier = "12345" })
                {
                    Cause = new Code(
                        Oid.TryParse(null),
                        "Zeroes",
                        null, null,
                        "Cause Display Name 01"),
                    DateRange = new CqmSolutionDateRange(
                        CqmSolutionDate.TryParse(null),
                        CqmSolutionDate.TryParse("20190103")),
                    AllergyType = new Code(
                        Oid.TryParse(null),
                        "Ones",
                        null, null,
                        "Allergy Display Name 01")
                }
            };
        }

        public static IEnumerable<object[]> GetDiagnosisProblems()
        {
            yield return new object[]
            {
                "All Non-Null",
                new DiagnosisProblem(new Client {ClientIdentifier = "12345"})
                {
                    ProblemResult = new ResultValue(
                        ValueType.CD,
                        new Code(
                            Oid.TryParse("0.00.000.0.000000.0.0"),
                            "Zeroes",
                            "My.Code.01", null,
                            "Code Display Name 01"),
                        null,
                        null),
                    Status = new Code(null, null, "completed", null, null),
                    DateRange = new CqmSolutionDateRange(
                        CqmSolutionDate.TryParse("20190101"),
                        CqmSolutionDate.TryParse("20190201")),
                    TargetSite = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        "Ones",
                        "My.Target.Site.Code.01", null,
                        "Target Site Display Name 01"),
                    Severity = new Code(
                        Oid.TryParse("2.22.222.2.222222.2.2"),
                        "Twos",
                        "My.Severity.Observation.Code.01", null,
                        "Severity Observation Display Name 01")
                }
            };

            yield return new object[]
            {
                "Half Null",
                new DiagnosisProblem(new Client {ClientIdentifier = "12345"})
                {
                    ProblemResult = new ResultValue(
                        ValueType.CD,
                        new Code(
                            Oid.TryParse(null),
                            "Zeroes",
                            null, null,
                            "Code Display Name 01"),
                        null,
                        null),
                    Status = new Code(null, null, null, null, null),
                    DateRange = new CqmSolutionDateRange(
                        CqmSolutionDate.TryParse("20190101"),
                        CqmSolutionDate.TryParse(null)),
                    TargetSite = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        null,
                        "My.Target.Site.Code.01", null,
                        null),
                    Severity = new Code(
                        Oid.TryParse("2.22.222.2.222222.2.2"),
                        null,
                        "My.Severity.Observation.Code.01", null,
                        null)
                }
            };

            yield return new object[]
            {
                "Other Half Null",
                new DiagnosisProblem(new Client {ClientIdentifier = "12345"})
                {
                    ProblemResult = new ResultValue(
                        ValueType.CD,
                        new Code(
                            Oid.TryParse("0.00.000.0.000000.0.0"),
                            null,
                            "My.Code.01", null,
                            null),
                        null,
                        null),
                    Status = new Code(null, null, "completed", null, null),
                    DateRange = new CqmSolutionDateRange(
                        CqmSolutionDate.TryParse(null),
                        CqmSolutionDate.TryParse("20190201")),
                    TargetSite = new Code(
                        Oid.TryParse(null),
                        "Ones",
                        null, null,
                        "Target Site Display Name 01"),
                    Severity = new Code(
                        Oid.TryParse(null),
                        "Twos",
                        null, null,
                        "Severity Observation Display Name 01")
                }
            };
        }

        public static IEnumerable<object[]> GetEncounters()
        {
            yield return new object[]
            {
                "Performed",
                new Encounter(
                    new Client { ClientIdentifier = "12345" },
                    "PRF")
                {
                    Code = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Code.01",
                        null,
                        "Code Display Name 01"),
                    VisitDateRange =
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190110"),
                            CqmSolutionDate.TryParse("20190113")),
                    FacilityLocation = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse("1.11.111.1.111111.1.1"),
                            "Ones",
                            "My.Facility.Location.Code.01",
                            null,
                            "Facility Location Display Name 01"),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190101"),
                            CqmSolutionDate.TryParse("20190131"))),
                    DischargeDisposition =
                        new Code(
                            Oid.TryParse("2.22.222.2.222222.2.2"),
                            null,
                            "My.Discharge.Disposition.Code.01",
                            null,
                            null),
                    PrincipalDiagnosis =
                        new Code(
                            Oid.TryParse("3.33.333.3.333333.3.3"),
                            "Threes",
                            "My.Principal.Diagnosis.Code.01",
                            null,
                            "Principal Diagnosis Display Name 01"),
                    EncounterDiagnosis = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse("4.44.444.4.444444.4.4"),
                            "Fours",
                            "My.Encounter.Diagnosis.Code.01",
                            null,
                            "Encounter Diagnosis Display Name 01"),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190111"),
                            CqmSolutionDate.TryParse("20190112")))
                }
            };

            yield return new object[]
            {
                "Ordered",
                new Encounter(
                    new Client { ClientIdentifier = "12345" },
                    "ORD")
                {
                    Code = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Code.01",
                        null,
                        "Code Display Name 01"),
                    VisitDateRange =
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190110"),
                            CqmSolutionDate.TryParse("20190113")),
                    FacilityLocation = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse("1.11.111.1.111111.1.1"),
                            "Ones",
                            "My.Facility.Location.Code.01",
                            null,
                            "Facility Location Display Name 01"),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse(null),
                            CqmSolutionDate.TryParse(null))),
                    DischargeDisposition =
                        new Code(
                            Oid.TryParse(null),
                            null,
                            null,
                            null,
                            null),
                    PrincipalDiagnosis =
                        new Code(
                            Oid.TryParse(null),
                            null,
                            null,
                            null,
                            null),
                    EncounterDiagnosis = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse(null),
                            null,
                            null,
                            null,
                            null),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse(null),
                            CqmSolutionDate.TryParse(null)))
                }
            };

            yield return new object[]
            {
                "Principal Diagnosis",
                new Encounter(
                    new Client { ClientIdentifier = "12345" },
                    "PRF")
                {
                    Code = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Code.01",
                        null,
                        "Code Display Name 01"),
                    VisitDateRange =
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190110"),
                            CqmSolutionDate.TryParse("20190113")),
                    FacilityLocation = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse("1.11.111.1.111111.1.1"),
                            "Ones",
                            "My.Facility.Location.Code.01",
                            null,
                            "Facility Location Display Name 01"),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190101"),
                            CqmSolutionDate.TryParse("20190131"))),
                    DischargeDisposition =
                        new Code(
                            Oid.TryParse("2.22.222.2.222222.2.2"),
                            null,
                            "My.Discharge.Disposition.Code.01",
                            null,
                            null),
                    PrincipalDiagnosis =
                        new Code(
                            Oid.TryParse("3.33.333.3.333333.3.3"),
                            "Threes",
                            "My.Principal.Diagnosis.Code.01",
                            null,
                            "Principal Diagnosis Display Name 01"),
                    EncounterDiagnosis = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse(null),
                            null,
                            null,
                            null,
                            null),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse(null),
                            CqmSolutionDate.TryParse(null)))
                }
            };

            yield return new object[]
            {
                "Encounter Diagnosis",
                new Encounter(
                    new Client { ClientIdentifier = "12345" },
                    "PRF")
                {
                    Code = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Code.01",
                        null,
                        "Code Display Name 01"),
                    VisitDateRange =
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190110"),
                            CqmSolutionDate.TryParse("20190113")),
                    FacilityLocation = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse("1.11.111.1.111111.1.1"),
                            "Ones",
                            "My.Facility.Location.Code.01",
                            null,
                            "Facility Location Display Name 01"),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190101"),
                            CqmSolutionDate.TryParse("20190131"))),
                    DischargeDisposition =
                        new Code(
                            Oid.TryParse("2.22.222.2.222222.2.2"),
                            null,
                            "My.Discharge.Disposition.Code.01",
                            null,
                            null),
                    PrincipalDiagnosis =
                        new Code(
                            Oid.TryParse(null),
                            null,
                            null,
                            null,
                            null),
                    EncounterDiagnosis = new CodeWithDateRange(
                        new Code(
                            Oid.TryParse("4.44.444.4.444444.4.4"),
                            "Fours",
                            "My.Encounter.Diagnosis.Code.01",
                            null,
                            "Encounter Diagnosis Display Name 01"),
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190111"),
                            CqmSolutionDate.TryParse("20190112")))
                }
            };
        }

        public static IEnumerable<object[]> GetMedications()
        {
            yield return new object[]
            {
                "Active",
                new Medication(
                    new Client {ClientIdentifier = "12345"},
                    "ACT")
                {
                    Product = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Product.Code.01",
                        null,
                        "My Product Display Name 01"),
                    Generic = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        "Ones",
                        "My.Generic.Code.01",
                        null,
                        "My Generic Display Name 01"),
                    AdministeredDateRange = 
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190110"),
                            CqmSolutionDate.TryParse("20190113")),
                    NegationRationale = null,
                    Refills = "2"
                }
            };

            yield return new object[]
            {
                "Ordered",
                new Medication(
                    new Client {ClientIdentifier = "12345"},
                    "ORD")
                {
                    Product = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Product.Code.01",
                        null,
                        "My Product Display Name 01"),
                    Generic = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        "Ones",
                        "My.Generic.Code.01",
                        null,
                        "My Generic Display Name 01"),
                    AdministeredDateRange = null,
                    NegationRationale = null,
                    Refills = "2"
                }
            };

            yield return new object[]
            {
                "Dispensed",
                new Medication(
                    new Client {ClientIdentifier = "12345"},
                    "DISP")
                {
                    Product = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Product.Code.01",
                        null,
                        "My Product Display Name 01"),
                    Generic = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        "Ones",
                        "My.Generic.Code.01",
                        null,
                        "My Generic Display Name 01"),
                    AdministeredDateRange =
                        new CqmSolutionDateRange(
                            CqmSolutionDate.TryParse("20190110"),
                            null),
                    NegationRationale = null,
                    Refills = "2"
                }
            };

            yield return new object[]
            {
                "Not Ordered",
                new Medication(
                    new Client {ClientIdentifier = "12345"},
                    "ORDND")
                {
                    Product = new Code(
                        Oid.TryParse("0.00.000.0.000000.0.0"),
                        "Zeroes",
                        "My.Product.Code.01",
                        null,
                        "My Product Display Name 01"),
                    Generic = new Code(
                        Oid.TryParse("1.11.111.1.111111.1.1"),
                        "Ones",
                        "My.Generic.Code.01",
                        null,
                        "My Generic Display Name 01"),
                    AdministeredDateRange = null,
                    NegationRationale = new Code(
                        Oid.TryParse("2.22.222.2.222222.2.2"),
                        "Twos",
                        "My.Negation.Reason.Code.01",
                        null,
                        "My Negation Reason Display Name 01"),
                    Refills = "0"
                }
            };
        }
    }
}
