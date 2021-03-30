using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Models
{
    public interface ITicketRepository
    {
        ActemiumTicket GetBy(int id);

        IEnumerable<ActemiumTicket> GetAll();

        void SaveChanges();

    }
}
