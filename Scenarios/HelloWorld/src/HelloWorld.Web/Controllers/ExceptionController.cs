using Microsoft.AspNetCore.Mvc;
using System;
using Tnf.AspNetCore.Mvc.Response;

namespace HelloWorld.Web.Controllers
{
    [Route("api/exception")]
    public class ExceptionController : TnfController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult Get()
        {
            throw new Exception("Exception simulation");
        }
    }
}
