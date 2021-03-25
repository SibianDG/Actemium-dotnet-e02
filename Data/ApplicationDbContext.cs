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

        public DbSet<KbItem> KbItems { get; set; }

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
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ticketsysteme02;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new KbItemConfiguration());


        }
    }
}
