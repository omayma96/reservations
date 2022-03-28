using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Models
{
    public class Category 
    {
       
        public int CatID { get; set; }
        public string Nom { get; set; }
        public int CountProd { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public ICollection<CategoryImage> CategoryImages { get; set; }
    }
}
