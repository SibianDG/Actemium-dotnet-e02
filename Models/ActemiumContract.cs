using System;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumContract
    {
        public int ContractId { get; set; }
        public ActemiumContractType ContractType { get; set; }
        public ActemiumCompany Company { get; set; }
        public string Status { get; set; }
        
        //Todo change to Date in java => done
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ActemiumContract()
        {
            
        }
    }
}