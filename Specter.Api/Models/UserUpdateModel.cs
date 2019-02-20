namespace Specter.Api.Models
{
    public class UserUpdateModel
    {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string CurrentPassword { get; set; }

        public virtual string NewPassword { get; set; }

        public virtual UserPreferencesModel Preferences { get; set; }
    }
}