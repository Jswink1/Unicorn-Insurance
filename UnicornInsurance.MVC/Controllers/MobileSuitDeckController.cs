using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;

namespace UnicornInsurance.MVC.Controllers
{
    public class MobileSuitDeckController : Controller
    {
        private readonly IUserItemService _userItemService;

        public MobileSuitDeckController(IUserItemService userItemService)
        {
            _userItemService = userItemService;
        }

        public async Task<IActionResult> Index()
        {
            var userMobileSuits = await _userItemService.GetAllUserMobileSuits();

            return View(userMobileSuits);
        }
    }
}
