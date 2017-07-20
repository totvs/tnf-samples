namespace Tnf.Architecture.Common.ValueObjects
{
    public class Address
    {
        public const int MaxAddressLength = 50;
        public const int MaxAddressNumberLength = 9;
        public const int MaxAddressComplementLength = 100;

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Complement { get; private set; }
        public ZipCode ZipCode { get; private set; }

        public Address(string street, string number, string complement, ZipCode zipCode)
        {
            SetStreet(street);
            SetNumber(number);
            SetComplement(complement);
            SetZipCode(zipCode);
        }

        private void SetStreet(string street) => Street = TextHelper.ToTitleCase(street);
        private void SetNumber(string number) => Number = TextHelper.ToTitleCase(number);
        private void SetComplement(string complement) => Complement = complement;
        private void SetZipCode(ZipCode zipCode) => ZipCode = zipCode;

        public override string ToString() => $"{Street}, {Number} - {Complement} - {ZipCode}";
    }
}