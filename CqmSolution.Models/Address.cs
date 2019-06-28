using System.Collections.Generic;

namespace CqmSolution.Models
{
    public class Address : ValueObject
    {
        public string Type { get; protected set; }
        public string Street1 { get; protected set; }
        public string Street2 { get; protected set; }
        public string City { get; protected set; }
        public string State { get; protected set; }
        public string Zip { get; protected set; }
        public string Country { get; protected set; }

        public Address(string type, string street1, string street2, string city, string state, string zip, string country)
        {
            Type    = type?.Trim().ToNullIfEmpty();
            Street1 = street1?.Trim().ToNullIfEmpty();
            Street2 = street2?.Trim().ToNullIfEmpty();
            City    = city?.Trim().ToNullIfEmpty();
            State   = state?.Trim().ToNullIfEmpty();
            Zip     = zip?.Trim().ToNullIfEmpty();
            Country = country?.Trim().ToNullIfEmpty();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Street1;
            yield return Street2;
            yield return City;
            yield return State;
            yield return Zip;
            yield return Country;
        }
    }
}
