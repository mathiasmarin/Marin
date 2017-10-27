using System;
using Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security
{
    /// <inheritdoc />
    /// <summary>
    /// This maps to a domainuser. Domainuser should never have login information, therefore there are two types of users.
    /// The mapping is done via the username (email). Should maybe have done this better via guid but unuqie constraint on email is fine. 
    /// </summary>
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
