using Microsoft.AspNetCore.Mvc;
using Querying.Infra.Dto;
using Querying.Infra.Entities;
using Querying.Infra.Repositories;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;

namespace Querying.Web
{
    [Route(WebConstants.PurchaseOrderRouteName)]
    public class PurchaseOrderController : TnfController
    {
        private readonly IPurchaseOrderRepository orderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseOrderDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(int id, [FromQuery]RequestDto requestDto)
        {
            if (id <= 0) return BadRequest();

            requestDto.WithId(id);

            var response = await orderRepository.GetPurchaseOrder(requestDto);

            return CreateResponseOnGetAll(response);
        }

        [HttpGet("{id}/customer")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetCustomerFromPurchaseOrder(int id, [FromQuery]RequestDto requestDto)
        {
            if (id <= 0) return BadRequest();

            requestDto.WithId(id);

            var response = await orderRepository.GetCustomerFromPurchaseOrder(requestDto);

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
