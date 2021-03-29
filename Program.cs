using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Data;
using _2021_dotnet_e_02.Data.Repositories;
using _2021_dotnet_e_02.Models;
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
                    CompanyRepository companyRepo = new CompanyRepository(db);
                    Console.WriteLine("#########COMPANIES########");
                    IEnumerable<ActemiumCompany> companies = companyRepo.GetAll();
                    foreach (ActemiumCompany company in companies)
                    {
                        Console.WriteLine(company.Name);
                        //prints to check if the relations are correct
                        //count is 0 atm for every list because the relations are not on point yet
                        Console.WriteLine(company.Tickets.Count);
                        Console.WriteLine(company.ContactPersons.Count);
                        Console.WriteLine(company.Contracts.Count);
                    }

                    Console.WriteLine("GETBY ID TEST");
                    Console.WriteLine(companyRepo.GetBy(2).Name);

                    /*
                    // doesnt work yet
                    TicketRepository ticketRepo = new TicketRepository(db);
                    IEnumerable<ActemiumTicket> tickets = ticketRepo.GetAll();
                    //Console.WriteLine(list);
                    foreach (ActemiumTicket ticket in tickets)
                    {
                        Console.WriteLine(ticket.Priority);
                    }
                    // doesnt work yet
                    //IEnumerable<ActemiumTicketChange> list2 = db.ActemiumTicketChanges.ToList();
                    //Console.WriteLine(list);
                    foreach (ActemiumTicket ticket in tickets)
                    {
                        Console.WriteLine(ticket.Title);
                        foreach(ActemiumTicketChange ticketChange in ticket.TicketChanges)
                        {
                            Console.WriteLine(ticketChange.UserRole);
                        }
                    }
                    */
                    UserRepository userRepo = new UserRepository(db);
                    Console.WriteLine("#########CUSTOMERS########");
                    IEnumerable<UserModel> customers = userRepo.GetAllCustomers();
                    foreach (ActemiumCustomer Customer in customers)
                    {
                        Console.WriteLine(Customer.UserName);
                    }
                    
                    Console.WriteLine("#########EMPLOYEES########");
                    IEnumerable<UserModel> employees = userRepo.GetAllEmployees();
                    foreach (ActemiumEmployee Employee in employees)
                    {
                        Console.WriteLine(Employee.UserName);
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
