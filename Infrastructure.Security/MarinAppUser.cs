using System;
using Microsoft.AspNetCore.Identity;
using Utils;

namespace Infrastructure.Security
{
    public sealed class MarinAppUser: IdentityUser
    {
        public MarinAppUser(string userName, string email) : base(userName)
        {
            Check.Require(!string.IsNullOrWhiteSpace(email));
            Check.Require(!string.IsNullOrWhiteSpace(userName));
            Email = email;
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
