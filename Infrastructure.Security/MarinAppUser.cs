using System;
using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    public sealed class MarinAppUser: IdentityUser
    {
        public MarinAppUser(User domainUser) : base(domainUser.Email)
        {
            Email = domainUser.Email;
        }

        #region Do not use

        /// <summary>
        /// Ef requires empty Ctor. Do not use
        /// </summary>
        [Obsolete]
        private MarinAppUser()
        {
            
        }
        

        #endregion
    }
}
