using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Controllers
{
    public class KDBController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult OverviewType(string type)
        {
            ViewData["type"] = type;
            return View();
        }
    }
}
