using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CqmSolution.Models
{
    public class ResultValue : ValueObject
    {
        public ValueType Type { get; protected set; }
        public Code Code { get; protected set; }
        public string Unit { get; protected set; }
        public string Value { get; protected set; }
        public string DisplayName { get; protected set; }

        public ResultValue(ValueType type, Code code, string value, string unit, string displayName=null)
        {
            Type = type;
            Code = code;
            Value = value;
            Unit = unit;
            DisplayName = displayName;

            switch (Type)
            {
                case ValueType.PQ:
                {
                    if (string.IsNullOrWhiteSpace(Value))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value),
                            "Result Value is required when Type is PQ (PhysicalQuantity).");
                    }

                    if (string.IsNullOrWhiteSpace(Unit))
                    {
                        throw new ArgumentOutOfRangeException(nameof(unit),
                            "Result Unit is required when Type is PQ (PhysicalQuantity).");
                    }

                    break;
                }
                case ValueType.CD:
                {
                    if (code == null)
                    {
                        throw new ArgumentNullException(nameof(code),
                            "Result Code is required when Type is CD (Code).");
                    }

                    break;
                }
                case ValueType.ST:
                {
                    if (string.IsNullOrWhiteSpace(Value))
                    {
                        throw new ArgumentOutOfRangeException(nameof(value),
                            "Result Value is required when Type is ST (String).");
                    }

                    break;
                }
                default:
                {
                    throw new InvalidEnumArgumentException(nameof(type));
                }
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Code;
            yield return Unit;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case ValueType.CD:
                    return Code.Value;
                case ValueType.PQ:
                    return $"{Value} {Unit}";
                case ValueType.ST:
                    return Value;
                default:
                    throw new InvalidEnumArgumentException(nameof(Type), (int)Type, typeof(Type));
            }
        }
    }
}
