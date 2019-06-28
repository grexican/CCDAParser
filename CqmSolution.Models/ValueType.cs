namespace CqmSolution.Models
{
    /// <summary>
    /// ValueType Samples:				
    /// (Sample below may not apply to all test results but is provided for insight on ValueType usage)				
    ///
    ///PQ(physical quantity)– The result value of a 'Length of gestation at birth (observable entity)' test can be '37 (weeks)'. '37' is a physical quantity.
    ///D009 will be '37' and D013 will be 'PQ'. D010 will be the unit.
    ///
    ///CD(code)– The result value for 'Length of gestation at birth (observable entity)' test can be 'Very low (qualifier value)' which is a SNOMED CT code 260362008. 				
    ///D011 should be populated with '260362008' (D005 will be NULL) and D013 will be 'CD'. D012 will be Code Systems and D013 will be 'CD'.				
	///			
    ///ST(string)– The result value for 'Length of gestation at birth (observable entity)' can be 'normal'. 				
    ///D009 will be 'normal' and D013 will be 'ST'.				
    /// </summary>
    public enum ValueType
    {
        PQ,
        CD,
        ST
    }
}
