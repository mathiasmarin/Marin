using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    public class AuthManager:IAuthManager
    {
        private readonly SignInManager<MarinAppUser> _signInManager;

        public AuthManager(SignInManager<MarinAppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe)
        {
            var hej =  await _signInManager.PasswordSignInAsync(userName, password, false, false);
            return hej;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}