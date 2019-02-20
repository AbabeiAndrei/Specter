namespace Specter.Api.Services.Email
{
    public class UserResetPasswordEmailTemplate : BaseEmailTemplate
    {
        private readonly string _resetLink;

        public override string Subject => "Specter - Password Reset";

        public UserResetPasswordEmailTemplate(string resetLink)
        {
            _resetLink = resetLink;
        }

        protected override string BuildBody()
        {
            return $"You've requested a reset password link. Please follow this url : {_resetLink}";
        }
    }
}