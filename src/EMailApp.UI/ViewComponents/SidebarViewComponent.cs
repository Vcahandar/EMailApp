using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMailApp.UI.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EMailDbContext _context;

        public SidebarViewComponent(UserManager<AppUser> userManager, EMailDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Inbox = _context.Messages.Where(x => x.ReceiverMail == values.Email && x.Status == true && x.IsDraft == false && x.IsRead == false).Count();

            return View(values);
        }
    }
}
