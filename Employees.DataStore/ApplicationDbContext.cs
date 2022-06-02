using Employees.DataStore.DbConfigurations;
using Employees.DataStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Db
{
    /// <summary>
    /// Database context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        /// <summary>
        /// Set of employees from database
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }
    }
}
