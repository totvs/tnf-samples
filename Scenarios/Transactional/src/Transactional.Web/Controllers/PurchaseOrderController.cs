using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Transactional.Domain.Entities;
using Transactional.Domain.Interfaces;

namespace Transactional.Web.Controllers
{
    /// <summary>
    /// Purchase order API
    /// </summary>
    [Route("api/purchaseorder")]
    public class PurchaseOrderController : TnfController
    {
        private readonly IPurchaseOrderService purchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            this.purchaseOrderService = purchaseOrderService;
        }

        /// <summary>
        /// Get all purchase orders
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<PurchaseOrder>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult GetAll()
        {
            var response = purchaseOrderService.GetAllPurchaseOrders();

            return CreateResponseOnGetAll(response);
        }

        /// <summary>
        /// Create a pre defined purchase order for test
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseOrder), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post()
        {
            var purchaseOrder = new PurchaseOrder()
            {
                ClientId = 1,
                Data = DateTime.UtcNow.Date,
                Discount = 0,
                Tax = 10,
                BaseValue = 100,
                TotalValue = 110,
            };

            purchaseOrder.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
            {
                ProductId = 1,
                Amount = 2,
                UnitValue = 50.0m
            });

            purchaseOrder.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
            {
                ProductId = 2,
                Amount = 5,
                UnitValue = 10.0m
            });

            purchaseOrder = await purchaseOrderService.CreateNewPurchaseOrder(purchaseOrder);

            return CreateResponseOnPost(purchaseOrder);
        }

        /// <summary>
        /// Delete purchase order
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            await purchaseOrderService.DeleteAsync(id);

            return CreateResponseOnDelete();
        }
    }
}
