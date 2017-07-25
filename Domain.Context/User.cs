using Domain.Common;
using Utils;

namespace Domain.Context
{
    public class User: Entity
    {
        public User(string firstName, string lastName, string email)
        {
            Check.Require(!string.IsNullOrWhiteSpace(firstName));
            Check.Require(!string.IsNullOrWhiteSpace(lastName));
            Check.Require(!string.IsNullOrWhiteSpace(email));
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
    }
}
