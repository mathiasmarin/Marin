using System.ComponentModel.DataAnnotations;

namespace Marin.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Användarnamn/epost måste matas in")]
        [Display(Name = "Användarnamn/Epost")]
        [EmailAddress(ErrorMessage = "Eposten är felaktig")]
        public string Email { get; set; }
    }
}