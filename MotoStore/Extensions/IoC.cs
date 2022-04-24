using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoStore.Repositories;
using MotoStore.Repositories.Abstractions;
using MotoStore.Services;
using MotoStore.Services.Abstractions;

namespace MotoStore.Extensions
{
    public static class IoC
    {
        public static void AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration _)
        {
            //Services
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAccountService, AccountService>();
            
            //Repositories
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}