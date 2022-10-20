using System;

namespace MeilisearchExp.Exceptions
{
    public class MeilisearchTimeoutError : Exception
    {
        public MeilisearchTimeoutError(string message)
            : base(message)
        {
        }
    }
}