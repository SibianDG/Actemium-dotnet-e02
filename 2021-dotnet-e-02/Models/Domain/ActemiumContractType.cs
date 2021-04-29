using _2021_dotnet_e_02.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumContractType
    {
        
        public int ContractTypeId { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ContractTypeStatus Status { get; set; }
        public bool HasEmail { get; set; }
        public bool HasPhone { get; set; }
        public bool HasApplication { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Timestamp TimeStamp { get; set; }
        public int MaxHandlingTime { get; set; }
        public int MinThroughputTime { get; set; }
        public double Price { get; set; }
        
        public ActemiumContractType()
        {
            
        }
    }
}