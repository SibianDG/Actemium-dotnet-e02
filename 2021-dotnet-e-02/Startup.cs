using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Data;
using _2021_dotnet_e_02.Data.Repositories;
using _2021_dotnet_e_02.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _2021_dotnet_e_02
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //MVC middleware
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
            
            services.AddSession();

            services.AddHttpContextAccessor();

            //Connection middleware
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("TicketSystemE02Context")));

            //Configure Identity framework
            //services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddDefaultIdentity<IdentityUser>(options => 
                options.SignIn.RequireConfirmedAccount = false
            ).AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;

                options.User.RequireUniqueEmail = true;
            });

            //configure authorization policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("SupportManager", policy => policy.RequireClaim(ClaimTypes.Role, "SupportManager"));
                options.AddPolicy("Customer", policy => policy.RequireClaim(ClaimTypes.Role, "Customer"));
            });

            //configure repositories
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IContractTypeRepository, ContractTypeRepository>();
            services.AddScoped<IKbItemRepository, KbItemRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //configure cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseSession();

            //Configure authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}