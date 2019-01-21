namespace Specter.Api.Models
{
    public class UserModel
    {        
        public virtual string Email { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual UserPreferencesModel Preferences { get; set; }
    }

    public class LoginUserModel : UserModel
    {
        public virtual string Token { get; set; }
    }

    public class UserPreferencesModel
    {
        public virtual bool DarkMode { get; set; }
    }
}