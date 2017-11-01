using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
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
            var appUser = new MarinAppUser(newUser);
            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
            {
                return result;
            }
            _userRepository.Add(newUser);
            _userRepository.SaveChanges();
            var newClaim = await _userManager.AddClaimAsync(appUser, new Claim("UserId", newUser.Id.ToString()));
            var savedS = await _userManager.UpdateAsync(appUser);
            return result;
        }
    }
}