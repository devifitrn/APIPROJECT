using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Profiling> Profillings { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
              .HasOne(a => a.Account)
              .WithOne(b => b.Employee)
              .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profiling)
                .WithOne(b => b.Account)
                .HasForeignKey<Profiling>(b => b.NIK);

            modelBuilder.Entity<University>()
                .HasMany(a => a.Educations)
                .WithOne(b => b.University)
                .HasForeignKey(b => b.UniversityId);

            modelBuilder.Entity<Profiling>()
                .HasOne(a => a.Education)
                .WithMany(b => b.Profilings)
                .HasForeignKey(b => b.EducationId);

            modelBuilder.Entity<AccountRole>()
                .HasKey(bc => new { bc.AccountId, bc.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.AccountRole)
                .HasForeignKey(bc => bc.AccountId);
            modelBuilder.Entity<Role>()
                .HasMany(c => c.AccountRole)
                .WithOne(bc => bc.Role)
                .HasForeignKey(bc => bc.RoleId);
        }
    }
}
