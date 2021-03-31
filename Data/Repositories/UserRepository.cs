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
        private readonly DbSet<UserModel> _users;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _employees = _context.Employees;
            _customers = _context.Customers;
            _users = _context.Users;
        }

        public IEnumerable<ActemiumEmployee> GetAllTechnicians()
        {
            return _employees.AsNoTracking().ToList();
        }

        public IEnumerable<ActemiumCustomer> GetAllCustomers()
        {
            return _customers.AsNoTracking().ToList();
        }

        public UserModel GetBy(int id)
        {
            return _users.SingleOrDefault(u => u.UserId.Equals(id));
        }

        public UserModel GetByUsername(string username)
        {
            return _users.SingleOrDefault(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        public UserModel GetBy(string emailAddress)
        {
            return _users.SingleOrDefault(u => u.Email.Equals(emailAddress, StringComparison.CurrentCultureIgnoreCase));

        }

        public IEnumerable<ActemiumEmployee> GetAllEmployees()
        {
            return _employees.ToList();
        }
    }
}
