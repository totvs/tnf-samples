using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.AspNetCore.Mvc.Controllers;

namespace Acme.SimpleTaskApp.Web.Controllers
{
//    public class ErrorController : TnfController
//    {
//        private readonly IErrorInfoBuilder _errorInfoBuilder;

//        public ErrorController(IErrorInfoBuilder errorInfoBuilder)
//        {
//            _errorInfoBuilder = errorInfoBuilder;
//        }

//        public ActionResult Index()
//        {
//            var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

//            var exception = exHandlerFeature != null
//                                ? exHandlerFeature.Error
//                                : new Exception("Unhandled exception!");

//            return View(
//                "Error",
//                new ErrorViewModel(
//                    _errorInfoBuilder.BuildForException(exception),
//                    exception
//                )
//            );
//        }
//    }
}