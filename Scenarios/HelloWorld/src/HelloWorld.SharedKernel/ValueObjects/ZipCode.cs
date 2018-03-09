using System.Linq;

namespace HelloWorld.SharedKernel.ValueObjects
{
    public class ZipCode
    {
        public const int Length = 8;
        public string Number { get; private set; }

        public ZipCode(string number)
        {
            SetNumber(number);
        }

        public void SetNumber(string number)
        {
            Number = ClearZipCode(number);
        }

        public static string ClearZipCode(string zipCode)
        {
            zipCode = GetNumbers(zipCode);

            if (string.IsNullOrEmpty(zipCode))
                return string.Empty;

            while (zipCode.StartsWith("0"))
                zipCode = zipCode.Substring(1);

            return zipCode;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Number))
                return string.Empty;

            while (Number.Length < Length)
                Number = "0" + Number;

            return Number.Substring(0, 5) + "-" + Number.Substring(5);
        }

        public static string GetNumbers(string text)
            => string.IsNullOrEmpty(text) ? "" : new string(text.Where(char.IsDigit).ToArray());
    }

    public static class ZipCodeExtensions
    {
        public static bool IsValid(this ZipCode zipCode)
            => zipCode != null && 
            !string.IsNullOrWhiteSpace(zipCode.Number) && 
            zipCode.Number.Length == ZipCode.Length;

    }
}