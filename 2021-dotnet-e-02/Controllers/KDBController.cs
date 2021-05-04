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
        
        public IActionResult OverviewType(string type, int? page)
        {
            page ??= 1;
            page = page == 0 ? 1 : page;
            
            IEnumerable<ActemiumKbItem> kbItems;
            kbItems = _kbItemRepository.GetByType(type);
            kbItems = kbItems.OrderBy(c => c.Title).ThenBy(c => c.KbItemId).ToList();
            
            int totalPages = kbItems.Count() / 10;
            if (kbItems.Count() % 10 != 0)
                totalPages++;
            ViewData["totalPages"] = totalPages;
            
            kbItems = kbItems.Skip((page.Value - 1) * 10).Take(10).ToList();
            ViewData["page"] = page;
            
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
