using reservation_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.Services
{
    public interface IReservationTypeService
    {
        Task<string> GetReservationTypebyId(int ResID);
        Task<ReservationType> GetReservationTypeDetails(int ResID);
    }
}
