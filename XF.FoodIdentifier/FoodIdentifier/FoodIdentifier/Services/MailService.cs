using FoodIdentifier.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FoodIdentifier.Services
{
    public class MailService : IMailService
    {
        public async Task SendMailAsync(List<string> recipient, string subject, string body)
        {
            var message = new EmailMessage
            {
                To = recipient,
                Subject = subject,
                Body = body
            };

            await Email.ComposeAsync(message);
        }
    }
}
