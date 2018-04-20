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
    /// <summary>
    /// Purchase order API
    /// </summary>
    [Route("api/purchaseorder")]
    public class PurchaseOrderController : TnfController
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository orderRepository)
        {
            _purchaseOrderRepository = orderRepository;
        }

        /// <summary>
        /// Get a purchase order
        /// </summary>
        /// <param name="id">Purchase Order id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Purchase Order founded</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PurchaseOrder), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(int id, [FromQuery]RequestDto requestDto)
        {
            if (id <= 0)
                return BadRequest();

            if (requestDto == null)
                return BadRequest();

            var response = await _purchaseOrderRepository.GetPurchaseOrder(new DefaultRequestDto(id, requestDto));

            return CreateResponseOnGet(response);
        }

        /// <summary>
        /// Get a customer from purchase order
        /// </summary>
        /// <param name="orderId">Purchase Order id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Customer founded</returns>
        [HttpGet("{orderId}/customer")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetCustomerFromOrder(int orderId, [FromQuery]RequestDto requestDto)
        {
            if (orderId <= 0)
                return BadRequest();

            if (requestDto == null)
                return BadRequest();

            var response = await _purchaseOrderRepository.GetCustomerFromPurchaseOrder(new DefaultRequestDto(orderId, requestDto));

            return CreateResponseOnGet(response);
        }

        /// <summary>
        /// Get sumarized order
        /// </summary>
        /// <param name="date">Date to get sumarized order</param>
        /// <returns>Return a order sumarized from product</returns>
        [HttpPost("sumarized")]
        [ProducesResponseType(typeof(SumarizedPurchaseOrder), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetSumarizedOrderFromProduct([FromQuery]DateTime date)
        {
            if (date == null || date == DateTime.MinValue)
                return BadRequest();

            var response = await _purchaseOrderRepository.GetSumarizedPurchaseOrderFromDate(date);

            return CreateResponseOnGet(response);
        }
    }
}
