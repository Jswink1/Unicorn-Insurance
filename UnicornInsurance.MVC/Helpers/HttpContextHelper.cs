using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts.Helpers;

namespace UnicornInsurance.MVC.Helpers
{
    public class HttpContextHelper : IHttpContextHelper
    {
        public IFormFileCollection GetUploadedFiles(ControllerBase controller)
        {
            return controller.HttpContext.Request.Form.Files;
        }
    }
}
