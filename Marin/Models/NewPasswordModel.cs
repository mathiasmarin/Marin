using System.ComponentModel.DataAnnotations;

namespace Marin.Models
{

    public class NewPasswordModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Lösenord måste vara innehålla minst 8 tecken, max 100", MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Lösenord måste vara minst 8 tecken långt, innehålla både stora och små bokstäver och minst 1 siffra")]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Mata in lösendord igen")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Lösenorden matchar ej")]
        public string NewPasswordConfirm { get; set; }
    }
}