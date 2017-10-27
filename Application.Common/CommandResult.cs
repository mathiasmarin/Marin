using System;

namespace Application.Common
{
    public class CommandResult
    {
        public CommandResult(object result)
        {
            Result = result;
        }

        public object Result { get; }
    }
}