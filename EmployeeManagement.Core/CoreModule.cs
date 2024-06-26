﻿using EmployeeManagement.Core.Db;
using EmployeeManagement.Core.Implementations;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.Core
{
    public static class CoreModule
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, Action<DbOptions> dbOptionsConfig)
        {
            services.Configure(dbOptionsConfig);
            services.AddDbContextFactory<ApplicationDbContext>((sp, options) =>
            {
                var dbOptions = sp.GetRequiredService<IOptions<DbOptions>>().Value;
                options.UseSqlServer(dbOptions.ConnectionString)
                       .EnableSensitiveDataLogging(dbOptions.EnableSensitiveDataLogging)
                       .EnableDetailedErrors(dbOptions.EnableDetailedErrors);
            });

            return services
                .AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
