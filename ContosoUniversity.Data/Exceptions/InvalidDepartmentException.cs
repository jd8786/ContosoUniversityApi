using System;
using System.Runtime.Serialization;

namespace ContosoUniversity.Data.Exceptions
{
    public class InvalidDepartmentException : Exception
    {
        public InvalidDepartmentException()
        {
        }

        public InvalidDepartmentException(string message) : base(message)
        {
        }

        public InvalidDepartmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidDepartmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
