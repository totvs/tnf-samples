using Microsoft.AspNetCore.Mvc;
using System;

namespace HelloWorld.Web.Controllers
{
    [Route("api/exception")]
    public class ExceptionController : TnfController
    {
        [HttpGet]
        public IActionResult Get()
        {
            throw new Exception("Exception simulation");
        }
    }
}
