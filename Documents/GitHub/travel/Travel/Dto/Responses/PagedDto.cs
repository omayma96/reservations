using Travel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Dto.Responses
{
    public class PagedDto : SuccessResponse
    {
        public PageMeta PageMeta { get; set; }
    }
   
}
