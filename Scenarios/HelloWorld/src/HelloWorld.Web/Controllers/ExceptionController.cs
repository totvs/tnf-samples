using Microsoft.AspNetCore.Mvc;
using System;
using Tnf.AspNetCore.Mvc.Response;

namespace HelloWorld.Web.Controllers
{
    /// <summary>
    /// Exception API
    /// </summary>
    [Route("api/exception")]
    public class ExceptionController : TnfController
    {
        /// <summary>
        /// Simulate throw exception in controller scope
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public IActionResult Get()
        {
            throw new Exception("Exception simulation");
        }
    }
}
