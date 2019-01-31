using System;
using System.Runtime.Serialization;

namespace ContosoUniversity.Data.Exceptions
{
    public class InvalidInstructorException : Exception
    {
        public InvalidInstructorException()
        {
        }

        public InvalidInstructorException(string message) : base(message)
        {
        }

        public InvalidInstructorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidInstructorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
