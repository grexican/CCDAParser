using System;
using System.Collections.Generic;

namespace CqmSolution.Models
{
    public class CqmSolutionDate : ValueObject
    {
        public DateTime? DateTime { get; }
        public string Value { get; }
        public string NullFlavor { get; protected set; }

        private CqmSolutionDate(string value, string nullFlavor)
        {
            Value      = value?.Trim();
            NullFlavor = nullFlavor; //TODO: NullFlavor validation?

            if(!Value.IsNullOrEmpty())
            {
                DateTime = Value.AdvancedParse() ?? throw new InvalidOperationException($"Invalid DateTime string: '{Value}'");
            }
        }

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static CqmSolutionDate TryParse(string value, string nullFlavor=null)
        {
            return string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(nullFlavor) ? null : new CqmSolutionDate(value, nullFlavor);
        }
    }
}
