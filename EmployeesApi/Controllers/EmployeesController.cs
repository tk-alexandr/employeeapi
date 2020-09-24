using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeesApi.Db;
using EmployeesApi.Models;

namespace EmployeesApi.Controllers
{
    /// <summary>
    /// Controller of employees
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public int PageSize = 10;

        /// <summary>
        /// GET api/employees/page/1
        /// </summary>
        [HttpGet("page/{page}")]
        public ActionResult<IEnumerable<Employee>> GetPage(int page)
        {
            var employees = repository.Employees
                .OrderBy(e => e.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return employees;
        }

        /// <summary>
        /// GET api/employees
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            return GetPage(1);
        }

        /// <summary>
        /// GET api/employees/5
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmloyee(long id)
        {
            return repository.Load(id);
        }

        /// <summary>
        /// POST api/employees
        /// </summary>
        [HttpPost]
        public void Post([ModelBinder(BinderType = typeof(EmployeeModelBinder))] Employee employee)
        {
            repository.Save(employee);
        }

        /// <summary>
        /// PUT api/employees/5
        /// </summary>
        [HttpPut("{id}")]
        public void Put(long id, [ModelBinder(BinderType = typeof(EmployeeModelBinder))] Employee employee)
        {
            employee.Id = id;
            repository.Save(employee);
        }

        // 
        /// <summary>
        /// DELETE api/employees/5
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            repository.Delete(id);
        }
    }
}
