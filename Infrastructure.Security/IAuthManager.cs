using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Domain.Common;
using Domain.Core;

namespace Infrastructure.Security
{
    public interface IAuthManager
    {
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe);
        Task SignOut();
    }

    public interface IUserManager
    {
        Task<IdentityResult> CreateUser(string firstName, string lastName, string email, string password);
    }
    public class UserManager:IUserManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly UserManager<MarinAppUser> _userManager;

        public UserManager(UserManager<MarinAppUser> userManager, IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IdentityResult> CreateUser(string firstName, string lastName, string email, string password)
        {
            var newUser = new User(firstName,lastName,email);
            var appUser = new MarinAppUser(email,email);
            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
            {
                return result;
            }
            _userRepository.Add(newUser);
            _userRepository.SaveChanges();
            return result;
        }
    }
}