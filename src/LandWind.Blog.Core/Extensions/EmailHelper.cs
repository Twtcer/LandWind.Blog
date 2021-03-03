using System.Linq;
using System.Threading.Tasks;
using LandWind.Blog.Core.Domain.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace LandWind.Blog.Core.Extensions
{
    /// <summary>
    /// EmailHelper
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Send Email
        /// </summary>
        public static async Task SendAsync(MimeMessage message)
        {
            if (!message.From.Any())
            {
                message.From.Add(new MailboxAddress(Appsettings.Email.From.Name, Appsettings.Email.From.Username));
            }
            if (!message.To.Any())
            {
                var address = Appsettings.Email.To.Select(x => new MailboxAddress(x.Key, x.Value));
                message.To.AddRange(address);
            }

            using (var client = new SmtpClient { ServerCertificateValidationCallback = (s, c, h, e) => true })
            {
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.ConnectAsync(Appsettings.Email.Host, Appsettings.Email.Port, Appsettings.Email.UseSsl);
                await client.AuthenticateAsync(Appsettings.Email.From.Username, Appsettings.Email.From.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
