namespace Application.Common
{
    public class CommandResult
    {
        public CommandResult(object result = null)
        {
            Result = result;
        }

        public object Result { get; }
    }
}