namespace Specter.Api.Services.Email
{
    public interface IEmailConfiguration
    {
        string ApiKey { get; }

        string FromEmail { get; }

        string FromName { get; }

        string SmtpHost { get; }

        int SmtpPort { get; }

        bool SmtpUseSsl { get; }

        string SmtpUser { get; }

        string SmtpPassword { get; }
    }

    internal class EmailConfiguration : IEmailConfiguration
    {
        public virtual string ApiKey { get; set; }

        public virtual string FromEmail { get; set; }

        public virtual string FromName { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public bool SmtpUseSsl { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }
    }
}