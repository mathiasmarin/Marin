using System;

namespace Utils
{
    internal class PreconditionException : Exception
    {
        public PreconditionException()
        {
        }

        public PreconditionException(string message) : base(message)
        {
        }
    }
}