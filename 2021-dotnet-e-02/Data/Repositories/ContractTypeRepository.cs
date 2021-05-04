using System.Collections.Generic;
using System.Linq;
using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;

namespace _2021_dotnet_e_02.Data.Repositories
{
    public class ContractTypeRepository : IContractTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ActemiumContractType> _contractTypes;
        public ContractTypeRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _contractTypes = dbContext.ActemiumContractTypes;
        }

        public ActemiumContractType GetBy(int id)
        {
            return _contractTypes.SingleOrDefault(ct => ct.ContractTypeId.Equals(id));
        }

        public IEnumerable<ActemiumContractType> GetAll()
        {
            return _contractTypes.AsNoTracking().ToList();
        }
    }
}