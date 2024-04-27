using System.ComponentModel.DataAnnotations;

namespace EMailApp.UI.Models
{
    public class LoginVM
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Enter Username...!")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter the password...!")]
        public string Password { get; set; }
    }
}
