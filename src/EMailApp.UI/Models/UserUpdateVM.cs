namespace EMailApp.UI.Models
{
    public class UserUpdateVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PictureUrl { get; set; }
        public IFormFile Picture { get; set; }
    }
}
