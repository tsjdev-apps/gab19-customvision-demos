using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodIdentifier.Interfaces
{
    public interface IMailService
    {
        Task SendMailAsync(List<string> recipient, string subject, string body);
    }
}
