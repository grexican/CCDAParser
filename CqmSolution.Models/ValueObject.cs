using System;
using System.Collections.Generic;
using System.Linq;

namespace CqmSolution.Models
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;
            return this.GetEqualityComponents().SequenceEqual<object>(((ValueObject)obj).GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return this.GetEqualityComponents().Aggregate<object, int>(1, (Func<int, object, int>)((current, obj) => current * 23 + (obj != null ? obj.GetHashCode() : 0)));
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if ((object)a == null && (object)b == null)
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return a.Equals((object)b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
