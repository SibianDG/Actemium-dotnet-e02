using System;

namespace _2021_dotnet_e_02.Models
{
    public class ActemiumCustomer : UserModel
    {
        public ActemiumCompany Company { get; set; }
        public string Email { get; set; }

        public ActemiumCustomer()
        {
            
        }
    }
}