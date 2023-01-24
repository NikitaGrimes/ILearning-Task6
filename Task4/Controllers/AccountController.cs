using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4.Data;
using Task4.Data.Entities;
using Task4.Models;

namespace Task4.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IApplicationRepository _applicationRepository;

        public AccountController(SignInManager<ApplicationUser> signInManager, IApplicationRepository repository, 
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _applicationRepository = repository;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (_applicationRepository.IsUserExist(model.UserName))
                {
                    ApplicationUser user = _applicationRepository.GetUserByUserName(model.UserName);
                    var t = _signInManager.SignInAsync(user, true);
                    t.Wait();
                    if (t.IsCompletedSuccessfully)
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ApplicationUser user = new ApplicationUser(model.UserName);
                    _applicationRepository.AddUser(user);
                    var t = _signInManager.SignInAsync(user, true);
                    t.Wait();
                    if (t.IsCompletedSuccessfully)
                        return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Failed to login");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
