using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.Models
{
    public class ReservationViewModel
    {
        public int id { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "Nom")]
        public string Fname { get; set; }

        [Display(Name = "Prenom")]
        public string Lname { get; set; }
        public string Classe { get; set; }
        public ReservationType Type { get; set; }
        public ReservationUser student { get; set; }
        public string Status { get; set; }

        [Display(Name = "Absence")]
        public int absentcount { get; set; }


    }
}
