using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reservation_system.Data;
using reservation_system.Models;
using reservation_system.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin ")]

    public class ReservationsTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IReservationTypeService _reservationTypeService;

        public ReservationsTypeController(IReservationTypeService reservationTypeService)
        {
            _reservationTypeService = reservationTypeService;
        }

        public async Task<string> GetReservationTypeById(int ResID)
        {
            var result = await _reservationTypeService.GetReservationTypebyId(ResID);
            return result;
        }

        public async Task<ReservationType> GetReservationTypeDetails(int ResID)
        {
            var result = await _reservationTypeService.GetReservationTypeDetails(ResID);
            return result;
        }
        public ReservationsTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ReservationTypeController
        public ActionResult Index()
        {
            var AllReservationType = _context.ReservationsType.ToList();
            return View(AllReservationType);
            
        }

        // GET: ReservationTypeController/Create
        public ActionResult AddType()
        {
            return UpdateType(0);
        }

        // POST: ReservationTypeController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddType(ReservationType NameReservation)
        {
            try
            {

                _context.ReservationsType.Add(NameReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

  
        // GET: ReservationTypeController/Edit/5
        public ActionResult UpdateType(int id)
        {
            var NamereservationT = _context.ReservationsType.Find(id);
            return View(NamereservationT);
        }

        // POST: ReservationTypeController/Edit/5
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateType(int id, ReservationType UpdtReservation)
        {
            try
            {
                _context.Update(UpdtReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationTypeController/Delete/5
        public ActionResult Delete(int id, ReservationType DeleteReservation)
        {
            var dlttype = _context.ReservationsType.Find(id);
            return View(dlttype);
        }

        // POST: TypeReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deletetype = _context.ReservationsType.Find(id);
                _context.ReservationsType.Remove(deletetype);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
