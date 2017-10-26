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

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
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
    }
}