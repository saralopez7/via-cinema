using System.Threading.Tasks;

namespace VIACinemaApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}