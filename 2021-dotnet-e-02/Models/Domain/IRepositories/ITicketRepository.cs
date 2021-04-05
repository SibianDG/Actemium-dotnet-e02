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
        ActemiumTicket GetById(int id);

        void Add(ActemiumTicket ticket);
        
        //Nothing is deleted in this application (See message of the client on Chamilo)
        //void Delete(ActemiumTicket ticket);
        void Update(ActemiumTicket ticket);
        void SaveChanges();


    }
}
