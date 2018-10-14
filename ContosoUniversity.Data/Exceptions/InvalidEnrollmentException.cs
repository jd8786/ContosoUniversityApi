using System;
using System.Runtime.Serialization;

namespace ContosoUniversity.Data.Exceptions
{
    public class InvalidEnrollmentException: Exception
    {
        public InvalidEnrollmentException()
        {
        }

        public InvalidEnrollmentException(string message) : base(message)
        {
        }

        public InvalidEnrollmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidEnrollmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
