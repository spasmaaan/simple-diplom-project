using SimpleDiplomBackend.Application.Features.EmailNotification;

namespace SimpleDiplomBackend.Application.Shared.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message, EmailTemplates template);
    }
}