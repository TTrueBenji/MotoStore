using System.Threading.Tasks;

namespace MotoStore.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}