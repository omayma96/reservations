using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Models
{
    public class CategoryImage : FileUpload
    {
        public Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}
