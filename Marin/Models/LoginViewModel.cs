using System.ComponentModel.DataAnnotations;

namespace Marin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Användarnamn/epost måste matas in")]
        [Display(Name = "Användarnamn/Epost")]
        [EmailAddress(ErrorMessage = "Eposten är felaktig")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lösenordet måste matas in")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }
    }
}
