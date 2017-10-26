using System;

namespace Domain.Common
{
    public class NullGuardException : Exception
    {
        public NullGuardException(string msg):base(msg)
        {
            
        }
    }
}