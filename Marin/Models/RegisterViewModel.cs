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
        public string Password { get; set; }
        [Required(ErrorMessage = "Lösenord måste sättas")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenorden matchar ej")]
        public string PasswordConfirm { get; set; }
    }
}