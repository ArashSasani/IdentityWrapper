using System;

namespace WebApplication.Infrastructure
{
    public class LogicalException : Exception
    {
        public LogicalException()
        {
        }

        public LogicalException(string message)
            : base(message)
        {
        }

        public LogicalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public override string Message
        {
            get { return AppSettings.BAD_REQUEST_ERROR_MESSAGE + " [LE]"; }
            //return "Logical error happend in the application stack."; }
        }


    }
}
