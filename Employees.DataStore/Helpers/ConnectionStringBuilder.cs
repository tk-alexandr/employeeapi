using Employees.DataStore.Exceptions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Employees.DataStore.Helpers
{
    public class ConnectionStringBuilder
    {
        public static string Build(IConfiguration appConfig,
                                   bool           isProduction,
                                   string         connectionVarName = "postgres",
                                   string         dbVarName         = "APP_DB",
                                   string         hostVarName       = "APP_DB_HOST",
                                   string         portVarName       = "APP_DB_PORT",
                                   string         userVarName       = "APP_DB_USER",
                                   string         passwordVarName   = "APP_DB_PWD")
        {
            var source = appConfig.GetConnectionString(connectionVarName);
            if (string.IsNullOrWhiteSpace(source))
                throw new ConfigurationException("ConnectionsStrings", connectionVarName);

            var user = appConfig[userVarName];
            if (string.IsNullOrWhiteSpace(user))
                throw new ConfigurationException(
                    $"There is no db user name in the environment variable '{userVarName}'");

            var password = appConfig[passwordVarName];
            if (string.IsNullOrWhiteSpace(password))
                throw new ConfigurationException(
                    $"There is no db password in the environment variable '{passwordVarName}'");

            var host = appConfig[hostVarName];
            if (string.IsNullOrWhiteSpace(host))
                throw new ConfigurationException($"There is no db host in the environment variable '{hostVarName}'");

            var portStr = appConfig[portVarName] ?? "";
            if (string.IsNullOrWhiteSpace(portStr) || !int.TryParse(portStr, out var port))
                port = 5432;

            var dbName = appConfig[dbVarName];
            if (string.IsNullOrWhiteSpace(dbName))
                throw new ConfigurationException($"There is no db name in the environment variable '{dbVarName}'");

            var builder = new NpgsqlConnectionStringBuilder(source)
            {
                Host                = host,
                Port                = port,
                Username            = user,
                Password            = password,
                Database            = dbName,
                IncludeErrorDetails = !isProduction
            };
            return builder.ConnectionString;
        }
    }
}