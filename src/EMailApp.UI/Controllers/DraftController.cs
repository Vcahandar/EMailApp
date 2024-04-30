using EMailApp.Business.Implementations;
using EMailApp.Business.Interfaces;
using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMailApp.UI.Controllers
{
    public class DraftController : Controller
    {
        readonly EMailDbContext _context;
        readonly IDraftService _draftService;
        private readonly UserManager<AppUser> _userManager;


        public DraftController(IDraftService draftService , 
                                EMailDbContext context, 
                                UserManager<AppUser> userManager)
        {
            _draftService = draftService;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult DraftMessageList()
        {
            var values = _draftService.TGetListAll();
            return View(values);
        }

        [HttpGet]
        public IActionResult DraftMessageSave()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DraftMessageSave(Draft draft)
        {
           
            if (draft == null)
            {
                return BadRequest(); 
            }

            if (string.IsNullOrWhiteSpace(draft.Subject) || string.IsNullOrWhiteSpace(draft.Content))
            {
                ModelState.AddModelError("", "Please fill out all fields"); 
                return RedirectToAction("SendMessage", "EMailMessage");
            }

            if (string.IsNullOrEmpty(draft.ReceiverMail))
            {
                draft.ReceiverMail = "default@example.com"; 
            }

            // ReceiverMail dolu mu kontrol ediyoruz
            if (!string.IsNullOrEmpty(draft.ReceiverMail))
            {
                var values = await _userManager.FindByNameAsync(User.Identity.Name);
                string mail = values.Email;
                string name = values.Name + " " + values.Surname;

                draft.Date = DateTime.Now;
                draft.SenderMail = mail;
                draft.SenderName = name;
                draft.Status = true;

                string usernamesurname = _context.Users
                    .Where(x => x.Email == draft.ReceiverMail)
                    .Select(y => y.Name + " " + y.Surname)
                    .FirstOrDefault();

                draft.ReceiverName = usernamesurname ?? "";

                _draftService.TInsert(draft);

                return RedirectToAction("DraftMessageList");
            }
            else
            {
                draft.Date = DateTime.Now;
                draft.Status = true;
                _draftService.TInsert(draft);
                return RedirectToAction("DraftMessageList");
            }
        }




        public IActionResult DraftMessageDetails(int id)
        {
            Draft draft = _draftService.TGetById(id);
            return View(draft);
        }


        public IActionResult DraftOut(int id)
        {
            _draftService.TDelete(id);
            return RedirectToAction("DraftMessageList");
        }
    }
}
