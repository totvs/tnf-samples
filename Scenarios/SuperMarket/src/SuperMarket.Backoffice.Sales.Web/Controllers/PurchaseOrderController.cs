using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Web;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
    /// <summary>
    /// Purchase Order API
    /// </summary>
    [Route(WebConstants.PurchaseOrderRouteName)]
    public class PurchaseOrderController : TnfController
    {
        private readonly IPurchaseOrderAppService _appService;
        private const string name = "PurchaseOrder";

        public PurchaseOrderController(IPurchaseOrderAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get purchase orders
        /// </summary>
        /// <param name="requestAll">Request params to search purchase orders</param>
        /// <returns>Purchase order results</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<PurchaseOrderDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery]PurchaseOrderRequestAllDto requestAll)
        {
            var response = await _appService.GetAllPurchaseOrderAsync(requestAll);

            return CreateResponseOnGetAll(response, name);
        }

        /// <summary>
        /// Get purchase order
        /// </summary>
        /// <param name="id">Purchase order id</param>
        /// <param name="request">Purchase order</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto request)
        {
            var response = await _appService.GetPurchaseOrderAsync(new DefaultRequestDto(id, request));

            return CreateResponseOnGet(response, name);
        }

        /// <summary>
        /// Create a new Purchase Order
        /// </summary>
        /// <param name="purchaseOrder">Purchase order to create</param>
        /// <returns>Purchase Order created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]PurchaseOrderDto purchaseOrder)
        {
            var response = await _appService.CreatePurchaseOrderAsync(purchaseOrder);

            return CreateResponseOnPost(response, name);
        }

        /// <summary>
        /// Update a Purchase Order
        /// </summary>
        /// <param name="id">Purchase Order Id</param>
        /// <param name="purchaseOrder">Purchase order to update</param>
        /// <returns>Purchase Order updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody]PurchaseOrderDto purchaseOrder)
        {
            var response = await _appService.UpdatePurchaseOrderAsync(id, purchaseOrder);

            return CreateResponseOnPut(response, name);
        }
    }
}
