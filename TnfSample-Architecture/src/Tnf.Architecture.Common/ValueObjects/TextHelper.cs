using System.Globalization;
using System.Linq;

namespace Tnf.Architecture.Common.ValueObjects
{
    public static class TextHelper
    {
        private const string WithAccents = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç'`´^~";
        private const string WithoutAccents = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc     ";
        private const string Allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmonopqrstuvwxyz0123456789-_";

        public static string RemoveAccents(string text)
        {
            if (text == null)
                return string.Empty;

            for (var i = 0; i < WithAccents.Length; i++)
                text = text.Replace(WithAccents[i].ToString(), WithoutAccents[i].ToString());

            return text;
        }

        public static string FormatTextByUrl(string text)
        {
            text = RemoveAccents(text);

            var returnText = text.Replace(" ", "");

            for (var i = 0; i < text.Length; i++)
            {
                if (!Allowed.Contains(text.Substring(i, 1)))
                    returnText = returnText.Replace(text.Substring(i, 1), "");
            }

            return returnText.ToLower();
        }

        public static string GetNumbers(string text)
            => string.IsNullOrEmpty(text) ? "" : new string(text.Where(char.IsDigit).ToArray());

        public static string AjustText(string value, int length)
            => (value.Length > length) ? value.Substring(0, length) : value;

        public static string ToTitleCase(string text)
            => ToTitleCase(text, false);

        public static string ToTitleCase(string text, bool keepUpperWords)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.Trim();

            if (!keepUpperWords)
                text = text.ToLower();
            
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }

        public static string ToTitleCase(this TextInfo info, string str)
        {
            var auxStr = str.ToLower();
            var auxArr = auxStr.Split(' ');
            var result = "";
            var firstWord = true;

            foreach (var word in auxArr)
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