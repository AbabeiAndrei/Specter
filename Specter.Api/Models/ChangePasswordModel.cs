using System.ComponentModel.DataAnnotations;

namespace Specter.Api.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public virtual string CurrentPassword { get; set; }

        [Required]
        public virtual string NewPassword { get; set; }
    }
}