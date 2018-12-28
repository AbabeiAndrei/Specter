using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class ActivateUserModel
    {
        [Required]
        public virtual string Email { get; set; }
        
        [Required]
        public virtual string Token { get; set; }
    }
}