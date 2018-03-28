using Microsoft.AspNetCore.Mvc;
using Querying.Infra.Dto;
using Querying.Infra.Repositories;
using System.Threading.Tasks;
using Tnf.Dto;

namespace Querying.Web
{
    [Route("api/purchaseorder")]
    public class PurchaseOrderController : TnfController
    {
        private readonly IPurchaseOrderRepository orderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery]RequestDto requestDto)
        {
            if (id <= 0) return BadRequest();

            requestDto.WithId(id);

            var response = await orderRepository.GetPurchaseOrder(requestDto);

            return CreateResponseOnGetAll(response);
        }

        [HttpGet("{orderId}/customer")]
        public async Task<IActionResult> GetCustomerFromOrder(int orderId, [FromQuery]RequestDto requestDto)
        {
            if (orderId <= 0) return BadRequest();

            requestDto.WithId(orderId);

            var response = await orderRepository.GetCustomerFromPurchaseOrder(requestDto);

            return CreateResponseOnGet(response);
        }

        [HttpPost("sumarized")]
        public async Task<IActionResult> GetSumarizedOrderFromProduct([FromBody]SumarizedPurchaseOrderRequestAllDto param)
        {
            if (param == null) return BadRequest();

            var response = await orderRepository.GetSumarizedPurchaseOrderFromProduct(param);

            return CreateResponseOnGet(response);
        }
    }
}
