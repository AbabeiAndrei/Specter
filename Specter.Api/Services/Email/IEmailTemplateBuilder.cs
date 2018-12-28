using System;
using Specter.Api.Services.Email;

namespace Specter.Api.Services
{
    public enum EmailTemplates : short
    {
        UserResetPassword = 0,
        ActivateUser = 1,
    }

    public interface IEmailTemplateBuilder
    {
        IEmailTemplate BuildTemplate(EmailTemplates template, params string[] parameters);
    }

    public class EmailTemplateBuilder : IEmailTemplateBuilder
    {
        public IEmailTemplate BuildTemplate(EmailTemplates template, params string[] parameters)
        {
            switch(template)
            {
                case EmailTemplates.UserResetPassword:
                    return new UserResetPasswordEmailTemplate(parameters[0]);
                case EmailTemplates.ActivateUser:
                    return new ActivateUserEmailTemplate(parameters[0], parameters[1]);
                default:
                    throw new ArgumentOutOfRangeException(nameof(template), template, "Unknown template type");
            }
        }
    }
}
