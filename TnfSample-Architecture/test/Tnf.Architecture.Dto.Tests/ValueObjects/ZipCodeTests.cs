using Tnf.Architecture.Common.ValueObjects;
using Xunit;

namespace Tnf.Architecture.Dto.Tests.ValueObjects
{
    public class ZipCodeTests
    {
        [Theory]
        [InlineData("99.999-999")]
        [InlineData("00099999999")]
        public void ClearZipCode_Test(string zipCode)
        {
            // Act
            var result = ZipCode.ClearZipCode(zipCode);

            // Assert
            Assert.Equal(result, "99999999");
        }
    }
}
