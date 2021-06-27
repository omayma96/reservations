using Microsoft.EntityFrameworkCore;
using reservation_system.Data;
using reservation_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.Services
{
    public class ReservationTypeService : IReservationTypeService
    {
        private readonly ApplicationDbContext _appDbContext;


       
        public ReservationTypeService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string> GetReservationTypebyId(int ResID)
        {
            var name = await _appDbContext.ReservationsType.Where(c => c.id == ResID).Select(d => d.type).FirstOrDefaultAsync();
            return name;
        }

        public async Task<ReservationType> GetReservationTypeDetails(int ResID)
        {
            var res = await _appDbContext.ReservationsType.FirstOrDefaultAsync(c => c.id == ResID);
            return res;
        }
    }
}
