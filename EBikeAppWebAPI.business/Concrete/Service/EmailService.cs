using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.Operations.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Concrete.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsHtml(string email, string subject, string htmlContent)
        {
            SmtpClient client = new SmtpClient(SmtpConfig.Host, SmtpConfig.Port)
            {
                Host = SmtpConfig.Host,
                Port = SmtpConfig.Port,
                EnableSsl = SmtpConfig.EnableSSL,
                Credentials = new NetworkCredential(SmtpConfig.UserName, SmtpConfig.Password),
            };

            var sender = new MailAddress("info@ebike.com.tr", "ebike.com");
            var reciver = new MailAddress(email);
            var mailMessage = new MailMessage(sender, reciver) { IsBodyHtml = true, Sender = sender, Subject = subject, Body = htmlContent };
            await client.SendMailAsync(mailMessage);
            return true;
        }

        public async Task<bool> SendEmailAsync (string email, string subject, string url)
        {
            SmtpClient client = new SmtpClient(SmtpConfig.Host, SmtpConfig.Port)
            {
                Host = SmtpConfig.Host,
                Port = SmtpConfig.Port,
                EnableSsl = SmtpConfig.EnableSSL,
                Credentials = new NetworkCredential(SmtpConfig.UserName, SmtpConfig.Password),
            };

            string htmlContent = SetHtmlContent(_configuration["Domain"] + url, subject);
            await client.SendMailAsync(new MailMessage(
                                       SmtpConfig.UserName,
                                       email, subject,
                                       htmlContent)
            { IsBodyHtml = true });
            return true;
        }
        private Email SmtpConfig
        {
            get
            {
                Email email = new()
                {
                    EnableSSL = bool.Parse(_configuration["SmtpProfile:EnableSSL"]),
                    Host = _configuration["SmtpProfile:Host"],
                    Password = _configuration["SmtpProfile:Password"],
                    Port = int.Parse(_configuration["SmtpProfile:Port"]),
                    UserName = _configuration["SmtpProfile:UserName"]
                };
                return email;
            }
        }

        private string SetHtmlContent(string url, string subject)
        {
            return "<h2 style='margin-bottom:0!important'>ebike.com.tr Hoş Geldiniz</h2> <h3 style='margin-top:0!important;'>" + subject + "</h3> <a style='text-decoration:none;color:#fff;background-color:#007bff;padding:1rem;border-radius:10px;margin-top:2rem;' href='" + url + "'>Tamam</a> <p style='margin-bottom:0;margin-top:2rem'>Eğer linke gidemezseniz aşağıdaki linki tarayıcıda açın</p> <a style='margin-top:0' href='" + url + "'>" + url + "</a>";
        }
    }
}
