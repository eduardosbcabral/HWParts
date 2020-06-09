using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HWParts.Core.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailOptions)
        {
            _emailSettings = emailOptions.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.Account),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };

                 mailMessage.To.Add(new MailAddress(email));

                using var smtp = new SmtpClient(_emailSettings.Host, int.Parse(_emailSettings.Port));
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
