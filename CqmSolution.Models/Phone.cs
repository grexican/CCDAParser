using System.Collections.Generic;

namespace CqmSolution.Models
{
    public class Phone : ValueObject
    {
        public string Type { get; set; }
        public string Number { get; set; }

        public Phone(string type, string number)
        {
            Type   = type;
            Number = number;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Number;
        }
    }
}
