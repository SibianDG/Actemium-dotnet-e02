using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumKbItem
    {
        public int KbItemId { get; set; }
        public string Title { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public KbItemType Type { get; set; }
        public string Keywords { get; set; }
        public string Text { get; set; }

        public ActemiumKbItem()
        {

        }
    }
}
