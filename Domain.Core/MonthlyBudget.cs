using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Core
{
    public class MonthlyBudget : Entity
    {
        public MonthlyBudget(string month, int income)
        {
            this.Require(!string.IsNullOrWhiteSpace(month));
            this.Require(!income.Equals(default(int)));
            Month = month;
            Income = income;
        }
        public string Month { get; private set; }
        public int Income { get; private set; }
        public ICollection<Cost> Costs { get; private set; }

        #region Do not use
        /// <summary>
        /// Do not use. FOr EF
        /// </summary>
        [Obsolete]
        private MonthlyBudget()
        {

        }

        #endregion

    }
}