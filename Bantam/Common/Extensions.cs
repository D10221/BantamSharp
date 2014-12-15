using System.Text.RegularExpressions;

namespace Bantam.Common
{
    public static class Extensions
    {
        public static string Reglex(this string input, string pattern, string replacement, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            return new Regex(pattern, regexOptions).Replace(input, replacement);
        }
    }
}
