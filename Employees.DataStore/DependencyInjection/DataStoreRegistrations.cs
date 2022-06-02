using System;
using Employees.DataStore.Helpers;
using EmployeesApi.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employees.DataStore.DependencyInjection
{
    public static class DataStoreRegistrations
    {
        public static IServiceCollection AddApplicationDataStore(this IServiceCollection services,
                                                                 IConfiguration          appConfig,
                                                                 bool                    isProduction,
                                                                 string                  migrationAssembly)
        {
            return services.AddDbContextFromEnvironment<ApplicationDbContext>(appConfig, isProduction, migrationAssembly);
        }

        public static IServiceCollection AddDbContextFromEnvironment<TContext>(this IServiceCollection services,
                                                                               IConfiguration          appConfig,
                                                                               bool                    isProduction,
                                                                               string                  migrationAssembly,
                                                                               string                  connectionVarName = "postgres",
                                                                               string                  dbVarName         = "APP_DB",
                                                                               string                  hostVarName       = "APP_DB_HOST",
                                                                               string                  portVarName       = "APP_DB_PORT",
                                                                               string                  userVarName       = "APP_DB_USER",
                                                                               string                  passwordVarName   = "APP_DB_PWD")
            where TContext : DbContext
        {
            if (string.IsNullOrWhiteSpace(connectionVarName))
                throw new ArgumentNullException(nameof(connectionVarName));
            if (string.IsNullOrWhiteSpace(passwordVarName))
                throw new ArgumentNullException(nameof(passwordVarName));

            services.AddDbContext<TContext>(options =>
            {
                var connectionStr = ConnectionStringBuilder.Build(appConfig,
                                                                  isProduction,
                                                                  connectionVarName,
                                                                  dbVarName,
                                                                  hostVarName,
                                                                  portVarName,
                                                                  userVarName,
                                                                  passwordVarName);
                if (string.IsNullOrWhiteSpace(migrationAssembly))
                    options.UseNpgsql(connectionStr);
                else
                    options.UseNpgsql(connectionStr, npgsqlOptionsAction => npgsqlOptionsAction.MigrationsAssembly(migrationAssembly));
            });
            return services;
        }
    }
}
