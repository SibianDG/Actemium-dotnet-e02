using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Data;
using _2021_dotnet_e_02.Data.Repositories;
using _2021_dotnet_e_02.Models;
using _2021_dotnet_e_02.Models.Enums;
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

                    
                    UserRepository userRepo = new UserRepository(db);
                    Console.WriteLine("#########CUSTOMERS########");
                    IEnumerable<UserModel> customers = userRepo.GetAllCustomers();
                    foreach (ActemiumCustomer Customer in customers)
                    {
                        Console.WriteLine(Customer.UserName + " " + Customer.Company.Name);
                    }
                    
                    Console.WriteLine("#########EMPLOYEES########");
                    IEnumerable<UserModel> employees = userRepo.GetAllEmployees();
                    foreach (ActemiumEmployee Employee in employees)
                    {
                        Console.WriteLine(Employee.UserName + " " + Employee.Role);
                    }
                    
                    Console.WriteLine("#########TICKETS########");
                    TicketRepository TikcetRepo = new TicketRepository(db);
                    IEnumerable<ActemiumTicket> Tickets = TikcetRepo.GetAll();
                    foreach (ActemiumTicket Ticket in Tickets)
                    {
                        Console.WriteLine(Ticket.Priority.ToString() + Ticket.Title);
                        Console.WriteLine("Comments" + Ticket.Comments.Count);
                        foreach(ActemiumTicketComment comment in Ticket.Comments)
                        {
                            Console.WriteLine(comment.CommentText);
                            Console.WriteLine(comment.ToString());
                        }
                        Console.WriteLine("Changes" + Ticket.TicketChanges.Count);
                        foreach (ActemiumTicketChange ticketChange in Ticket.TicketChanges)
                        {
                            Console.WriteLine(ticketChange.UserRole);
                        }
                    }
                    
                    Console.WriteLine("#########LoginAttempts########");
                    IEnumerable<LoginAttempt> LoginAttemps = db.LoginAttempts.ToList();
                    foreach (LoginAttempt loginAttempt in LoginAttemps)
                    {
                        Console.WriteLine(loginAttempt.LoginStatus);
                    }
                    
                                        
                    Console.WriteLine("#########ContractType########");
                    IEnumerable<ActemiumContractType> contractTypes = db.ActemiumContractTypes.ToList();
                    foreach (ActemiumContractType contractType in contractTypes)
                    {
                        Console.WriteLine(contractType.Name);
                    }
                    
                    Console.WriteLine("#########Contract########");
                    IEnumerable<ActemiumContract> contracts = db.ActemiumContracts.ToList();
                    foreach (ActemiumContract c in contracts)
                    {
                        Console.WriteLine(c.ContractId);
                        Console.WriteLine(c.ContractType.Name);
                        //Console.WriteLine(c.Company.Name);
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
