using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Utility;

namespace UnicornInsurance.Utilities.Mail
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings Settings { get; set; }

        public EmailSender(IOptions<EmailSettings> emailOptions)
        {
            Settings = emailOptions.Value;
        }

        public async Task<bool> SendEmail(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(Settings.SendGridKey);
            var msg = new SendGridMessage()
            {
                // TODO : change the email from jacob to unicorn insurance
                From = new EmailAddress("jacobswink1@gmail.com", "Unicorn Insurance"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            try
            {
                var response = await client.SendEmailAsync(msg);
                return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
            }
            catch (Exception)
            {

            }

            return false;
        }
    }
}
