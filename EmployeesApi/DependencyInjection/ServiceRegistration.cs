using Employees.DataStore.Exceptions;
using EmployeesApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeesApi.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddEmployeeApiServices(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<JwtGenerator>();

            return services;
        }


        public static IServiceCollection AddEmployeeApiAuthentication(this IServiceCollection services, IConfiguration cofiguration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    authenticationScheme: JwtBearerDefaults.AuthenticationScheme,
                    configureOptions: options =>
                    {
                        options.IncludeErrorDetails = true;
                        options.TokenValidationParameters =
                            new TokenValidationParameters()
                            {
                                IssuerSigningKey = new SymmetricSecurityKey(
                                    Encoding.UTF32.GetBytes(cofiguration["Jwt:PrivateKey"] ??
                                                            throw new ConfigurationException("Jwt", "PrivateKey"))
                                ),
                                ValidAudience         = "employee_app",
                                ValidIssuer           = "employee_app",
                                RequireExpirationTime = true,
                                RequireAudience       = true,
                                ValidateIssuer        = true,
                                ValidateLifetime      = true,
                                ValidateAudience      = true
                            };
                    });

            return services;
        }
    }
}
