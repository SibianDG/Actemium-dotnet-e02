using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace _2021_dotnet_e_02.Data.Repositories
{
    public class LoginAttemptRepository : ILoginAttemptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<LoginAttempt> _loginAttempts;

        public LoginAttemptRepository(ApplicationDbContext context)
        {
            _context = context;
            _loginAttempts = _context.LoginAttempts;
        }


        public LoginAttempt GetLatestLoginAttemptBy(string username)
        {
            return _loginAttempts
                .Where(l => l.UserModel.UserName.ToLower() == username.ToLower() && l.LoginStatus == LoginStatus.SUCCESS)
                .OrderByDescending(l => l.DateAndTime).FirstOrDefault();
        }
    }
}