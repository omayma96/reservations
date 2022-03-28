using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Models
{
    public class Command
    {
        public int CommandID { get; set; }
        public DateTime Date { get; set; } = System.DateTime.Now;
        public string Status { get; set; }

        public ApplicationUser ApplicationUsers { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }

        public virtual ICollection<ProductInCommand> ProductInCommands { get; set; }
    }
}
