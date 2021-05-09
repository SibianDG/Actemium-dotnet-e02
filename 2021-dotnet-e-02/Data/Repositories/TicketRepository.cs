using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ActemiumTicket> _tickets;

        public TicketRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _tickets = dbContext.ActemiumTickets;
        }

        public IEnumerable<ActemiumTicket> GetAll()
        {
            return _tickets.AsNoTracking()//.Include(t => t.Comments).ThenInclude(c => c.User)
                                          //.Include(t => t.Company)
                                          //.Include(t => t.TicketTechnicians)
                                          //.Include(t => t.TicketChanges).ThenInclude(c => c.User)
                                          .ToList();
        }
        public IEnumerable<ActemiumTicket> GetAll(ActemiumCompany actemiumCompany)
        {
            return _tickets.AsNoTracking().Where(t => t.Company == actemiumCompany)
                                          //.Include(t => t.Comments).ThenInclude(c => c.User)
                                          //.Include(t => t.Company)
                                          //.Include(t => t.TicketTechnicians)
                                          //.Include(t => t.TicketChanges).ThenInclude(c => c.User)
                                          .ToList();
        }

        // For full details new window
        public ActemiumTicket GetBy(int id)
        {
            return _tickets.Include(t => t.Comments).ThenInclude(c => c.User)
                          .Include(t => t.TicketTechnicians).ThenInclude(u => u.Technician)
                          .Include(t => t.TicketChanges).ThenInclude(c => c.User)
                          .Include(t => t.TicketChanges).ThenInclude(c => c.ChangeContents)
                          //TODO: fout met includes
                          .Include(t => t.Company)
                          .SingleOrDefault(t => id == t.TicketId);
        }

        // For right details
        public ActemiumTicket GetById(int id)
        {
            return _tickets
                           //.Include(t => t.Comments).ThenInclude(c => c.User)
                           //.Include(t => t.TicketTechnicians)
                           //.Include(t => t.TicketChanges).ThenInclude(c => c.User)
                           //.Include(t => t.TicketChanges).ThenInclude(c => c.ChangeContents)
                           //TODO: fout met includes
                           .Include(t => t.Company)
                           .SingleOrDefault(t => id == t.TicketId);
        }

        public void Add(ActemiumTicket ticket)
        {
            _tickets.Add(ticket);
        }

        public void Update(ActemiumTicket ticket)
        {
            _context.Update(ticket);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        public int GetLastAddedId()
        {
            return _tickets.AsNoTracking().OrderByDescending(t => t.TicketId).FirstOrDefault().TicketId;
        }

        public IEnumerable<ActemiumTicket> GetAllOpenTickets()
        {
            return _tickets.AsNoTracking()
                .Where(t => t.Status != TicketStatus.CANCELLED && t.Status != TicketStatus.COMPLETED)
                .OrderBy(t => t.Priority)
                .ThenBy(t => t.Status == TicketStatus.WAITING_ON_USER_INFORMATION)
                .ThenBy(t => t.DateAndTimeOfCreation)
                ;
        }

        public IEnumerable<ActemiumTicket> GetAllResolvedTickets()
        {
            return _tickets.AsNoTracking().Where(t => t.Status == TicketStatus.COMPLETED);
        }

        public IEnumerable<ActemiumTicket> GetAllOpenTickets(ActemiumCompany actemiumCompany)
        {
            return _tickets.AsNoTracking()
                .Where(t => t.Company == actemiumCompany)
                .Where(t => t.Status != TicketStatus.CANCELLED && t.Status != TicketStatus.COMPLETED)
                .OrderBy(t => t.Priority)
                .ThenBy(t => t.Status == TicketStatus.WAITING_ON_USER_INFORMATION)
                .ThenBy(t => t.DateAndTimeOfCreation)
                ;
        }

        public IEnumerable<ActemiumTicket> GetAllResolvedTickets(ActemiumCompany actemiumCompany)
        {
            return _tickets.AsNoTracking()
                .Where(t => t.Company == actemiumCompany)
                .Where(t => t.Status == TicketStatus.COMPLETED);
        }
    }
}
