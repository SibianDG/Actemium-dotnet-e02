using System.Collections.Generic;
using System.Linq;
using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;

namespace _2021_dotnet_e_02.Data.Repositories
{
    public class KbItemRepository : IKbItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ActemiumKbItem> _kbItems;

        public KbItemRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _kbItems = dbContext.KbItems;
        }
        
        public ActemiumKbItem GetBy(int id)
        {
            return _kbItems.AsNoTracking().SingleOrDefault(kbi => kbi.KbItemId.Equals(id));
        }

        public IEnumerable<ActemiumKbItem> GetAll()
        {
            return _kbItems.AsNoTracking().ToList();
        }
    }
}