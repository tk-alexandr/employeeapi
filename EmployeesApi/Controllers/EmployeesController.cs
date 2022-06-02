using Employees.DataStore.Entities;
using EmployeesApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Controllers
{
    /// <summary>
    /// Controller of employees
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository          _repository;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeRepository repository, ILogger<EmployeesController> logger)
        {
            _repository = repository;
            _logger     = logger;
        }

        public int PageSize = 10;

        /// <summary>
        /// GET api/employees/page/1
        /// </summary>
        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetPage(int page)
        {
            try
            {
                var employees = await _repository.Employees
                    .OrderBy(e => e.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync(); 
                
                return employees;
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }

        /// <summary>
        /// GET api/employees
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            try
            {
                return await GetPage(1);
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }

        /// <summary>
        /// GET api/employees/5
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(long id)
        {
            try
            {
                return _repository.Load(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }

        /// <summary>
        /// POST api/employees
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Employee employee)
        {
            try
            {
                await _repository.Save(employee);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }

        /// <summary>
        /// PUT api/employees/5
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, [FromBody] Employee employee)
        {
            try
            {
                employee.Id = id;
                await _repository.Save(employee);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }

        // 
        /// <summary>
        /// DELETE api/employees/5
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, EmployeeApiLogs.InternalServierError);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = EmployeeApiLogs.InternalServierError });
            }
        }
    }
}
