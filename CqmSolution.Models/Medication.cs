namespace CqmSolution.Models
{
    public class Medication : CqmSolutionClientEntity
    {
        public Medication(Client client, string dataSubType) : base(client)
        {
            DataSubType = dataSubType;
        }

        public override string SectionType => "MEDICATION";

        /// <summary>
        /// Data Sub-Type:	
        /// Active: StatusCode = ACT
        /// Dispensed: StatusCode = DISP
        /// Not Ordered: StatusCode = ORDND
        /// Order: StatusCode = ORD
        ///
        /// The eleven data sub-types in D014 represent the following distinctions:
        /// * Active: Medication is being taken by the patient as of the reporting period.
        /// * Administered/Administered Not Done:  Clinician confirmed the medication actually was administered,
        ///   which is a more common data sub-type in Inpatient settings. If ‘Administered Not Done,’
        ///   there is typically an accompanying reason.
        /// * Adverse Effects: Refers to a condition that occurs when medications or biological substances are
        ///   administered and the condition is not considered an allergy.
        /// * Allergy: Refers to an immunologically-mediated reaction that exhibits specificity and recurs
        ///   on re-exposure to the offending drug. May include type 1 hypersensitivity reactions and other
        ///   allergy-like reactions, including pseudo-allergy.
        /// * Discharge/Discharge Not Done: Medications may be taken by or given to the patient after being
        ///   discharged from an inpatient encounter. If ‘Discharge Not Done,’ there is typically an accompanying reason.
        /// * Dispensed: A medication prescription has been filled by a pharmacy and the medication has been provided to
        ///   the patient or patient proxy. This is more common in ambulatory settings. 
        /// * Intolerance: A reaction in specific patients representing a low threshold to the normal pharmacological
        ///   action of a drug.Intolerance is a record of a clinical assessment of a propensity, or a potential risk
        ///   to an individual, to have a non-immune mediated adverse reaction on future exposure to the specified
        ///   substance, or class of substance. Intolerance is generally based on patient report and perception of
        ///   his or her ability to tolerate proper administration of a medication.
        /// * Order/Order Not Done: A medication can be recorded as ordered or not (whether as a prescription or within a facility).
        ///   If not ordered, there is typically an accompanying reason.
        /// </summary>
        public override string DataSubType { get; }

        public Code Product { get; set; }

        public Code Generic { get; set; }

        public CqmSolutionDateRange AdministeredDateRange { get; set; }

        public Code NegationRationale { get; set; }

        /// <summary>
        /// Note: Refills is the total number of fills.		
        /// Eg. 2 refills means first prescription with 1 additional prescription.
        /// </summary>
        public string Refills { get; set; }

        //TODO: If NotPresent is TRUE (from negationInd="true" on the observation node),
        // should we just ignore or filter out this Medication when we parse out ECQMs?
        public bool NotPresent { get; set; }
    }
}
