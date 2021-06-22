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
        public int GetLatestNotificationNumber(string date = null)
        {
            try
            {
                Console.WriteLine(date);
                DateTime lastTime;
                if (date != null && date.ToLower() != "undefined")
                    lastTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                else
                    lastTime = _loginAttemptRepository
                        .GetLatestLoginAttemptBy(_userManager.GetUserName(User)).DateAndTime;
                Console.WriteLine();
                Console.WriteLine("lastTime "+lastTime);
                int number = _ticketRepository.GiveLatestUpdates(_userManager.GetUserName(User), lastTime);
                return number;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR MET NOTIFICATION");
                Console.WriteLine(e);
            }

            return 0;

        }
    }
}