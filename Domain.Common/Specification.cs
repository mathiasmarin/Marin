using System;
using System.Linq.Expressions;

namespace Domain.Common
{
    /// <summary>
    /// Specification pattern. 
    /// The ToExpression method makes the boolean specification executable in Linq to entities and Linq to Sql but the logic is in the domain layer. Brilliant!
    /// Ref: http://enterprisecraftsmanship.com/2016/02/08/specification-pattern-c-implementation/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}