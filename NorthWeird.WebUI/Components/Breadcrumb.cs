using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NorthWeird.WebUI.Components
{
    public class Breadcrumb : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewData["controllerName"] = this.ViewContext.RouteData.Values["controller"].ToString();
            ViewData["actionName"] = this.ViewContext.RouteData.Values["action"].ToString();
            return View();
        }
    }
}
