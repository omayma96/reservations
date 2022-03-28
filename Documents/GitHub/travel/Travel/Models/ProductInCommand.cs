using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Models
{
    public class ProductInCommand
    {
        public int ProductID { get; set; }
        public Product Products { get; set; }

        public int CommandID { get; set; }
        public Command Commands { get; set; }
    }
}
