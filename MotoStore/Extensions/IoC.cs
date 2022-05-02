using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
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
            IConfiguration configuration)
        {
            //Services
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IManagerPersonalAreService, ManagerPersonalAreaService>();
            services.AddScoped<IUserService, UserService>();
            
            string pathToDefaultAvatar = configuration.GetValue<string>("PathToDefaultAvatar:Path");
            services.AddScoped<IDefaultAvatarService, DefaultAvatarService>(_ 
                => new DefaultAvatarService(pathToDefaultAvatar));
            
            //Email Config
            string host = configuration.GetValue<string>("EmailSender:Host");
            int port = configuration.GetValue<int>("EmailSender:Port");
            string from = configuration.GetValue<string>("EmailSender:From");
            string password = configuration.GetValue<string>("EmailSender:Password");
            bool useSsl = configuration.GetValue<bool>("EmailSender:UseSSL");
            
            services.AddTransient<IEmailService, EmailService>(_ => 
                new EmailService(host, port, from, password, useSsl));
            
            //Repositories
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}