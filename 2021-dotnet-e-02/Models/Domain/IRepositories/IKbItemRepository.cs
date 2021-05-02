using System.Collections.Generic;

namespace _2021_dotnet_e_02.Models
{
    public interface IKbItemRepository
    {
        ActemiumKbItem GetBy(int id);
        IEnumerable<ActemiumKbItem> GetAll();
        IEnumerable<ActemiumKbItem> GetByType(string type);
        


    }
}