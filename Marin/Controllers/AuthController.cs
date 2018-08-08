using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Infrastructure.Security;
using Marin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Marin.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel vm)
        {
            var user = await _userManager.FindByEmailAsync(vm.UserName);
            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty,
                        "Du måste ha en verifierad e-post för att logga in");
                }

                var claims = _userManager.GetClaimsForUser(user).Result;
                var signinResult = await _authManager.PasswordSignInAsync(vm.UserName, vm.Password, true);

                if (signinResult.Succeeded)
                {
                    return Ok(new { Token = _authManager.GenerateJwtToken(user, claims), FullName = claims.First(x => x.Type.Equals("Name")).Value });
                }
            }
            
            return BadRequest("Login failed");
        }

        [HttpPost("IsUserLoggedIn")]
        public bool IsUserLoggedIn()
        {
            return User.Identity.IsAuthenticated;
        }
        [HttpPost("Logout")]
        public void Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _authManager.SignOut().Wait();
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel vm)
        {
            if (TryValidateModel(vm))
            {
                var signinResult = await _userManager.CreateUser(vm.FirstName, vm.LastName, vm.Email, vm.Password);

                if (signinResult.Succeeded)
                {
                    var user = _userManager.FindByEmailAsync(vm.Email).Result;

                    var code = _userManager.CreateEmailConfirmationToken(user);

                    string confirmationLink = Url.Action("confirmemail","login", new
                        {
                            useremail = user.Email,
                            token = code
                        },HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "Bekräfta e-post",
                        $"Klicka på följande länk för att bekräfta din e-post: " +
                        $"<a href={HtmlEncoder.Default.Encode(confirmationLink)}>Bekräfta e-post</a>");
                    return Ok();
                }
            }
            
            return BadRequest("Failed to create user");
        }
        [HttpPost("ConfirmEmail")]
        public IActionResult ConfirmEmail([FromBody]EmailConfirmationModel vm)
        {
            var user = _userManager.FindByEmailAsync(vm.Email);

            var result = _userManager.ConfirmEmailAsync(user.Result, vm.Token);
            if (result.Result.Succeeded)
            {
                return Ok();

            }
            else
            {
                return BadRequest("Det gick inte att bekräfta e-posten för denna användare");
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody]ResetPasswordModel vm)
        {
            if (TryValidateModel(vm))
            {
                var user = _userManager.FindByEmailAsync(vm.Email).Result;

                if (user == null)
                {
                    return BadRequest("Gick inte att hitta en användare med denna e-post");
                }

                var code = _userManager.CreatePasswordResetToken(user);

                string confirmationLink = Url.Action("resetpassword",
                    "login", new
                    {
                        useremail = user.Email,
                        token = code
                    },
                    HttpContext.Request.Scheme);

                _emailSender.SendEmailAsync(user.Email, "Återställ lösenord",
                    $"Klicka på följande länk för att återställa ditt lösenord: " +
                    $"<a href={HtmlEncoder.Default.Encode(confirmationLink)}>Återställ lösenord</a>");

                return Ok();
            }
            return BadRequest("Du har matat in felaktiga uppgifter");
        }
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody]NewPasswordModel vm)
        {
            if (TryValidateModel(vm))
            {
                var user = _userManager.FindByEmailAsync(vm.UserEmail);

                user.Wait();

                var result = _userManager.ResetPasswordAsync(user.Result, vm.Token, vm.NewPassword);

                result.Wait();

                if (result.Result.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest("Det gick inte att återställa lösenordet");

        }
    }
}