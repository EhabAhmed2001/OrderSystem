
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Entities;
using OrderSystem.PL.Extensions;
using OrderSystem.Repository.Data;
using StackExchange.Redis;

namespace OrderSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<OrderManagementDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
            {
                var connection = builder.Configuration.GetConnectionString("RidusConnection");

                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddApplicationService();
            builder.Services.AddIdentityServices(builder.Configuration);

            var app = builder.Build();


            using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider;

            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var DbContext = service.GetRequiredService<OrderManagementDbContext>();

                await DbContext.Database.MigrateAsync();

                var userManager = service.GetRequiredService<UserManager<User>>();


            }
            catch (Exception ex)
            {

                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "Error during update Database");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
