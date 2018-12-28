using System;
using System.IO;
using System.Threading.Tasks;

namespace Specter.Api.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, IEmailTemplate template);
    }

    public class SmtpEmailService : IEmailService
    {
        public Task<bool> SendEmail(string email, IEmailTemplate template)
        {
            throw new NotImplementedException();
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

        public Task<bool> SendEmail(string email, IEmailTemplate template)
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