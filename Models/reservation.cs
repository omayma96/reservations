using reservation_system.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace reservation_system.Models
{
    public  class Reservation
    {
        
        public int id { get; set; }
        public DateTime Date { get; set; } = System.DateTime.Now;
        public string Status { get; set; }
   
        [ForeignKey("studentId,reservationId")]
        public string ReservationUserId { get; set; }
        public ReservationUser Student { get; set; }
        public ReservationType  TypeReservation { get; set; }
        public int reservationId { get; set; }


    }
}
