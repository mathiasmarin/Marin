﻿using System;
using System.Collections.Generic;
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
            await _userManager.AddClaimAsync(appUser, new Claim("UserId", newUser.Id.ToString()));
            await _userManager.AddClaimAsync(appUser, new Claim("Name", newUser.GetFullName()));
            await _userManager.UpdateAsync(appUser);
            return result;
        }

        public async Task<MarinAppUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
            
        }

        public async Task<bool> IsEmailConfirmedAsync(MarinAppUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public string CreateEmailConfirmationToken(MarinAppUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

        }

        public string CreatePasswordResetToken(MarinAppUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user).Result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(MarinAppUser user, string code)
        {
            return await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<IdentityResult> ResetPasswordAsync(MarinAppUser user, string code, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, code, newPassword);
        }

        public async Task<IList<Claim>> GetClaimsForUser(MarinAppUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }
    }
}