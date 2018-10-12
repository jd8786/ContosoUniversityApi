using System;
using System.Runtime.Serialization;

namespace ContosoUniversity.Data.Exceptions
{
    public class FailedToSaveDatabaseException : Exception
    {
        public FailedToSaveDatabaseException()
        {
        }

        public FailedToSaveDatabaseException(string message) : base(message)
        {
        }

        public FailedToSaveDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FailedToSaveDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}   