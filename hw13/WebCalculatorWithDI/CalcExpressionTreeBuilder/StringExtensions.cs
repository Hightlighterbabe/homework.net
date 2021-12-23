using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace WebCalculatorWithDI.CalcExpressionTreeBuilder
{
    public static class StringExtensions
    {
        public static bool TrySplitMathExpressionByOperationsAndNumbers(this string str, List<string> elements)
        {
            var strWithoutSpaces = str.Replace(" ", "")
                .ToLower()
                .Replace("%2b", "+");

            var startCurrentNumber = -1;
            for (var i = 0; i < strWithoutSpaces.Length; i++)
            {
                if (char.IsLetter(strWithoutSpaces[i])) return false;
                if (char.IsDigit(strWithoutSpaces[i]) || strWithoutSpaces[i] == '.')
                {
                    if (startCurrentNumber == -1) startCurrentNumber = i;
                    continue;
                }

                if (startCurrentNumber != -1)
                {
                    var isValid = TryAddNumberToList(elements, strWithoutSpaces[startCurrentNumber..i]);
                    if (!isValid) return false;
                }

                elements.Add(strWithoutSpaces[i].ToString());
                startCurrentNumber = -1;
            }

            if (startCurrentNumber == -1) return true;

            var isValidLast = TryAddNumberToList(elements, strWithoutSpaces[startCurrentNumber..]);

            return isValidLast;
        }

        private static bool TryAddNumberToList(ICollection<string> elements, string str)
        {
            var isNumber = decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var number);
            if (!isNumber) return false;
            elements.Add(number.ToString(CultureInfo.InvariantCulture));
            return true;
        }

        public static bool IsValidPlacementBrackets(this string str)
        {
            if (string.IsNullOrEmpty(str)) return true;
            var balance = 0;
            foreach (var e in str)
            {
                if (balance < 0) return false;

                switch (e)
                {
                    case '(': balance++; break;
                    case ')': balance--; break;
                }
            }

            return balance == 0;
        }
    }
}
