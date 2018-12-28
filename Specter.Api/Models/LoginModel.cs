using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class LoginModel
    {
        [Required]
        public virtual string Email { get; set; }
        
        [Required]
        public virtual string Password { get; set; }

        public virtual bool Persist { get; set; }
    }
}