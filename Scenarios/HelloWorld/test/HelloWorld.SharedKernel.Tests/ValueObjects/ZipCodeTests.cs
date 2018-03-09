using HelloWorld.SharedKernel.ValueObjects;
using Xunit;

namespace HelloWorld.SharedKernel.Tests.ValueObjects
{
    public class ZipCodeTests
    {
        [Theory]
        [InlineData("99.999-999")]
        [InlineData("00099999999")]
        public void ClearZipCode(string zipCode)
        {
            // Act
            var result = ZipCode.ClearZipCode(zipCode);

            // Assert
            Assert.Equal("99999999", result);
        }

        [Theory]
        [InlineData("99.999-999")]
        [InlineData("00099999999")]
        public void CreateZipCode(string zipCode)
        {
            // Act
            var result = new ZipCode(zipCode);

            // Assert
            Assert.Equal("99999999", result.Number);
        }

        [Theory]
        [InlineData("99.999-999")]
        [InlineData("00099999999")]
        public void IsValidZipCode(string zipCode)
        {
            // Arragen
            var result = new ZipCode(zipCode);

            // Assert
            Assert.True(result.IsValid());

            // Arragne
            ZipCode nullableZipCode = null;

            // Assert
            Assert.False(nullableZipCode.IsValid());
        }
    }
}
