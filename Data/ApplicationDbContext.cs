using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Data.Mapping;
using _2021_dotnet_e_02.Models;

namespace _2021_dotnet_e_02.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<ActemiumKbItem> KbItems { get; set; }
        public DbSet<ActemiumCompany> ActemiumCompanies { get; set; }
        public DbSet<ActemiumTicket> ActemiumTickets { get; set; }
        public DbSet<ActemiumTicketChange> ActemiumTicketChanges { get; set; }
        public DbSet<ActemiumTicketComment> ActemiumTicketComments { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ActemiumCustomer> Customers { get; set; }
        public DbSet<ActemiumEmployee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public ApplicationDbContext() : base()
        {
            
        }

        public static  ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //"Server=.;Initial Catalog=Beerhall;Integrated Security=True"
            optionsBuilder.UseSqlServer("Server=.;Initial Catalog=ticketsysteme02;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new KbItemConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TicketChangeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketCommentConfiguration());
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());
            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            modelBuilder.Entity<ActemiumCustomer>().HasBaseType<UserModel>();
            modelBuilder.Entity<ActemiumEmployee>().HasBaseType<UserModel>();


        }
    }
}
