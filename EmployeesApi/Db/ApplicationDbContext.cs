using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeesApi.Models;

namespace EmployeesApi.Db
{
    /// <summary>
    /// Database context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        /// <summary>
        /// Set of employees from database
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
    }
}
