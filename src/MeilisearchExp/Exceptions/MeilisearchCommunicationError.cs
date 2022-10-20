using System;

namespace MeilisearchExp.Exceptions
{
    public class MeilisearchCommunicationError : Exception
    {
        public MeilisearchCommunicationError(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}