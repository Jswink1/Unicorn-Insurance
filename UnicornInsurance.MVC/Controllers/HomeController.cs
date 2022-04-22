using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Constants;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.Services.Base;

namespace UnicornInsurance.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorVM = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is ApiException)
            {
                var apiError = (ApiException)exceptionHandlerPathFeature?.Error;

                if (apiError.StatusCode == 401)
                {
                    return RedirectToAction(nameof(NotAuthorized));
                }
                else
                {
                    errorVM.Error = JsonConvert.DeserializeObject<Error>(apiError.Response);
                }
            }
            else
            {
                errorVM.Error = new Error { ErrorType = "Failure", ErrorMessage = "Something went wrong" };
            }

            return View(errorVM);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Reviews()
        {
            return View();
        }
    }
}
