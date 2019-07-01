namespace CqmSolution.Models.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string ToNullIfEmpty(this string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text;
            return null;
        }
    }
}
