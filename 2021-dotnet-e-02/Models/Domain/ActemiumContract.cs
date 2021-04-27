using System;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumContract
    {
        public int ContractId { get; set; }
        public ActemiumContractType ContractType { get; set; }
        public ActemiumCompany Company { get; set; }
        public ContractStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ActemiumContract()
        {
            
        }
    }
}