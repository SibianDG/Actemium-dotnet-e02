﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<ACTEMIUMKBITEM> KbItems { get; set; }

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


        }
    }
}