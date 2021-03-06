﻿using AquaServiceSPA.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AquaServiceSPA.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSettingsService emailSettingsService;
        private readonly ILoggerService loggerService;

        public EmailService(IEmailSettingsService emailSettingsService,
            ILoggerService loggerService)
        {
            this.emailSettingsService = emailSettingsService;
            this.loggerService = loggerService;
        }

        public async Task SendEmailAsync(Email email)
        {
            try
            {
                var emailSettings = emailSettingsService.GetSettings();
                using (var mailMessage = new MailMessage(emailSettings.EmailAddress, email.EmailAddress))
                {
                    mailMessage.Subject = email.Subject;
                    mailMessage.Body = email.Message;
                    mailMessage.IsBodyHtml = emailSettings.IsHtmlMessage;
                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.Host = emailSettings.Smtp;
                        smtpClient.Port = emailSettings.Port;
                        smtpClient.Credentials = 
                            new NetworkCredential(emailSettings.Username, emailSettings.Password);
                        smtpClient.EnableSsl = emailSettings.EnableSsl;
                        await Task.Run(() => smtpClient.Send(mailMessage));
                    }
                };
            }
            catch (Exception e)
            {
                loggerService.Log($"Send email exception - {e.Message}");
            }
        }
    }
}
