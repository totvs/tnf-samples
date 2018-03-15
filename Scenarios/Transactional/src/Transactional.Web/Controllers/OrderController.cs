using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Transactional.Domain.Entities;
using Transactional.Domain.Interfaces;

namespace Transactional.Web.Controllers
{
    [Route("api/order")]
    public class OrderController : TnfController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = orderService.GetAllOrders();

            return CreateResponseOnGetAll(response);
        }

        [HttpPost]
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

            order = await orderService.CreateNewOrder(order);

            return CreateResponseOnPost(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await orderService.DeleteAsync(id);

            return CreateResponseOnDelete();
        }
    }
}
