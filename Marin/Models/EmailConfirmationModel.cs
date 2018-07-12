using System.ComponentModel.DataAnnotations;

namespace Marin.Models
{
    public class EmailConfirmationModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}