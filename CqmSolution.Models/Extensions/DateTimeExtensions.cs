using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CqmSolution.Models
{
    public static class DateTimeExtensions
    {
        static Regex _reDateTimeOffset = new Regex(@"(\d{14})(\-|\+)(\d{2})(\d{2})", RegexOptions.Compiled);
        static readonly CultureInfo enUs = new CultureInfo("en-US");


        /// <summary>
        /// Combines a date with a time.
        /// </summary>
        /// <param name="d">The date to combine.</param>
        /// <param name="t">The time to combine. If null, then <paramref name="t"/> is ignored and <paramref name="d"/> is returned.</param>
        /// <returns>Returns a combined date and time.</returns>
        public static DateTime Combine(this DateTime d, TimeSpan? t)
        {
            return t == null ? d : d.Date.Add(t.Value);
        }

        /// <summary>
        /// Combines a date with a time.
        /// </summary>
        /// <param name="d">The date to combine.</param>
        /// <param name="t">The time to combine.</param>
        /// <returns>Returns a combined date and time.</returns>
        public static DateTime Combine(this DateTime d, TimeSpan t)
        {
            return Combine(d, (TimeSpan?)t);
        }

        public static bool IsValidSqlDateTime(this DateTime dateTime)
        {
            DateTime minValue = DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MinValue.ToString());
            DateTime maxValue = DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MaxValue.ToString());

            if (minValue > dateTime || maxValue < dateTime)
                return false;

            return true;
        }

        public static bool TryAdvancedParse(this string dt, out DateTime result)
        {
            result = DateTime.MinValue;
            if (dt.IsNullOrEmpty()) return false;

            var res = AdvancedParse(dt);

            if (res == null) return false;

            result = res.Value;
            return true;
        }

        public static DateTime? AdvancedParse(this string dt)
        {
            if (string.IsNullOrWhiteSpace(dt)) return null;

            if (_reDateTimeOffset.IsMatch(dt))
            {
                var match = _reDateTimeOffset.Match(dt);
                dt = $"{match.Groups[1]}{match.Groups[2]}{match.Groups[3]}:{match.Groups[4]}";
            }

            DateTime dtOut;
            DateTimeOffset dtOffset;

            if (DateTimeOffset.TryParse(dt, enUs, DateTimeStyles.None, out dtOffset))
                return dtOffset.DateTime;

            if (DateTimeOffset.TryParseExact(dt, "yyyyMMddHHmmsszzz", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out dtOffset))
                return dtOffset.DateTime;

            if (DateTime.TryParseExact(dt, "yyyyMMdd", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out dtOut))
                return dtOut;

            if (DateTime.TryParseExact(dt, "yyyyMMddHHmmss", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out dtOut))
                return dtOut;

            /////////////////
            // NJS 6/3/19 - Adding support for these next two formats, with minutes but no seconds,
            // to comply with the IVL_TS data type, which provides the following guidance:
            //
            // "When implementing IVL_TS, always express the boundaries at least up to a precision of a minute.
            // The end-of-day should be expressed as 2359 - theoretically this is one minute shorter than the
            // semantically correct expression - which is good enough in almost all use cases."
            //
            // http://wiki.hl7.org/index.php?title=Implementation_Guidance_for_the_IVL_TS_data_type
            //
            // Several instances of CCD sample data have included dates like this: "20141031150400-0500".
            //
            if (DateTimeOffset.TryParseExact(dt, "yyyyMMddHHmmzzz", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out dtOffset))
                return dtOffset.DateTime;
            //
            if (DateTime.TryParseExact(dt, "yyyyMMddHHmm", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out dtOut))
                return dtOut;
            /////////////////

            if (DateTime.TryParse(dt, enUs, DateTimeStyles.None, out dtOut))
                return dtOut;

            return null;
        }
    }
}
