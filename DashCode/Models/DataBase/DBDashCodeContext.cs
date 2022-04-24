using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashCode.Models.DataBase
{
    public class DBDashCodeContext : DbContext
    {
        //protected DBDashCodeContext() : base("DBConnectionString")
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocal; DataBase=DashCodeBD; Trusted_Connection=True");
        }
        public DbSet<User> Users { get; private set; }
    }
}
