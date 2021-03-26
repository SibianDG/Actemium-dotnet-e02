namespace _2021_dotnet_e_02.Models
{
    public class ActemiumContract
    {
        public int ContractId { get; set; }
        //ActemiumContractType contractType
        public string ContractTypeName { get; set; }
        //ActemiumCustomer customer
        public string Status { get; set; }
        
        //Todo change to Date in java
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public ActemiumContract()
        {
            
        }
    }
}