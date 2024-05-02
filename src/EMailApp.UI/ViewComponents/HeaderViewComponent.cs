using EMailApp.Core.Concrete;
using EMailApp.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMailApp.UI.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new UserUpdateVM
            {

                PictureUrl = user.ImageUrl
            };

            return View(model);

        }

    }
}
