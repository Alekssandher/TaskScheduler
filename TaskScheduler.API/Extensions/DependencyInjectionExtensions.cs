using Microsoft.EntityFrameworkCore;
using TaskScheduler.API.Domain.Interfaces;
using TaskScheduler.API.Domain.Repositories;
using TaskScheduler.API.Domain.Services;
using TaskScheduler.API.Infrastructure.Db;

namespace TaskScheduler.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mysql");

            services.AddDbContext<MyDbContext>(
                options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
            services.AddScoped<IJwtRepository, JwtRepository>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }   
}