using Employees.DataStore.DependencyInjection;
using EmployeesApi.Db;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.DataStoreMigrations
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var appConfig = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .AddUserSecrets<ApplicationDbContextFactory>()
                                    .Build();
            var sp = new ServiceCollection().AddApplicationDataStore(appConfig,
                                                                                   isProduction: false,
                                                                                   typeof(ApplicationDbContextFactory).Assembly.GetName().Name)
                                                          .BuildServiceProvider();
            return sp.GetRequiredService<ApplicationDbContext>();
        }
    }
}
