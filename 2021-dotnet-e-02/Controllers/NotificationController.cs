using System;
using System.Globalization;
using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _2021_dotnet_e_02.Controllers
{
    public class NotificationController : Controller
    {
        private readonly ILoginAttemptRepository _loginAttemptRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public NotificationController(ILoginAttemptRepository loginAttemptRepository, ITicketRepository ticketRepository, IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            _loginAttemptRepository = loginAttemptRepository;
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public string GetLatestNotificationNumber(string date = null)
        {
            Console.WriteLine("STARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTART");
            if (date != null)
            {
                Console.WriteLine("GetLatestNotificationNumber");
                DateTime dateFromClient =
                    DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                Console.WriteLine(dateFromClient);
                DateTime lastTimeLogedIn = _loginAttemptRepository
                    .GetLatestLoginAttemptBy(_userManager.GetUserName(User)).DateAndTime;
            }

            return ""+66;
        }
    }
}