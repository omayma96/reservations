using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Errors
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException(string message) : base(message)
        {
        }

        public PermissionDeniedException()
        {
        }
    }
}
