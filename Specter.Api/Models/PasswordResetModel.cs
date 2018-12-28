using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class PasswordResetModel
    {
        [Required]
        public virtual string Email { get; set; }

        [Required]
        public virtual string Token { get; set; }

        [Required]
        public virtual string NewPassword { get; set; }
    }
}