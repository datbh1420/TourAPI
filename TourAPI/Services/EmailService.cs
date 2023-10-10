using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;
using User.Manage.API.Models;

namespace TourAPI.Services
{
    public interface IEmailService
    {
        Task<MimeMessage> CreateMailMessage(Message message);
    }
    public class EmailService : IEmailService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public EmailService(
            UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<MimeMessage> CreateMailMessage(Message message)
        {
            var EmailExist = await checkEmailExist(message.To);
            if (EmailExist)
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse(configuration["Email:From"]));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(TextFormat.Text)
                {
                    Text = message.Content
                };
                await SendMail(emailMessage);
                return emailMessage;
            }
            return null;
        }
        private async Task<bool> checkEmailExist(IEnumerable<MailboxAddress> email)
        {
            var listEmail = email.ToList();
            foreach (var item in listEmail)
            {
                var emailExist = await userManager.FindByEmailAsync(item.Address);
                if (emailExist == null)
                {
                    return false;
                }
            }
            return true;
        }
        private async Task SendMail(MimeMessage emailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(configuration["Email:Host"], int.Parse(configuration["Email:Port"]), true);
                client.Authenticate(configuration["Email:UserName"], configuration["Email:Password"]);
                await client.SendAsync(emailMessage);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
