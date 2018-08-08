using System.ComponentModel.DataAnnotations;

namespace Marin.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}