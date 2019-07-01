using System;
using System.Collections.Generic;
using CqmSolution.Models.Extensions;

namespace CqmSolution.Models
{
    public class Code : ValueObject
    {
        public Oid CodeSystem { get; protected set; }
        public string CodeSystemName { get; protected set; }
        public string Value { get; protected set; }
        public string DisplayName { get; protected set; }
        public string NullFlavor { get; protected set; }
        public Oid SdtcValueSet { get; protected set; }

        /// <summary>
        /// Standard codes
        /// </summary>
        /// <param name="codeSystem"></param>
        /// <param name="codeSystemName"></param>
        /// <param name="value"></param>
        /// <param name="nullFlavor"></param>
        /// <param name="displayName"></param>
        /// <param name="sdtcValueSet"></param>
        public Code(Oid codeSystem, string codeSystemName, string value, string nullFlavor, string displayName, Oid sdtcValueSet=null)
        {
            CodeSystem     = codeSystem; // ?? throw new ArgumentNullException(nameof(codeSystem));
            CodeSystemName = codeSystemName; // ?? throw new ArgumentNullException(nameof(codeSystem));
            Value          = value;
            NullFlavor     = nullFlavor;
            DisplayName    = displayName;
            SdtcValueSet   = sdtcValueSet;

            if(NullFlavor.IsNullOrEmpty())
            {
                //if(CodeSystem == null)
                //    throw new InvalidOperationException("Code System is required when not NullFlavor.");

                //TODO: had to comment this out for Encounters, many of which had codes with only a code system
                //if(Value.IsNullOrEmpty())
                //    throw new InvalidOperationException("Value is required when not NullFlavor.");
            }
            else
            {
                if(!Value.IsNullOrEmpty())
                    throw new InvalidOperationException("Value is not allowed when NullFlavor.");
            }
        }

        //public static class Factory
        //{
        //    public static Code CreateCode(Oid codeSystem, string value, string nullFlavor, string displayName)
        //    {
        //        return new Code(codeSystem, value, nullFlavor, displayName);
        //    }

        //    public static Code CreateCode(Oid codeSystem, string value, string displayName = null)
        //    {
        //        return new Code(codeSystem, value, null, displayName);
        //    }

        //    public static Code CreateNullCode()
        //    {
        //        return new Code(null, null, "UNK", "UNKNOWN");
        //    }

        //    public static Code CreateNullCode(string nullFlavor, string displayName = null)
        //    {
        //        return new Code(null, null, nullFlavor, displayName);
        //    }

        //    public static Code CreateNullCode(Oid codeSystem, string nullFlavor, string displayName = null)
        //    {
        //        return new Code(codeSystem, null, nullFlavor, displayName);
        //    }
        //}
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CodeSystem;
            yield return CodeSystemName;
            yield return Value;
            yield return DisplayName;
            yield return NullFlavor;
            yield return SdtcValueSet;
        }
    }
}
