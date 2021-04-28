using System;
using _2021_dotnet_e_02.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumContract
    {
        public int ContractId { get; set; }
        public ActemiumContractType ContractType { get; set; }
        public ActemiumCompany Company { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ContractStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ActemiumContract()
        {
            
        }

        public ActemiumContract(ActemiumContractType contractType, ActemiumCompany company, DateTime startDate, DateTime endDate)
        {
            ContractType = contractType;
            Company = company;
            Status = ContractStatus.IN_REQUEST;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}