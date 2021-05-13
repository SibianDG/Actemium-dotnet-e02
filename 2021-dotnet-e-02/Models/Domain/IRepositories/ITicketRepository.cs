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
        IEnumerable<ActemiumTicket> GetAll(ActemiumCompany company);

        IEnumerable<ActemiumTicket> GetAllOpenTickets();
        IEnumerable<ActemiumTicket> GetAllOpenTickets(ActemiumCompany company);

        IEnumerable<ActemiumTicket> GetAllResolvedTickets();
        IEnumerable<ActemiumTicket> GetAllResolvedTickets(ActemiumCompany company);
        ActemiumTicket GetById(int id);

        int GetLastAddedId();
        
        

        void Add(ActemiumTicket ticket);
        
        //Nothing is deleted in this application (See message of the client on Chamilo)
        //void Delete(ActemiumTicket ticket);
        void Update(ActemiumTicket ticket);
        void SaveChanges();
    }
}
