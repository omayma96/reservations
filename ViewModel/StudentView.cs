using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.ViewModel
{
    public class StudentView
    {
        public int id { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }
        public string Classe { get; set; }

        public int AbsCount { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }


        public string Type { get; set; }

        public int ReservationTypeId { get; set; }
       
        public string ReservationUserId { get; set; }
        public DateTime DateC { get; set; }
        public IdentityUser ReservationUser { get; set; }
    }
}
