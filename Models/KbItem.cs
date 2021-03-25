using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Models
{
    public class KbItem
    {
        public long KBITEMID { get; set; }
        public string TITLE { get; set; }
        public KbItemType TYPE { get; set; }
        public string KEYWORDS { get; set; }
        public string TEXT { get; set; }

        public KbItem()
        {

        }
    }
}
