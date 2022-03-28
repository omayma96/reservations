using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Errors
{
    public class UnexpectedApplicationStateException : Exception
    {
        public UnexpectedApplicationStateException(string message) : base(message)
        {

        }

        public UnexpectedApplicationStateException()
        {

        }
    }
}
