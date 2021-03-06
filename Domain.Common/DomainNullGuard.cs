﻿namespace Domain.Common
{
    public static class DomainNullGuard
    {
        public static void Require(this Entity entity, bool condition)
        {
            if (!condition)
            {
                throw new NullGuardException($"{entity.GetType().Name}: Precondition failed" );
            }
        }
    }
}