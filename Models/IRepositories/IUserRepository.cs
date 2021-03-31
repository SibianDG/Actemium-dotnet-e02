using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Models
{
    public interface IUserRepository
    {
        UserModel GetBy(int id);
        UserModel GetByUsername(string username);
        UserModel GetBy(string emailAddress);
        IEnumerable<ActemiumEmployee> GetAllEmployees();
        IEnumerable<ActemiumEmployee> GetAllTechnicians();

        IEnumerable<ActemiumCustomer> GetAllCustomers();
        
        
    }
}
