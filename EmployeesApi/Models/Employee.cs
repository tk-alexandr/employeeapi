using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Models
{
    /// <summary>
    /// Employee entity
    /// </summary>
    [ModelBinder(BinderType = typeof(EmployeeModelBinder))]
    public class Employee
    {
        public long Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FathersName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
