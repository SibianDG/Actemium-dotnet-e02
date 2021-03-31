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
        
        ActemiumCompany GetByName(string name);
        
        void Add(ActemiumCompany company);
        
        //Nothing is deleted in this application (See message of the client on Chamilo)
        //void Delete(ActemiumTicket ticket);
        void Update(ActemiumCompany company);
        void SaveChanges();

    }
}
