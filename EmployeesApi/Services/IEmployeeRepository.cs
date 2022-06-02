using System.Linq;
using System.Threading.Tasks;
using Employees.DataStore.Entities;

namespace EmployeesApi.Services
{
    /// <summary>
    /// Interface of Employee Repository
    /// </summary>
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
        Employee Load(long     employeeId);
        Task Save(Employee     employee);
        Task<bool> Delete(long employeeId);
    }
}