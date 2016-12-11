using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class TodoAccessDeniedException: Exception
    {
        public TodoAccessDeniedException()
        {
        }

        public TodoAccessDeniedException(string message)
        : base(message)
        {
        }

        public TodoAccessDeniedException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
