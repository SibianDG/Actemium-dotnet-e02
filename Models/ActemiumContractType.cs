namespace _2021_dotnet_e_02.Models
{
    public class ActemiumContractType
    {
        
        public int ContractTypeId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool HasEmail { get; set; }
        public bool HasPhone { get; set; }
        public bool HasApplication { get; set; }
        private string TimeStamp { get; set; }
        private int MaxHandlingTime { get; set; }
        private int MinThroughputTime { get; set; }
        private double Price { get; set; }
        
        public ActemiumContractType()
        {
            
        }
    }
}