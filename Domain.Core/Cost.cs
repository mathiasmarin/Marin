using System;
using Domain.Common;

namespace Domain.Core
{
    public class Cost:Entity
    {
        public Cost(DateTime date, double costValue, string description)
        {
            this.Require(date != default(DateTime));
            this.Require(!string.IsNullOrWhiteSpace(description));
            this.Require(costValue != default(double));
            Date = date;
            CostValue = costValue;
            Description = description;
        }

        public BudgetCategory Category { get; set; }
        public DateTime Date { get; private set; }
        public double CostValue { get; private set; }
        public string Description { get; private set; }


        #region Do not use
        /// <summary>
        /// For EF
        /// </summary>
        [Obsolete]
        protected Cost()
        {
            
        }
        

        #endregion
    }
}