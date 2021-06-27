using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.Models
{
    public class ReservationUser : IdentityUser
    {
        [Required(ErrorMessage = "Entrer votre nom !!")]
        [Display(Name = "Nom")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Entrer votre prénom !")]
        [Display(Name = "Prenom")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Entrer votre Classe !")]
      
        public string Classe { get; set; }

        [Display(Name = "Nbr d'absence")]
        public int abscount { get; set; }
        public string status { get; set; }

        public string statusAbs { get; set; }

    }
}
