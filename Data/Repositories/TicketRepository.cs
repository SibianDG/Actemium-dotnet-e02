using _2021_dotnet_e_02.Models;
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
            return _tickets.ToList();
        }

        public ActemiumTicket GetBy(int id)
        {
            //return _tickets.Include(t => t.Comments).Include(t => t.Company).Include(t => t.Technicians).Include(t => t.TicketChanges).SingleOrDefault(t => t.TicketId == id);
            return _tickets.SingleOrDefault(t => t.TicketId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
