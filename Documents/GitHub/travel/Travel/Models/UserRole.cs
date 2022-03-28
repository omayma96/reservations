using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
