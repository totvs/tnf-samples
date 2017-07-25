using Tnf.Architecture.Common.Helpers;
using Xunit;

namespace Tnf.Architecture.Common.Tests.Helpers
{
    public class TextHelperTests
    {
        [Theory]
        [InlineData("téstê")]
        [InlineData("^^teste~~")]
        public void RemoveAccents_Test(string text)
        {
            // Act
            var result = TextHelper.RemoveAccents(text);

            // Assert
            Assert.Equal(result.Trim(), "teste");
        }

        [Theory]
        [InlineData("te.s/te")]
        [InlineData("téstê@ ")]
        public void FormatTextByUrl_Test(string text)
        {
            // Act
            var result = TextHelper.FormatTextByUrl(text);

            // Assert
            Assert.Equal(result, "teste");
        }

        [Theory]
        [InlineData("99.999-999")]
        [InlineData("a9b9c9d9e9f9g9h9i")]
        public void GetNumbers_Test(string text)
        {
            // Act
            var result = TextHelper.GetNumbers(text);

            // Assert
            Assert.Equal(result, "99999999");
        }

        [Theory]
        [InlineData("testes", 5)]
        [InlineData("testestestes", 5)]
        public void AjustText_Test(string value, int length)
        {
            // Act
            var result = TextHelper.AjustText(value, length);

            // Assert
            Assert.Equal(result, "teste");
        }

        [Theory]
        [InlineData("teste teste")]
        [InlineData("TESTE TESTE")]
        public void ToTitleCase_Test(string text)
        {
            // Act
            var result = TextHelper.ToTitleCase(text);

            // Assert
            Assert.Equal(result, "Teste Teste");
        }
    }
}
