namespace Application.Common
{
    /// <summary>
    /// Role interface for queries that are to be cached. 
    /// </summary>
    public interface ICachedQuery
    {
        /// <summary>
        /// This name must match with the property on the cacheremover command. 
        /// EG: Query x has property UserId with a value then Command x of type ICacheRemoveCommand must have a property called exactly UserId with the same value
        /// </summary>
        string NameOfUniqueProperty { get; }
        int DurationMinutes { get; }
    }
}