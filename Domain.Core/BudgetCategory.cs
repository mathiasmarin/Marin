using System;
using Domain.Common;

namespace Domain.Core
{
    public class BudgetCategory:Entity
    {
        public BudgetCategory(string name)
        {
            this.Require(!string.IsNullOrWhiteSpace(name));
            Name = name;
        }

        public string Name { get; private set; }

        #region Do not use
        /// <summary>
        /// For EF
        /// </summary>
        [Obsolete]
        private BudgetCategory()
        {

        }

        #endregion

    }
}