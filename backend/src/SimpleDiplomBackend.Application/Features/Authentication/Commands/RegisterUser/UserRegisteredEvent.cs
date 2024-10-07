using MediatR;
using SimpleDiplomBackend.Application.Features.EmailNotification;
using SimpleDiplomBackend.Application.Shared.Interface;
using SimpleDiplomBackend.Domain.Shared;

namespace SimpleDiplomBackend.Application.Features.Authentication.Commands.RegisterUser
{
    public class UserRegisteredEvent : IDomainEvent
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        private readonly EmailTemplates EmailTemplates;
        private readonly IEmailSender _emailService;

        public UserRegisteredEventHandler(IEmailSender emailSender)
        {
            _emailService = emailSender;
        }

        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // send email notification
            EmailMessage emailMessage = new ()
            {
                To = notification.Email,
                Subject = "Welcome to SimpleDiplomBackend",
                Model = new
                {
                    notification.FirstName,
                    notification.LastName
                }
            };

            Console.WriteLine("Sending email notification to {0}", notification.Email);
            //await _emailService.SendEmailAsync(emailMessage, EmailTemplates.WelcomeEmail);
        }
    }
}