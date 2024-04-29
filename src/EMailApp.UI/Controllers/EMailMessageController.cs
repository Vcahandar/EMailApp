﻿using EMailApp.Business.Interfaces;
using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMailApp.UI.Controllers
{
    public class EMailMessageController : Controller
    {
        readonly IMessageService _messageService;
        readonly UserManager<AppUser> _userManager;
        readonly EMailDbContext _context;
        public EMailMessageController(IMessageService messageService, UserManager<AppUser> userManager, EMailDbContext context)
        {
            _messageService = messageService;
            _userManager = userManager;
            _context = context;
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
        public async Task<IActionResult> SendMessage(Message message)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            string mail = values.Email;
            string name = values.Name + " " + values.Surname;
            message.Date = DateTime.Now;
            message.SenderMail = mail;
            message.SenderName = name;
            message.Status = true;
            message.IsDraft = false;
            message.IsRead = false;
            var usernamesurname = _context.Users.Where(x => x.Email == message.ReceiverMail).Select(y => y.Name + " " + y.Surname).FirstOrDefault();
            message.ReceiverName = usernamesurname;
            _messageService.TInsert(message);
            return RedirectToAction("Inbox");

        }



        public IActionResult InboxMessageDetails(int id)
        {
            Message message = _messageService.TGetById(id);
            return View(message);
        }

        public IActionResult SendboxMessageDetails(int id)
        {
            Message message = _messageService.TGetById(id);
            return View(message);
        }


        public IActionResult TrashMessageList()
        {
            var values = _messageService.GetListDeleteMessage();
            return View(values);
        }


        public IActionResult TrashMessages(int id)
        {
           
            var value = _context.Messages.Where(x => x.MessageId == id).FirstOrDefault();
            value.Status = false;
            _context.SaveChanges();
            return RedirectToAction("TrashMessageList");
        }

        public IActionResult TrashOutMessages(int id)
        {
            var value = _context.Messages.Where(x => x.MessageId == id).FirstOrDefault();
            value.Status = true;
            _context.SaveChanges();
            return RedirectToAction("TrashMessageList");
        }



        [HttpPost]
        public void MarkAsRead(int messageId)
        {
            var message = _context.Messages.FirstOrDefault(m => m.MessageId == messageId);
            if (message != null)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }
        }


    }


}
