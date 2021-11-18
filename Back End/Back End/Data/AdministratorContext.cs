using Back_End.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Data
{
    public class AdministratorContext : DbContext
    {
        CommonMethod method = new CommonMethod();
        public AdministratorContext(DbContextOptions<AdministratorContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                ID = 1,
                UserName = "Admin",
                Password = method.ConvertToEncrypt("admin")
            });
        }
    }
}
