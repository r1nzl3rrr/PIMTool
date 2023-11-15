using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PIMTool.Core.Domain.Entities;
using System.Reflection;

namespace PIMTool.Database
{
    public class PimContext : DbContext
    {
        // TODO: Define your models

        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectEmployee> ProjectEmployees { get; set; } = null!;

        public PimContext(DbContextOptions options) : base(options)
        {
        }

        public PimContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}