using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Task4.Data;
using Task4.Data.Entities;
using Task4.Hubs;
using Task4.Models;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationRepository _repository;

        private readonly IHubContext<ChatHub> _hubContext;

        public HomeController(IApplicationRepository repository, IHubContext<ChatHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Email> emails = _repository.GetEmailsByUserName(User.Identity.Name);
            List<EmailModel> emailModels = new List<EmailModel>();
            foreach (Email email in emails)
                emailModels.Add(email);

            return View(emailModels);
        }

        [Authorize]
        public IActionResult CreateEmail()
        {
            EmailCreatingModel emailCreatingModel = new EmailCreatingModel();
            emailCreatingModel.UserNames = _repository
                .GetAllUsers()
                .Select(x => x.UserName)
                .ToList();
            return View(emailCreatingModel);
        }

        [HttpPost]
        public IActionResult CreateEmail(EmailCreatingModel emailCreatingModel)
        {
            if (emailCreatingModel != null)
            {
                if (ValidateEmail(emailCreatingModel) != string.Empty)
                {
                    ModelState.AddModelError("", ValidateEmail(emailCreatingModel));
                    return View();
                }
                ApplicationUser recipient;
                if (_repository.IsUserExist(emailCreatingModel.RecipientName))
                    recipient = _repository.GetUserByUserName(emailCreatingModel.RecipientName);
                else
                {
                    _repository.AddUser(new ApplicationUser(emailCreatingModel.RecipientName));
                    recipient = _repository.GetUserByUserName(emailCreatingModel.RecipientName);
                }
                Email email = new Email(emailCreatingModel.Title, emailCreatingModel.Body,
                    User.Identity.Name, recipient);
                _hubContext.Clients.User(emailCreatingModel.RecipientName)
                    .SendAsync("ReceiveMessage", email.Id.ToString(), User.Identity.Name,
                    email.Title, email.Body, DateTime.Now.ToString());
                _repository.AddEmail(email);
            }
            return RedirectToAction("Index", "Home");
        }

        private string ValidateEmail(EmailCreatingModel emailCreatingModel)
        {
            if (emailCreatingModel.RecipientName == null)
            {
                return "You do not choose recipient!";
            }
            else if (emailCreatingModel.Title == null ||
                emailCreatingModel.Body == null)
            {
                return "Title and body can not be empty!";
            }
            else
                return string.Empty;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}