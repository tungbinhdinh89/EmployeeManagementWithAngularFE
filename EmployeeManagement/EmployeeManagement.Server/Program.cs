using EmployeeManagement.Core;
using EmployeeManagement.Shared.Options;

namespace EmployeeManagement.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var dbOptions = builder.Configuration.GetSection(DbOptions.SECTION).Get<DbOptions>();
        builder.Services.AddCoreServices(dbOptions => builder.Configuration.GetSection(DbOptions.SECTION).Bind(dbOptions));


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();

        }



        app.UseAuthorization();


        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();
    }
}
