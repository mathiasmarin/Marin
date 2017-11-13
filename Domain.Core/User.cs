using System;
using System.Collections.Generic;
using System.Linq;
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
        public ICollection<BudgetCategory> BudgetCategories { get; private set; }
        public ICollection<MonthlyBudget> Budgets { get; private set; }
        public string GetFullName()
        {
            return string.Join(" ", FirstName, LastName);
        }

        public void AddCategories(ICollection<BudgetCategory> categories)
        {
            this.Require(categories.Any());
            if (BudgetCategories == null)
            {
                BudgetCategories = categories.ToList();
            }
            else
            {
                foreach (var cat in categories)
                {
                    if(BudgetCategories.Any(h => h.Name.Equals(cat.Name))) continue;
                    BudgetCategories.Add(cat);
                }
            }
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