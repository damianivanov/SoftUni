﻿using Microsoft.EntityFrameworkCore;
using SulsApp.Models;

namespace SulsApp
{
    public class SulsDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-FCSILHL\SQLEXPRESS; Database=SulsApp; Integrated Security = True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)        
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Problem> Problems{ get; set; }
        public DbSet<Submission> Submissions { get; set; }
    }
}
