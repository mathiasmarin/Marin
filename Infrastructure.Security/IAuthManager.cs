﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    public interface IAuthManager
    {
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe);
        Task SignOut();
        string GenerateJwtToken(MarinAppUser user);
    }
}