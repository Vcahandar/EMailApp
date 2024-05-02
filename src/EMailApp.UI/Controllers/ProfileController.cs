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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserUpdateVM
            {
                Name = user.Name,
                Surname = user.Surname,
                PictureUrl = user.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserUpdateVM userUpdateVM)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            if (userUpdateVM.Picture != null)
            {
                var extension = Path.GetExtension(userUpdateVM.Picture.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "userimage", imageName);

                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await userUpdateVM.Picture.CopyToAsync(stream);
                }

                user.ImageUrl = imageName;
            }

            user.Name = userUpdateVM.Name;
            user.Surname = userUpdateVM.Surname;

            if (!string.IsNullOrEmpty(userUpdateVM.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userUpdateVM.Password);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Profile");
            }
            else
            {
                // If update fails, add model errors to display in the view
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(userUpdateVM);
            }
        }

    }
}
