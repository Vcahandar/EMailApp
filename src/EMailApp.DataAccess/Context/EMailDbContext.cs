using EMailApp.Core.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailApp.DataAccess.Context
{
    public class EMailDbContext:IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-1HLMAF8;Database=EMailAppDB;Trusted_Connection=True;");
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Draft> Drafts { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Message>().HasQueryFilter(m => m.Status);


        //}
    }
}
