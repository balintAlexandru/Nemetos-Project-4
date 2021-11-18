using Microsoft.EntityFrameworkCore;
using MVC_Company.Entity;

using MVC_Company.Services;

namespace MVC_Company.Data
{
    public class EmployeeContext : DbContext
    {

        CommonMethod method = new CommonMethod();
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.SocialMedia)
                .WithOne(b => b.Employees)
                .HasForeignKey<SocialMedia>(b => b.EmployeesRef);
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
