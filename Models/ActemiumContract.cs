﻿using System;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumContract
    {
        public int ContractId { get; set; }
        //ActemiumContractType contractType
        public string ContractTypeName { get; set; }
        //ActemiumCustomer customer
        public string Status { get; set; }
        
        //Todo change to Date in java => done
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ActemiumContract()
        {
            
        }
    }
}