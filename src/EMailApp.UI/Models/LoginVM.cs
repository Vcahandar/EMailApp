using System.ComponentModel.DataAnnotations;

namespace EMailApp.UI.Models
{
    public class LoginVM
    {
        [Display(Name = "E-posta, Kullanıcı Adı veya Kullanıcı ID")]
        [Required(ErrorMessage = "E-posta, kullanıcı adı veya kullanıcı ID girin...!")]
        public string EmailOrUsername { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre girin...!")]
        public string Password { get; set; }
    }
}
