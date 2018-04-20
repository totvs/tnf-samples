using Microsoft.AspNetCore.Mvc;
using Dapper.Infra.Dto;
using Dapper.Infra.Repositories;
using System.Threading.Tasks;
using Tnf.Dto;
using System;
using Tnf.AspNetCore.Mvc.Response;
using Dapper.Infra.Entities;

namespace Dapper.Web
{
    [Route(WebConstants.PurchaseOrderRouteName)]
    public class PurchaseOrderController : TnfController
    {
        private readonly IPurchaseOrderRepository orderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IListDto<PurchaseOrderDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery]SumarizedPurchaseOrderRequestAllDto param)
        {
            if (param == null) return BadRequest();

            var response = await orderRepository.GetAllPurchaseOrders(param);

            return CreateResponseOnGet(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(int id, [FromQuery]RequestDto requestDto)
        {
            if (id <= 0) return BadRequest();

            var response = await orderRepository.GetPurchaseOrder(new DefaultRequestDto(id, requestDto));

            return CreateResponseOnGet(response.MapTo<PurchaseOrderDto>());
        }

        [HttpGet("{id}/customer")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetCustomerFromPurchaseOrder(int id)
        {
            if (id <= 0) return BadRequest();

            var response = await orderRepository.GetCustomerFromPurchaseOrder(id);

            return CreateResponseOnGet(response);
        }

        [HttpGet("sumarized")]
        [ProducesResponseType(typeof(SumarizedPurchaseOrder), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetSumarizedPurchaseOrderFromDate([FromQuery]DateTime date)
        {
            if (date == null) return BadRequest();

            var response = await orderRepository.GetSumarizedPurchaseOrderFromDate(date);

            return CreateResponseOnGet(response);
        }
    }
}
