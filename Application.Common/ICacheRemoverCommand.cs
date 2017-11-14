namespace Application.Common
{
    public interface ICacheRemoverCommand
    {
        ICachedQuery Query { get; }
    }
}