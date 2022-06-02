using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Employees.DataStore.Entities;
using EmployeesApi.Db;
using EmployeesApi.Models;

namespace EmployeesApi.Services
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationDbContext _context;

        public UserManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CheckCredentials(LoginModel loginModel)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == loginModel.UserName);
            if (user is null) return false;
            var hash = CreateMd5(loginModel.Password);
            return hash == user.Password;
        }

        public async Task RegisterUser(RegisterModel registerModel)
        {
            var passwordHash = CreateMd5(registerModel.Password);

            _context.Users.Add(new User
            {
                UserName = registerModel.UserName,
                Password = passwordHash
            });
            await _context.SaveChangesAsync();
        }

        private string CreateMd5(string input)
        {
            using MD5 md5        = MD5.Create();
            byte[]    inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[]    hashBytes  = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
