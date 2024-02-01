global using System.Text.RegularExpressions;

public static class PrimitiveExtensions
{
    // String
    public static string Truncate(this string value, int length) => (value != null && value.Length > length) ? value[..length] : value;

    public static string SubStr(this string str, int start, int end) => str.Length >= end - start ? str[start..end] : string.Empty;

    public static bool IsDigitsOnly(this string str)
    {
        foreach (char c in str)
        {
            if (!char.IsDigit(c))
                return false;
        }

        return true;
    }

    public static decimal ToDecimal(this string decimalString) => decimal.Parse(decimalString.Trim());

    public static bool HasContent(this string str) => !string.IsNullOrWhiteSpace(str);

    public static string CamelCaseToWords(this string s) => Regex.Replace(s, "(\\B[A-Z])", " $1");

    public static string Left(this string s, int length) => s.Length >= length ? s[..length] : s;
    public static string Right(this string sValue, int iMaxLength) => !sValue.IsNullOrEmpty() && sValue.Length > iMaxLength
            ? sValue.Substring(sValue.Length - iMaxLength, iMaxLength)
            : sValue ?? string.Empty;

    public static string AllTextBefore(this string s, char character) => string.Concat(s.TakeWhile(x => x != character)).Trim();
    public static string AllTextAfterAndIncluding(this string s, char character) => string.Concat(s.SkipWhile(x => x != character)).Trim();

    public static bool In(this string str, params string[] list) => str != null && list.Contains(str);

    public static int LevenshteinDistance(this string s, string t)
    {
        var (lengthS, lengthT) = (s.Length, t.Length);
        var distanceArray = new int[lengthS + 1, lengthT + 1];

        // If the strings are equal, return 0
        // If one string is empty, return the length of the other
        if (s == t || lengthS == 0 || lengthT == 0)
        {
            return Math.Abs(lengthS - lengthT);
        }

        // Initialize row 0 for S and column 0 for T
        Enumerable.Range(0, lengthS).ForEach(i => distanceArray[i, 0] = i);
        Enumerable.Range(0, lengthT).ForEach(j => distanceArray[0, j] = j);

        // for all i and j, d[i,j] will hold the Levenshtein distance between
        // the first i characters of s and the first j characters of t
        for (var i = 1; i <= lengthS; i++)
        {
            for (var j = 1; j <= lengthT; j++)
            {
                var substitutionCost = t[j - 1] == s[i - 1] ? 0 : 1;

                distanceArray[i, j] = Math.Min(
                    Math.Min(distanceArray[i - 1, j] + 1, distanceArray[i, j - 1] + 1),
                    distanceArray[i - 1, j - 1] + substitutionCost);
            }
        }

        return distanceArray[lengthS, lengthT];
    }

    // Decimal
    public static bool IsInteger(this decimal number) => number == Math.Truncate(number);
}
