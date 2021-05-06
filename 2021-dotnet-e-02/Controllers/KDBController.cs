using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
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
        
        public IActionResult OverviewType(string? type, int? page, List<int> types)
        {
            page ??= 1;
            page = page == 0 ? 1 : page;

            if (types is {Count: > 0})
            {
                type ??= "";
                types.ForEach(x =>
                {
                    type += ", " + ((KbItemType) x).ToString();
                });
            }

            if (type != null && !type.Contains(",") && Enum.GetNames(typeof(KbItemType)).Contains(type, StringComparer.InvariantCultureIgnoreCase))
            {
                KbItemType t = (KbItemType) Enum.Parse(typeof(KbItemType), type.ToUpper());
                types.Add(Enum.GetValues(typeof(KbItemType)).Cast<KbItemType>()
                    .Select((x, ie) => new {item = x, index = ie}).Single(x => x.item == t).index);
            }

            Console.WriteLine("SSSS: "+type);
            
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
            ViewData["types"] = JsonConvert.SerializeObject(types);
            Console.WriteLine("Sending: "+JsonConvert.SerializeObject(types));
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
