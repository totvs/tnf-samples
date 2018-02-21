using Case6.Infra.Services;
using Microsoft.AspNetCore.Mvc;

namespace Case5.Web.Controllers
{
    [Route("api/customers")]
    public class CustomerController : TnfController
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = customerService.GetAllCustomers();

            return CreateResponseOnGetAll(response);
        }
    }
}
