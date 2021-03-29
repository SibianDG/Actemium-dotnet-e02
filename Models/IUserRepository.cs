using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Models
{
    public interface IUserRepository
    {
        IEnumerable<ActemiumEmployee> GetAllEmployees();

        IEnumerable<ActemiumCustomer> GetAllCustomers();
    }
}
