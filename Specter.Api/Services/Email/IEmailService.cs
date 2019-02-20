using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using SendGrid;
using SendGrid.Helpers.Mail;

using Specter.Api.Services.Email;

namespace Specter.Api.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, IEmailTemplate template, string name = null);
    }

    public class SendgridEmailService : IEmailService
    {
        private readonly IEmailConfiguration _configuration;
        private readonly ISecretInterpreter _secretInterpreter;

        private protected virtual IEmailConfiguration Configuration => _configuration;
        
        public SendgridEmailService(IEmailConfiguration configuration, ISecretInterpreter secretInterpreter)
        {
            _configuration = configuration;
            _secretInterpreter = secretInterpreter;
        }

        public virtual async Task<bool> SendEmailAsync(string email, IEmailTemplate template, string name = null)
        {
            var apikey = _secretInterpreter.GetKey(Configuration.ApiKey);
            var client = new SendGridClient(apikey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Configuration.FromEmail, Configuration.FromName),
                Subject = template.Subject,
                PlainTextContent = template.PlainText(),
                HtmlContent = template.HtmlText()
            };
            msg.AddTo(email, name);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }

    public class SmtpEmailService : IEmailService, IDisposable
    {
        private readonly SmtpClient _mailClient;
        private readonly IEmailConfiguration _configuration;
        //private readonly ILogger _logger;

        public SmtpEmailService(IEmailConfiguration configuration, ISecretInterpreter secretInterpreter/*, ILogger logger*/)
        {
            var user = secretInterpreter.GetKey(configuration.SmtpUser);
            var password = secretInterpreter.GetKey(configuration.SmtpPassword);

            _mailClient = new SmtpClient
            {
                Host = configuration.SmtpHost,
                Port = configuration.SmtpPort,
                EnableSsl = configuration.SmtpUseSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(user, password)
            };

            _configuration = configuration;
            //_logger = logger;
        }

        public Task<bool> SendEmailAsync(string email, IEmailTemplate template, string name = null)
        {
            return Task.Factory.StartNew(() => SendMailAsyncImpl(email, template, name));
        }

        private bool SendMailAsyncImpl(string email, IEmailTemplate template, string name)
        {
            try
            {
                using (var message = CreateMailMessage(email, template, name))
                    _mailClient.Send(message);

                return true;
            }
            catch//(Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                return false;
            }
        }

        private MailMessage CreateMailMessage(string email, IEmailTemplate template, string name)
        {
            var fromAddress = new MailAddress(_configuration.FromEmail, _configuration.FromName);
            var toAddress = new MailAddress(email, name);

            return new MailMessage(fromAddress, toAddress)
            {
                Subject = template.Body,
                Body = template.Body,
                IsBodyHtml = true
            };
        }

        public void Dispose()
        {
            _mailClient.Dispose();
        }
    }

    public class FakeEmailService : IEmailService
    {
        private readonly string _filePath;
        private readonly string _extension;

        public FakeEmailService()
        {
            _filePath = @"C:\Emails\";
            _extension = ".email";
        }

        public Task<bool> SendEmailAsync(string email, IEmailTemplate template, string name = null)
        {
            File.WriteAllText(Path.Combine(_filePath, email.Replace('@', '_') + _extension), CreateEmailBody(template));
            return Task.FromResult(true);
        }

        private static string CreateEmailBody(IEmailTemplate template)
        {
            return $"{template.Subject}{Environment.NewLine}" +
                    "---------------------------" +
                   $"{Environment.NewLine}{template.Body}";
        }
    }
}