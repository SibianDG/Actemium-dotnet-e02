using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumKbItem
    {
        public int KbItemId { get; set; }
        public string Title { get; set; }
        public KbItemType Type { get; set; }
        public string Keywords { get; set; }
        public string Text { get; set; }

        public ActemiumKbItem()
        {

        }
    }
}
