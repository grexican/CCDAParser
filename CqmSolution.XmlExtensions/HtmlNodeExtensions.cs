using System.Linq;
using HtmlAgilityPack;
using CqmSolution.Models;

namespace CqmSolution.XmlExtensions
{
    public static class HtmlNodeExtensions
    {
        public static Code GetCode(this HtmlNode node)
        {
            if(node == null) return null;

            if (node.Attributes["code"] == null)
            {
                var translation = node.SelectSingleNode("translation");
                if (translation != null)
                {
                    node = translation;
                }
            }

            return new Code(Oid.TryParse(node.Attributes["codesystem"]?.Value)
                , node.Attributes["codesystemname"]?.Value
                , node.Attributes["code"]?.Value
                , node.Attributes["nullflavor"]?.Value
                , node.Attributes["displayname"]?.Value
                , Oid.TryParse(node.Attributes["sdtc:valueset"]?.Value));
        }

        public static CqmSolutionDate GetDate(this HtmlNode node)
        {
            if (node == null) return null;

            return CqmSolutionDate.TryParse(node.Attributes["value"]?.Value
                , node.Attributes["nullflavor"]?.Value);
        }

        public static CqmSolutionDateRange GetDateRange(this HtmlNode node)
        {
            var effectiveTimes = node?.SelectNodes("effectivetime");

            if (effectiveTimes == null) return null;

            HtmlNode effectiveTime =
                (from effectiveTimeNode in effectiveTimes
                    let xsiType = effectiveTimeNode.Attributes["xsi:type"]?.Value
                    where xsiType == null || !xsiType.Contains("PIVL")  //Make sure we are NOT getting a Periodic Interval time a.k.a. Frequency
                    select effectiveTimeNode).FirstOrDefault();         //(i.e. "every 4 hours", like you would find on a Medication).

            if (effectiveTime == null) return null;

            //Some of our sample data files have effectiveTime nodes with only a single value, not low & high values.
            //If that happens, it represents a Point In Time, so we take that to be both the low and the high value. //TODO: is that correct?
            return new CqmSolutionDateRange(effectiveTime.SelectSingleNode("low").GetDate() ?? effectiveTime.GetDate()
                , effectiveTime.SelectSingleNode("high").GetDate() ?? effectiveTime.GetDate());
        }

        public static CodeWithDateRange GetCodeWithDateRange(this HtmlNode node)
        {
            if (node == null) return null;

            var code = GetCode(node);

            var dateRange = node.ParentNode?.GetDateRange();

            return new CodeWithDateRange(code, dateRange);
        }

        public static ResultValue GetResultValue(this HtmlNode node)
        {
            if (node == null) return null;

            ValueType type = ValueType.ST; //default to string value, if no type attribute

            var xsiType = node.Attributes["xsi:type"]?.Value;

            if (!string.IsNullOrWhiteSpace(xsiType))
            {
                if (System.Enum.TryParse(xsiType, out ValueType valueType))
                {
                    type = valueType;
                }
                else if (xsiType.Contains("PQ"))
                {
                    type = ValueType.PQ;
                }
            }
            //TODO: Should we have an else statement here, that tries to infer the type?  The logic could be:
            // First, try parsing out a code with GetCode; if that fails, see if it has a “value” attribute
            // (and possibly a unit), in which case it’s a PhysicalQuantity; finally, assume String and take the InnerHtml.

            Code code = null;
            string value = null;
            string unit = null;

            switch (type)
            {
                case ValueType.CD:
                {
                    code = GetCode(node);
                    break;
                }
                case ValueType.PQ:
                {
                    value = node.Attributes["value"]?.Value;
                    unit = node.Attributes["unit"]?.Value;
                    break;
                }
                case ValueType.ST:
                {
                    value = node.InnerHtml;
                    break;
                }
            }

            return new ResultValue(type, code, value, unit);
        }

        public static Address GetAddress(this HtmlNode node)
        {
            if (node == null) return null;

            var streets = node.SelectNodes("streetaddressline");

            return new Address(node.Attributes["use"]?.Value
                , streets.FirstOrDefault()?.InnerText
                , streets.Skip(1).FirstOrDefault()?.InnerText
                , node.SelectSingleNode("city")?.InnerText
                , node.SelectSingleNode("state")?.InnerText
                , node.SelectSingleNode("postalcode")?.InnerText
                , node.SelectSingleNode("country")?.InnerText);
        }

        public static Phone GetPhone(this HtmlNode node)
        {
            if (node == null) return null;

            return new Phone(node.Attributes["use"]?.Value
                , node.Attributes["value"]?.Value);
        }
    }
}
