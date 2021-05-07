using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace _2021_dotnet_e_02.Data.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ActemiumContract> _contracts;
        public ContractRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _contracts = _context.ActemiumContracts;
        }

        public ActemiumContract GetBy(int id)
        {
            return _contracts.Include(c => c.Company).Include(c => c.ContractType).SingleOrDefault(c => c.ContractId.Equals(id));
        }

        public IEnumerable<ActemiumContract> GetAll()
        {
            return _contracts.AsNoTracking().Include(c => c.Company).Include(c => c.ContractType).ToList();
        }

        public IEnumerable<ActemiumContract> GetAll(ActemiumCompany company)
        {
            return _contracts.AsNoTracking()
                            .Where(c => c.Company == company)
                            .Include(c => c.Company)
                            .Include(c => c.ContractType)
                            .ToList();
        }

        public void Add(ActemiumContract contract)
        {
            _contracts.Add(contract);
        }

        public void Update(ActemiumContract contract)
        {
            _context.Update(contract);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
