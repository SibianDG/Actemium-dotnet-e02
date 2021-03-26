using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumKbItem
    {
        public int KbItemId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Keywords { get; set; }
        public string Text { get; set; }

        public ActemiumKbItem()
        {

        }
    }
}
