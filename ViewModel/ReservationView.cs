using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.ViewModel
{
    public class ReservationView
    {
        public string id { get; set; }
        public DateTime Date { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Status { get; set; }
       
    }
}
