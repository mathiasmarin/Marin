using System.ComponentModel.DataAnnotations;

namespace Marin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Förnamn måste matas in")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Efternamn måste matas in")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Epost måste matas in")]
        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lösenord måste sättas")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Lösenord måste vara innehålla minst 8 tecken, max 100", MinimumLength = 8)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Lösenord måste vara minst 8 tecken långt, innehålla både stora och små bokstäver och minst 1 siffra")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lösenord måste sättas")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenorden matchar ej")]
        public string PasswordConfirm { get; set; }
    }
}