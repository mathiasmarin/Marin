namespace Application.Common
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult> where TResult : class
    {
        TResult HandleQuery(TQuery query);
    }
}