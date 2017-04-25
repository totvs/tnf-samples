using System.Globalization;
using System.Linq;

namespace Tnf.Architecture.Dto.Helpers
{
    public static class TextHelper
    {
        const string withAccents = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç'`´^";
        const string withoutAccents = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc    ";
        const string allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmonopqrstuvwxyz0123456789-_";

        public static string RemoveAccents(string text)
        {
            if (text == null) return string.Empty;

            for (var i = 0; i < withAccents.Length; i++)
                text = text.Replace(withAccents[i].ToString(), withoutAccents[i].ToString());

            return text;
        }

        public static string FormatTextByUrl(string text)
        {
            text = RemoveAccents(text);

            var returnText = text.Replace(" ", "");

            for (var i = 0; i < text.Length; i++)
            {
                if (!allowed.Contains(text.Substring(i, 1)))
                {
                    returnText = returnText.Replace(text.Substring(i, 1), "");
                }
            }

            return returnText.ToLower();
        }

        public static string GetNumbers(string text)
        {
            return string.IsNullOrEmpty(text) ? "" : new string(text.Where(char.IsDigit).ToArray());
        }

        public static string AjustText(string value, int length)
        {
            if (value.Length > length)
            {
                value = value.Substring(0, length);
            }
            return value;
        }

        public static string ToTitleCase(string text)
        {
            return ToTitleCase(text, false);
        }

        public static string ToTitleCase(string text, bool keepUpperWords)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.Trim();

            if (!keepUpperWords)
                text = text.ToLower();

            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(text);
        }

        public static string ToTitleCase(this TextInfo info, string str)
        {
            string auxStr = str.ToLower();
            string[] auxArr = auxStr.Split(' ');
            string result = "";
            bool firstWord = true;
            foreach (string word in auxArr)
            {
                if (!firstWord)
                    result += " ";
                else
                    firstWord = false;

                result += word.Substring(0, 1).ToUpper() + word.Substring(1, word.Length - 1);
            }

            return result;
        }
    }
}