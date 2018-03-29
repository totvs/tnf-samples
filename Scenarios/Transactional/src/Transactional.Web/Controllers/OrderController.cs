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
    /// Order API
    /// </summary>
    [Route("api/order")]
    public class OrderController : TnfController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Order>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult GetAll()
        {
            var response = _orderService.GetAllOrders();

            return CreateResponseOnGetAll(response);
        }

        /// <summary>
        /// Post pre defined order for test
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Order), 200)]
        public async Task<IActionResult> Post()
        {
            var order = new Order()
            {
                ClientId = 1,
                Data = DateTime.UtcNow.Date,
                Discount = 0,
                Tax = 10,
                BaseValue = 100,
                TotalValue = 110,
            };

            order.Products.Add(new ProductOrder()
            {
                ProductId = 1,
                Amount = 2,
                UnitValue = 50.0m
            });

            order.Products.Add(new ProductOrder()
            {
                ProductId = 2,
                Amount = 5,
                UnitValue = 10.0m
            });

            order = await _orderService.CreateNewOrder(order);

            return CreateResponseOnPost(order);
        }

        /// <summary>
        /// Delete order by id
        /// </summary>
        /// <param name="id">Order id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            await _orderService.DeleteAsync(id);

            return CreateResponseOnDelete();
        }
    }
}
