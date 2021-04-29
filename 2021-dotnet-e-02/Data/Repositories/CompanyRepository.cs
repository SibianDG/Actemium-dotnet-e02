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
            return _companies.AsNoTracking().Include(c => c.ContactPersons).Include(c => c.Tickets).Include(c => c.Contracts).ToList();
            //return _companies.ToList();
        }

        public ActemiumCompany GetByName(string name)
        {
            return _companies.AsNoTracking().SingleOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Add(ActemiumCompany company)
        {
            _companies.Add(company);
        }

        public void Update(ActemiumCompany company)
        {
            _context.Update(company);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ActemiumCompany GetBy(int id)
        {
            return _companies.Include(c => c.ContactPersons).Include(c => c.Tickets).Include(c => c.Contracts).SingleOrDefault(c => c.CompanyId == id);
            //return _companies.Include(c => c.Tickets).SingleOrDefault(c => c.CompanyId == id);
        }
    }
}
