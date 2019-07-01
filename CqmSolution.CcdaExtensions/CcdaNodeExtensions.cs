using System.Linq;
using CqmSolution.Models;

namespace CqmSolution.CcdaExtensions
{
    public static class CcdaNodeExtensions
    {
        public static CqmSolutionDate GetDate(this TS ts)
        {
            if (ts == null) return null;

            return CqmSolutionDate.TryParse(ts.value, ts.nullFlavor);
        }
        //
        public static Code GetCode(this CE ce)
        {
            if (ce == null) return null;

            if (string.IsNullOrWhiteSpace(ce.code))
            {
                var translation = ce.translation?.FirstOrDefault();
                if (translation != null)
                {
                    return new Code(Oid.TryParse(translation.codeSystem)
                        , translation.codeSystemName
                        , translation.code
                        , translation.nullFlavor
                        , translation.displayName
                        , Oid.TryParse(translation.valueSet));
                }
            }

            return new Code(Oid.TryParse(ce.codeSystem)
                , ce.codeSystemName
                , ce.code
                , ce.nullFlavor
                , ce.displayName
                , Oid.TryParse(ce.valueSet));
        }

        public static Address GetAddress(this AD ad)
        {
            if (ad == null)
                return null;

            var streets = ad.Items?.Where(a => a is adxpstreetAddressLine).ToList();

            return new Address(ad.use?.FirstOrDefault()
                , streets?.FirstOrDefault()?.Text?.FirstOrDefault()
                , streets?.Skip(1).FirstOrDefault()?.Text?.FirstOrDefault()
                , ad.Items?.FirstOrDefault(a => a is adxpcity)?.Text?.FirstOrDefault()
                , ad.Items?.FirstOrDefault(a => a is adxpstate)?.Text?.FirstOrDefault()
                , ad.Items?.FirstOrDefault(a => a is adxppostalCode)?.Text?.FirstOrDefault()
                , ad.Items?.FirstOrDefault(a => a is adxpcountry)?.Text?.FirstOrDefault());
        }

        public static Phone GetPhone(this TEL tel)
        {
            if (tel == null) return null;

            return new Phone(tel.use?.FirstOrDefault()
                , tel.value);
        }
    }
}
