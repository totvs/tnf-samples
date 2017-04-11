using Tnf.Architecture.Application.Services;

namespace Tnf.Architecture.Web.Tests.App.Controllers
{
    public class WhiteHouseController : Tnf.Architecture.Web.Controllers.WhiteHouseController
    {
        public WhiteHouseController(IWhiteHouseAppService whiteHouseAppService) 
            : base(whiteHouseAppService)
        {
        }
    }
}
