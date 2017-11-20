using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace LawBringer
{
    internal class LawDoggsEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            string apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.Key"];
            SendGrid.SendGridClient client = new SendGrid.SendGridClient(apiKey);

            SendGrid.Helpers.Mail.SendGridMessage mail = new SendGrid.Helpers.Mail.SendGridMessage();
            mail.SetFrom(new SendGrid.Helpers.Mail.EmailAddress { Name = "LawDoggs", Email = "NoReply@LawDoggs.com" });
            mail.AddTo(message.Destination);
            mail.Subject = message.Subject;
            mail.AddContent("text/plain", message.Body);
            mail.AddContent("text/html", message.Body);

            return client.SendEmailAsync(mail);
        }
    }
}