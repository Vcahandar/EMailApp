using EMailApp.Business.Interfaces;
using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        public async Task<IActionResult> Sendbox(string p)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            p = values.Email;
            var messageList = _messageService.GetListSenderMessage(p);
            return View(messageList);
        }


        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> SendMessage(Message p)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string mail = values.Email;
            string name = values.Name + " " + values.Surname;
            p.Date = DateTime.Now;
            p.SenderMail = mail;
            p.SenderName = name;
            p.Status = true;
            p.IsDraft = false;
            p.IsRead = false;
            EMailDbContext c = new EMailDbContext();
            var usernamesurname = c.Users.Where(x => x.Email == p.ReceiverMail).Select(y => y.Name + " " + y.Surname).FirstOrDefault();
            p.ReceiverName = usernamesurname;
            _messageService.TInsert(p);
            return RedirectToAction("Inbox");

        }




    }
}
