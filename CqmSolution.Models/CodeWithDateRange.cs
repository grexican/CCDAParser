using System.Collections.Generic;

namespace CqmSolution.Models
{
    public class CodeWithDateRange : ValueObject
    {
        public Code Code { get; protected set; }
        public CqmSolutionDateRange DateRange { get; protected set; }

        public CodeWithDateRange(Code code, CqmSolutionDateRange dateRange)
        {
            Code = code;
            DateRange = dateRange;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
            yield return DateRange;
        }
    }
}
