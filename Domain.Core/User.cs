using System;
using Domain.Common;

namespace Domain.Core
{
    public class User: Entity
    {
        public User(string firstName, string lastName, string email)
        {
            this.Require(!string.IsNullOrWhiteSpace(firstName));
            this.Require(!string.IsNullOrWhiteSpace(lastName));
            this.Require(!string.IsNullOrWhiteSpace(email));
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }

        public string GetFullName()
        {
            return string.Join(" ", FirstName, LastName);
        }
        /// <summary>
        /// Ef requires empty Ctor. Do not use
        /// </summary>
        [Obsolete]
        private User()
        {

        }
    }
}