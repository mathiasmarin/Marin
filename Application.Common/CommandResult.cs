namespace Application.Common
{
    public class CommandResult
    {
        private readonly object _result;

        public CommandResult(object result)
        {
            _result = result;
        }

        public object GetResult()
        {
            return _result;
        }

    }
}