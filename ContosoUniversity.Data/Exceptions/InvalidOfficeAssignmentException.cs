using System;
using System.Runtime.Serialization;

namespace ContosoUniversity.Data.Exceptions
{
    public class InvalidOfficeAssignmentException : Exception
    {
        public InvalidOfficeAssignmentException()
        {
        }

        public InvalidOfficeAssignmentException(string message) : base(message)
        {
        }

        public InvalidOfficeAssignmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidOfficeAssignmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
