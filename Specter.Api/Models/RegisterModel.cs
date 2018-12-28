using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class RegisterModel
    {
        [Required]
        public virtual string Email { get; set; }

        [Required]
        public virtual string Password { get; set; }

        [Required]
        public virtual string FirstName { get; set; }

        [Required]
        public virtual string LastName { get; set; }
    }
}