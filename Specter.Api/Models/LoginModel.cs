namespace Specter.Api.Model
{
    public class LoginModel
    {
        public string Email { get; set; }
        
        public string Password { get; set; }

        public bool Persist { get; set; }
    }
}