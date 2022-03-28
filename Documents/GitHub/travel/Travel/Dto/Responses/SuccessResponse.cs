using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Dto.Responses
{
    public class SuccessResponse : AppResponse
    {
        public SuccessResponse() : base(true)
        {
        }

        public SuccessResponse(string message) : base(true, message)
        {
        }
    }
}
