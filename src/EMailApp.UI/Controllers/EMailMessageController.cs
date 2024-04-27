using EMailApp.Business.Interfaces;
using EMailApp.Core.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMailApp.UI.Controllers
{
    public class EMailMessageController : Controller
    {
        readonly IMessageService _messageService;
        readonly UserManager<AppUser> _userManager;
        public EMailMessageController(IMessageService messageService, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Inbox(string e)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            e = values.Email;
            var messageList = _messageService.GetListReceiverMessage(e);
            return View(messageList);
        }
    }
}
