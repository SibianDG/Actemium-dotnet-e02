using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    public interface IContractRepository
    {
        ActemiumContract GetBy(int id);
        
        IEnumerable<ActemiumContract> GetAll();
        IEnumerable<ActemiumContract> GetAll(ActemiumCompany company);

        void Add(ActemiumContract contract);

        void Update(ActemiumContract contract);
        void SaveChanges();
    }
}