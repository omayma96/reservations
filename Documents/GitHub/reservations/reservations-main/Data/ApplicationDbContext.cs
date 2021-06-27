using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using reservation_system.Migrations;
using reservation_system.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace reservation_system.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ReservationUser> ReservationUsers { get; set; }

           public virtual DbSet<Reservation> Reservations { get; set; }

           public virtual DbSet<ReservationType> ReservationsType { get; set; }
           public DbSet<reservation_system.Models.ReservationViewModel> ReservationViewmodel { get; set; }

  

    }
}
