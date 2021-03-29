using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ActemiumEmployee> _employees;
        private readonly DbSet<ActemiumCustomer> _customers;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _employees = _context.Employees;
            _customers = _context.Customers;
        }

        public IEnumerable<ActemiumCustomer> GetAllCustomers()
        {
            return _customers.ToList();
        }

        public IEnumerable<ActemiumEmployee> GetAllEmployees()
        {
            return _employees.ToList();
        }
    }
}
