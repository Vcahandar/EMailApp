using EMailApp.Core.Concrete;
using EMailApp.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMailApp.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateVM model = new UserUpdateVM();
            model.Name = values.Name;
            model.Surname = values.Surname;
            model.PictureUrl = values.ImageUrl;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(UserUpdateVM userUpdateVM)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (userUpdateVM.Picture != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(userUpdateVM.Picture.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimage/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await userUpdateVM.Picture.CopyToAsync(stream);
                user.ImageUrl = imagename;
            }
            user.Name = userUpdateVM.Name;
            user.Surname = userUpdateVM.Surname;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userUpdateVM.Password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }
    }
}
