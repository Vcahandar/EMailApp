using EMailApp.Core.Concrete;
using EMailApp.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMailApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                if (registerVM.Password != registerVM.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(registerVM);
                }

                var appUser = new AppUser()
                {
                    Name = registerVM.Name,
                    Surname = registerVM.Surname,
                    Email = registerVM.Email,
                    UserName = registerVM.Username,
                    ImageUrl = "defaultImage.jpg"
                };

                var result = await _userManager.CreateAsync(appUser, registerVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(registerVM);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }






        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(loginVM.EmailOrUsername))
                {
                    var isEmail = loginVM.EmailOrUsername.Contains("@");
                    var user = isEmail ? await _userManager.FindByEmailAsync(loginVM.EmailOrUsername) : await _userManager.FindByNameAsync(loginVM.EmailOrUsername);

                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, true);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Inbox", "EMailMessage");
                        }
                    }
                }
            }

            // Eğer giriş başarısızsa, ModelState'e bir hata ekleyin
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            // Hata durumunda, Login sayfasını yeniden göster
            return View(loginVM);
        }








        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }



    }
}
