using System.Linq;
using CqmSolution.Models;

namespace CqmSolution.CcdaExtensions
{
    public static class CcdaNodeExtensions
    {
        public static Code GetCode(this CD cd)
        {
            if (cd == null) return null;

            if (string.IsNullOrWhiteSpace(cd.code))
            {
                var translation = cd.translation?.FirstOrDefault();
                if (translation != null)
                {
                    cd = translation;
                }
            }

            return new Code(Oid.TryParse(cd.codeSystem)
                , cd.codeSystemName
                , cd.code
                , cd.nullFlavor
                , cd.displayName
                , Oid.TryParse(cd.valueSet));
        }

        public static CqmSolutionDate GetDate(this TS ts)
        {
            if (ts == null) return null;

            return CqmSolutionDate.TryParse(ts.value, ts.nullFlavor);
        }

        public static CqmSolutionDateRange GetDateRange(this IVL_TS ivlts)
        {
            //TODO: figure out date range parsing
            //if (ivlts == null)
                return null;

                //[System.Xml.Serialization.XmlElementAttribute("center", typeof(TS))]
                //[System.Xml.Serialization.XmlElementAttribute("high", typeof(IVXB_TS))]
                //[System.Xml.Serialization.XmlElementAttribute("low", typeof(IVXB_TS))]
                //[System.Xml.Serialization.XmlElementAttribute("width", typeof(PQ))]
                //[System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]

            ////Some of our sample data files have effectiveTime nodes with only a single value, not low & high values.
            ////If that happens, it represents a Point In Time, so we take that to be both the low and the high value. //TODO: is that correct?
            //return new EcqmDateRange((ivlts.Items?.Where(t => t is IVXB_TS)?.FirstOrDefault() as IVXB_TS)?.GetDate(),
            //    effectiveTime.SelectSingleNode("low").GetDate() ?? effectiveTime.GetDate()
            //    , effectiveTime.SelectSingleNode("high").GetDate() ?? effectiveTime.GetDate());
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
