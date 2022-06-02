using System.Threading.Tasks;
using EmployeesApi.Models;

namespace EmployeesApi.Services
{
    public interface IUserManager
    {
        bool CheckCredentials(LoginModel loginModel);
        Task RegisterUser(RegisterModel        registerModel);
    }
}
