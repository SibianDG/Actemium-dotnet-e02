using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using _2021_dotnet_e_02.Models;
using System.Security.Claims;

namespace _2021_dotnet_e_02.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserRepository _userRepository;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager, 
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userRepository = userRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            //[EmailAddress]
            //public string Email { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // To avoid multiple dbContext error
            Boolean done = false;

            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {
                // Extra register IdentityUser stuff
                // Find username in javaUsers => then create IdentityUser with javaUser attributes
                var javaUser = _userRepository.GetByUsername(Input.UserName);
                if (javaUser != null)
                {
                    string javaEmail = "";
                    string javaRole;
                    if (javaUser is ActemiumEmployee)
                    {
                        javaEmail = ((ActemiumEmployee)javaUser).Email;
                        javaRole = ((ActemiumEmployee)javaUser).Role == Models.Enums.EmployeeRole.SUPPORT_MANAGER ? "SupportManager" : null;
                        Console.WriteLine(javaRole == null ? "only supportManagers can log in" : "tis ne supman");
                    }
                    else
                    {
                        javaEmail = ((ActemiumCustomer)javaUser).Email;
                        javaRole = "Customer";
                    }
                    var identityUserExists = await _userManager.FindByEmailAsync(javaEmail);

                    if (javaUser != null && javaRole != null && identityUserExists == null)
                    {
                        var user = new IdentityUser { UserName = javaUser.UserName, Email = javaEmail };
                        var resultCU = await _userManager.CreateAsync(user, javaUser.Password);
                        if (resultCU.Succeeded)
                        {
                            resultCU = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, javaRole));
                        }
                        if (resultCU.Succeeded)
                        {
                            Console.WriteLine("User creation success");
                            done = true;
                        }
                        else
                        {
                            Console.WriteLine("Errors: ");
                            Console.WriteLine(resultCU.Errors);
                        }
                    }
                    else
                    {
                        done = true;
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                // Normal login stuff
                if (done)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
