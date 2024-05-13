using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Invalid username or password")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
