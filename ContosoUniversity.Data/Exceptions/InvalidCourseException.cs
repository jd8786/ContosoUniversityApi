using System;
using System.Runtime.Serialization;

namespace ContosoUniversity.Data.Exceptions
{
    public class InvalidCourseException: Exception
    {
        public InvalidCourseException()
        {
        }

        public InvalidCourseException(string message) : base(message)
        {
        }

        public InvalidCourseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidCourseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
