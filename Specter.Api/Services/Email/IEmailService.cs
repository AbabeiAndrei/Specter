using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Specter.Api.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, IEmailTemplate template);
    }

    public class SmtpEmailService : IEmailService
    {
        private readonly EmailConfiguration _configuration;
        private readonly ISecretInterpreter _secretInterpreter;

        private protected virtual EmailConfiguration Configuration => _configuration;
        
        public SmtpEmailService(IConfiguration configuration, ISecretInterpreter secretInterpreter)
        {
            //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
            _configuration = configuration.GetValue<EmailConfiguration>("EmailConfig");
            _secretInterpreter = secretInterpreter;
        }

        public virtual async Task<bool> SendEmailAsync(string email, IEmailTemplate template)
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
            msg.AddTo(email);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }

    internal class EmailConfiguration
    {
        public virtual string ApiKey { get; set; }

        public virtual string FromEmail { get; set; }

        public virtual string FromName { get; set; }
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

        public Task<bool> SendEmailAsync(string email, IEmailTemplate template)
        {
            File.WriteAllText(Path.Combine(_filePath, email + _extension), CreateEmailBody(template));
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