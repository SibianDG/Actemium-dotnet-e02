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
            //return _tickets.Include(t => t.Comments).Include(t => t.Company).Include(t => t.Technicians).Include(t => t.TicketChanges).ToList();
            return _tickets.AsNoTracking().Include(t => t.Comments).ThenInclude(c => c.User).ToList();
            //return _tickets.AsNoTracking().ToList();
        }

        public ActemiumTicket GetById(int id)
        {
            return _tickets.Include(t => t.Comments).ThenInclude(c => c.User)
                //TODO: fout met includes
                //.Include(t => t.Company)
                //.Include(t => t.Technicians)
                //.Include(t => t.Comments)
                //.Include(t => t.TicketChanges)
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

        public ActemiumTicket GetBy(int id)
        {
            return _tickets.AsNoTracking().Include(t => t.Comments).Include(t => t.Technicians).Include(t => t.TicketChanges).SingleOrDefault(t => t.TicketId == id);
            //return _tickets.Include(t => t.Comments).Include(t => t.Company).Include(t => t.Technicians).Include(t => t.TicketChanges).SingleOrDefault(t => t.TicketId == id);
            //return _tickets.AsNoTracking().SingleOrDefault(t => t.TicketId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<ActemiumTicket> GetAllOpenTickets()
        {
            return _tickets.AsNoTracking().Where(t => t.Status != TicketStatus.COMPLETED || t.Status != TicketStatus.CANCELLED);
        }

        public IEnumerable<ActemiumTicket> GetAllResolvedTickets()
        {
            return _tickets.AsNoTracking().Where(t => t.Status == TicketStatus.COMPLETED || t.Status == TicketStatus.CANCELLED);
        }
    }
}
