using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models;
using Newtonsoft.Json;

namespace _2021_dotnet_e_02.Controllers
{
    public class KDBController : Controller
    {
        private readonly IKbItemRepository _kbItemRepository;

        public KDBController(IKbItemRepository kbItemRepository)
        {
            _kbItemRepository = kbItemRepository;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult OverviewType(string type)
        {
            IEnumerable<ActemiumKbItem> kbItems;
            kbItems = _kbItemRepository.GetByType(type);
            ViewData["type"] = type;
            return View(kbItems);
        }
        
        public IActionResult Details(int id)
        {
            ActemiumKbItem kbi = _kbItemRepository.GetBy(id);
            if (kbi == null)
                return NotFound();
            var json = JsonConvert.SerializeObject(kbi);
            return Json(json); 
        }
    }
}
