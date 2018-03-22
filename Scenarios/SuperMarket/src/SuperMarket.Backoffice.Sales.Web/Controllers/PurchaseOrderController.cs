using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Web;
using System;
using System.Threading.Tasks;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
    [Route(WebConstants.PurchaseOrderRouteName)]
    public class PurchaseOrderController : TnfController
    {
        private readonly IPurchaseOrderAppService _appService;
        private const string name = "PurchaseOrder";

        public PurchaseOrderController(IPurchaseOrderAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]PurchaseOrderRequestAllDto requestAll)
        {
            var response = await _appService.GetAllPurchaseOrderAsync(requestAll);

            return CreateResponseOnGetAll(response, name);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> request)
        {
            var response = await _appService.GetPurchaseOrderAsync(request.WithId(id));

            return CreateResponseOnGet<PurchaseOrderDto, Guid>(response, name);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PurchaseOrderDto purchaseOrder)
        {
            var response = await _appService.CreatePurchaseOrderAsync(purchaseOrder);

            return CreateResponseOnPost<PurchaseOrderDto, Guid>(response, name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]PurchaseOrderDto purchaseOrder)
        {
            var response = await _appService.UpdatePurchaseOrderAsync(id, purchaseOrder);

            return CreateResponseOnPut<PurchaseOrderDto, Guid>(response, name);
        }
    }
}
