using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CqmSolution.Models
{
    public class Oid : ValueObject
    {
        public string Value { get; private set; }

        static readonly Regex Test = new Regex(@"^(\d+\.)+(\d+)$", RegexOptions.Compiled);

        public Oid(string value)
        {
            if (!IsValid(value)) throw new ArgumentException("Not a valid OID");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool IsValid(string value)
        {
            return Test.IsMatch(value);
        }

        public static Oid TryParse(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : new Oid(value);
        }
    }
}
