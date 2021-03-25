using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _2021_dotnet_e_02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("START");
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var list = db.KbItems.FromSqlRaw("SELECT TEXT FROM dbo.ACTEMIUMKBITEM").ToList();
                    foreach (var l in list)
                    {
                        Console.WriteLine(l);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("EINDE");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
