using Microsoft.AspNetCore.Mvc;
using Tnf.CarShop.Host.Constants;

namespace Tnf.CarShop.Host.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route(Routes.Dealer)]
internal class DealerController : TnfController
{
}
