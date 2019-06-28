using System.Collections.Generic;

namespace CqmSolution.Models
{
    public class CqmSolutionDateRange : ValueObject
    {
        public CqmSolutionDate DateLow { get; }

        public CqmSolutionDate DateHigh { get; }

        public CqmSolutionDateRange(CqmSolutionDate dateLow, CqmSolutionDate dateHigh)
        {
            DateLow = dateLow;
            DateHigh = dateHigh;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DateLow;
            yield return DateHigh;
        }
    }
}
