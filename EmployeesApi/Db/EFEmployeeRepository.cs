using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesApi.Models;

namespace EmployeesApi.Db
{
    /// <summary>
    /// Employee repository
    /// </summary>
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext context;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ctx">Database context</param>
        public EFEmployeeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        /// <summary>
        /// Set of employees
        /// </summary>
        public IQueryable<Employee> Employees => context.Employees;

        /// <summary>
        /// Load employee by Id
        /// </summary>
        /// <param name="employeeID">Employee id</param>
        /// <returns>Employee</returns>
        public Employee Load(long employeeID)
        {
            return context.Employees.FirstOrDefault(e => e.Id == employeeID);;
        }

        /// <summary>
        /// Saves employee to database
        /// </summary>
        /// <param name="employee">Employee entity</param>
        public void Save(Employee employee)
        {
            if (employee.Id == 0)
            {
                context.Employees.Add(employee);
            }
            else
            {
                var dbEntity = context.Employees.FirstOrDefault(e => e.Id == employee.Id);

                if (dbEntity != null)
                {
                    dbEntity.Name = employee.Name;
                    dbEntity.Surname = employee.Surname;
                    dbEntity.FathersName = employee.FathersName;
                    dbEntity.Phone = employee.Phone;
                    dbEntity.Email = employee.Email;
                }
            }

            context.SaveChanges();

        }

        /// <summary>
        /// Deletes employee from database
        /// </summary>
        /// <param name="employeeID">Employee Id</param>
        /// <returns>Success</returns>
        public bool Delete(long employeeID)
        {
            var dbEntity = context.Employees.FirstOrDefault(e => e.Id == employeeID);

            if (dbEntity != null)
            {
                context.Employees.Remove(dbEntity);
            }
            else
            {
                return false;
            }

            context.SaveChanges();
            return true;
        }

        
    }
}
