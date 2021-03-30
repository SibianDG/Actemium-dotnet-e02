using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ActemiumCompany> _companies;
        public CompanyRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _companies = _context.ActemiumCompanies;
        }

        public IEnumerable<ActemiumCompany> GetAll()
        {
            return _companies.Include(c => c.ContactPersons).Include(c => c.Tickets).Include(c => c.Contracts).ToList();
            //return _companies.ToList();
        }

        public ActemiumCompany GetBy(int id)
        {
            //return _companies.Include(c => c.ContactPersons).Include(c => c.Tickets).Include(c => c.Contracts).SingleOrDefault(c => c.CompanyId == id);
            return _companies.SingleOrDefault(c => c.CompanyId == id);
        }
    }
}
