using System.Linq;
using System.Threading.Tasks;
using Employees.DataStore.Entities;
using EmployeesApi.Db;

namespace EmployeesApi.Services
{
    /// <summary>
    /// Employee repository
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="ctx">Database context</param>
        public EmployeeRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        /// <summary>
        /// Set of employees
        /// </summary>
        public IQueryable<Employee> Employees => _context.Employees;

        /// <summary>
        /// Load employee by Id
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <returns>Employee</returns>
        public Employee Load(long employeeId)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == employeeId);
        }

        /// <summary>
        /// Saves employee to database
        /// </summary>
        /// <param name="employee">Employee entity</param>
        public async Task Save(Employee employee)
        {
            if (employee.Id == 0)
            {
                _context.Employees.Add(employee);
            }
            else
            {
                var dbEntity = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);

                if (dbEntity != null)
                {
                    dbEntity.Name        = employee.Name;
                    dbEntity.Surname     = employee.Surname;
                    dbEntity.FathersName = employee.FathersName;
                    dbEntity.Phone       = employee.Phone;
                    dbEntity.Email       = employee.Email;
                }
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Deletes employee from database
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns>Success</returns>
        public async Task<bool> Delete(long employeeId)
        {
            var dbEntity = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (dbEntity != null)
            {
                _context.Employees.Remove(dbEntity);
            }
            else
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
