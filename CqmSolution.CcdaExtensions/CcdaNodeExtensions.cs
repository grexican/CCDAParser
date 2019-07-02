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

        public static Code GetFirstNonNullValue(this POCD_MT000040Observation obs)
        {
            if (obs == null) return null;

            var value = (obs.value?.FirstOrDefault(v => v is CD) as CD)?.GetCode();

            if (!string.IsNullOrWhiteSpace(value?.Value)) return value;

            var childObs = obs.entryRelationship?.FirstOrDefault(r =>
                    r.Item is POCD_MT000040Observation)
                ?.Item as POCD_MT000040Observation;

            return GetFirstNonNullValue(childObs);
        }

        public static CqmSolutionDate GetDate(this TS ts)
        {
            if (ts == null) return null;

            return CqmSolutionDate.TryParse(ts.value, ts.nullFlavor);
        }

        public static CqmSolutionDateRange GetDateRange(this IVL_TS ivlts)
        {
            if (ivlts?.ItemsElementName == null || ivlts.Items == null || ivlts.ItemsElementName.Length != ivlts.Items.Length)
            {
                return null;
            }

            TS low = null;
            TS high = null;
            TS center = null;

            for (int i = 0; i < ivlts.ItemsElementName.Length; i++)
            {
                switch (ivlts.ItemsElementName[i])
                {
                    case ItemsChoiceType2.low:
                        low = ivlts.Items[i] as TS;
                        break;
                    case ItemsChoiceType2.high:
                        high = ivlts.Items[i] as TS;
                        break;
                    case ItemsChoiceType2.center:
                        center = ivlts.Items[i] as TS;
                        break;
                }
            }

            //Some of our sample data files have effectiveTime nodes with only a single value, not low & high values.
            //If that happens, it represents a Point In Time, so we take that to be both the low and the high value. //TODO: is that correct?
            return new CqmSolutionDateRange(low?.GetDate() ?? center?.GetDate(), high?.GetDate() ?? center?.GetDate());
        }

        public static ResultValue GetResultValue(this ANY[] any)
        {
            var val = any?.FirstOrDefault(a => a is CD || a is PQ || a is ST);

            if (val == null)
                return null;

            ValueType type = ValueType.ST; //default to string value
            Code code = null;
            string value = null;
            string unit = null;

            switch (val)
            {
                case CD cd:
                {
                    type = ValueType.CD;
                    code = GetCode(cd);
                    break;
                }
                case PQ pq:
                {
                    type = ValueType.PQ;
                    value = pq.value;
                    unit = pq.unit;
                    break;
                }
                case ST st:
                {
                    type = ValueType.ST;
                    value = st.Text.FirstOrDefault();
                    break;
                }
            }

            return new ResultValue(type, code, value, unit);
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
