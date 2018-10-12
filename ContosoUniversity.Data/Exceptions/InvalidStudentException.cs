using System;
using System.Runtime.Serialization;

namespace ContosoUniversity.Data.Exceptions
{
    public class InvalidStudentException: Exception
    {
        public InvalidStudentException()
        {
        }

        public InvalidStudentException(string message) : base(message)
        {
        }

        public InvalidStudentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidStudentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
