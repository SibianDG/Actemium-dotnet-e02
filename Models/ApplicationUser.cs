using Microsoft.AspNetCore.Identity;

namespace _2021_dotnet_e_02.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}