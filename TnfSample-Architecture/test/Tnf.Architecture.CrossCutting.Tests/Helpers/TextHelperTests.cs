using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tnf.Architecture.Dto.Tests.Helpers
{
    public class TextHelperTests
    {
        [Fact]
        public void Should_Resolve_Controller()
        {
            ServiceProvider.GetService<CountryController>().ShouldNotBeNull();
        }
    }
}
