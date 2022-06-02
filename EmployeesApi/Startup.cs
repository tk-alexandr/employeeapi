using Employees.DataStore.DependencyInjection;
using Employees.DataStoreMigrations;
using EmployeesApi.Config;
using EmployeesApi.Db;
using EmployeesApi.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

namespace EmployeesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment   = environment;
        }

        private readonly IConfiguration   _configuration;
        private readonly IHostEnvironment _environment;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEmployeeApiServices();
            services.AddEmployeeApiAuthentication(_configuration);

            services.AddApplicationDataStore(_configuration, _environment.IsProduction(), typeof(ApplicationDbContextFactory).Assembly.GetName().Name);

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeesApi", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In          = ParameterLocation.Header,
                    Name        = JwtBearerDefaults.AuthenticationScheme,
                    Type        = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.OperationFilter<AddResponseHeadersFilter>();
            });
            
            services.AddOptions<JwtOptions>().Bind(_configuration.GetSection("Jwt"));
        }
        
        public void Configure(IApplicationBuilder app, ApplicationDbContext context, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeesApi v1"));
            }
            
            context.Database.Migrate();

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
