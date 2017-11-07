using System;
using System.Threading.Tasks;
using Infrastructure.Security;
using Marin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthManager _authManager;
        private readonly IUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public AuthController(IAuthManager authManager, IUserManager userManager, IEmailSender emailSender)
        {
            _authManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.Username);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty,
                            "Du måste ha en verifierad e-post för att logga in");
                        return View(vm);
                    }
                }
                var signinResult = await _authManager.PasswordSignInAsync(vm.Username, vm.Password, false);

                if (signinResult.Succeeded)
                {
                    if (String.IsNullOrWhiteSpace(returnUrl))
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "Användarnamn eller lösenord är inkorrekt");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _authManager.SignOut();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signinResult = await _userManager.CreateUser(vm.FirstName, vm.LastName, vm.Email, vm.Password);
                
                if (signinResult.Succeeded)
                {
                    var user = _userManager.FindByEmailAsync(vm.Email).Result;

                    var code = _userManager.CreateEmailConfirmationToken(user);

                    string confirmationLink = Url.Action("ConfirmEmail",
                        "Auth", new
                        {
                            useremail = user.Email,
                            token = code
                        },
                        HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "Bekräfta e-post",
                        $"Klicka på följande länk för att bekräfta din e-post: {confirmationLink}");
                    return RedirectToAction("ConfirmEmailSent");
                }
                ModelState.AddModelError("", "Du har matat in felaktiga uppgifter");
            }
            return View();
        }

        public IActionResult ConfirmEmail(string userEmail, string token)
        {
            var user = _userManager.FindByEmailAsync(userEmail);

            var result = _userManager.ConfirmEmailAsync(user.Result, token);
            if (result.Result.Succeeded)
            {
                return View();

            }
            else
            {
                ModelState.AddModelError("","Det gick inte att bekräfta e-posten för denna användare");
                return View();
            }
        }

        public IActionResult ConfirmEmailSent()
        {
            return View();
        }
    }
}