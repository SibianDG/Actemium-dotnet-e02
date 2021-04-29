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
            _employees = dbContext.Employees;
            _customers = dbContext.Customers;
            _users = dbContext.Users;
        }

        public IEnumerable<ActemiumEmployee> GetAllTechnicians()
        {
            return _employees.AsNoTracking().ToList();
        }

        public IEnumerable<ActemiumCustomer> GetAllCustomers()
        {
            return _customers.AsNoTracking().Include(c => c.Company).ToList();
        }

        public UserModel GetBy(int id)
        {
            return _users.SingleOrDefault(u => u.UserId.Equals(id));
        }

        public UserModel GetByUsername(string username)
        {
            return _users.SingleOrDefault(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        /*public UserModel GetBy(string emailAddress)
        {
            return _users.SingleOrDefault(u => u.Email.Equals(emailAddress, StringComparison.CurrentCultureIgnoreCase));
        }*/

        public IEnumerable<ActemiumEmployee> GetAllEmployees()
        {
            return _employees.ToList();
        }

        public void Add(UserModel user)
        {
            _users.Add(user);
        }

        public void Update(UserModel user)
        {
            _context.Update(user);
        }

        public void AddCustomer(ActemiumCustomer customer)
        {
            _customers.Add(customer);
        }

        public void UpdateCustomer(ActemiumCustomer customer)
        {
            _context.Update(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

