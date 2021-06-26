using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.Models
{
    public class ReservationUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        public string Fname { get; set; }

        [Display(Name = "prenom")]
        public string Lname { get; set; }

        [Display(Name = "Role")]
        public string roleName { get; set; }

        public int AbsCount { get; set; }

        public string Classe { get; set; }
        public string Email { get; set; }
       
    }
}
