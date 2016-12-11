using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException()
        {
        }

        public DuplicateTodoItemException(string message)
        : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
