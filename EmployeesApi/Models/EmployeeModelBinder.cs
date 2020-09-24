using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApi.Models
{
    /// <summary>
    /// EmployeeModelBinder binds inconming data to model entity
    /// </summary>
    public class EmployeeModelBinder : IModelBinder
    {
        /// <summary>
        /// Binds inconming data to model entity
        /// </summary>
        /// <param name="bindingContext">Context from request</param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                return Task.CompletedTask;
            }

            var model = new Employee();

            var idStr = bindingContext.ValueProvider.GetValue("id").ToString();

            long id;

            if (!string.IsNullOrEmpty(idStr))
            {
                long.TryParse(idStr, out id);
            }

            model.Name = bindingContext.ValueProvider.GetValue("name").ToString();
            model.Surname = bindingContext.ValueProvider.GetValue("surname").ToString();
            model.FathersName = bindingContext.ValueProvider.GetValue("fathers_name").ToString();
            model.Phone = bindingContext.ValueProvider.GetValue("phone").ToString();
            model.Email = bindingContext.ValueProvider.GetValue("email").ToString();


            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }
    }
}
