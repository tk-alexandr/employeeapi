using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesApi.Models;

namespace EmployeesApi.Db
{
    /// <summary>
    /// Interface of Employee Repository
    /// </summary>
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
        Employee Load(long employeeID);
        void Save(Employee employee);
        bool Delete(long employeeID);
    }
}
