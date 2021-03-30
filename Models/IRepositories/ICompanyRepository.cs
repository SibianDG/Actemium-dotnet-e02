using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Models
{
    public interface ICompanyRepository
    {
        ActemiumCompany GetBy(int id);

        IEnumerable<ActemiumCompany> GetAll();
    }
}
