using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    public interface IContractTypeRepository
    {
        ActemiumContractType GetBy(int id);
        
        IEnumerable<ActemiumContractType> GetAll();

        //No UC for CRUD?
    }
}