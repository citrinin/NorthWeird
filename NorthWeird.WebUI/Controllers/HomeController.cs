using System;
using Microsoft.AspNetCore.Mvc;
using NorthWeird.Application.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NorthWeird.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestError()
        {
            throw new Exception("This is test exception");
        }
    }
}
