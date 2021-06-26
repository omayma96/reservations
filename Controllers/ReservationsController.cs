using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using reservation_system.Data;
using reservation_system.Migrations;
using reservation_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace reservation_system.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {

        private readonly ApplicationDbContext _Context;
        private readonly UserManager<ReservationUser> _userManager;
        private readonly IToastNotification toastNotification;

        public ReservationsController(ApplicationDbContext Context, UserManager<ReservationUser> userManager, IToastNotification toastNotification)
        {
            _Context = Context;
            _userManager = userManager;
            this.toastNotification = toastNotification;

        }

        // la liste de toutes les réservations , On ne prend pas en considération la date
        // Joiture de 2 tables pour avoir les infos des apprenants et ainsi leurs réservations
        [Authorize(Roles = " Admin , Staff")]

        public IActionResult Index()
        {
            
            var AllReservations = (from reservat in _Context.Reservations
                          join apprenants in _Context.ReservationUsers
                          on reservat.Student.Id equals apprenants.Id
                          select new ReservationViewModel
                          {
                              id = reservat.id,
                              Date = reservat.Date,
                              Fname = apprenants.FName,
                              Lname = apprenants.LName,
                              Classe = apprenants.Classe,
                              Type = reservat.TypeReservation,
                              Status = reservat.Status,
                              absentcount = apprenants.abscount

                          }
                          ).ToList();
            return View(AllReservations);
        }

        // la liste des réservations pour aujourd'hui uniquement
        [Authorize(Roles = " Admin , Staff")]

        public IActionResult Today()
        {
            DateTime startDateTime = DateTime.Today.AddDays(0).AddTicks(-1);
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

            var ResTodayResult = (from reservat in _Context.Reservations
                                  join apprenants in _Context.ReservationUsers
                                    on reservat.Student.Id equals apprenants.Id
                                  where (reservat.Date > startDateTime && reservat.Date <= endDateTime)
                                  where reservat.Status == "approuved"
                                  select new ReservationViewModel
                            {
                              id = reservat.id,
                              Date = reservat.Date,
                              Fname = apprenants.FName,
                              Lname = apprenants.LName,
                              Classe = apprenants.Classe,
                              Type = reservat.TypeReservation,
                              Status = reservat.Status,
                              absentcount = apprenants.abscount


                           }
                          ).ToList();
            return View(ResTodayResult);
        }

        // la liste de réservations pour demain
        [Authorize(Roles = " Admin , Staff")]
        public IActionResult Demain()
        {
            DateTime startDateTime = DateTime.Today.AddDays(1).AddTicks(-1);
            DateTime endDateTime = DateTime.Today.AddDays(2).AddTicks(-1);

            var ResDemainResult = (from reservat in _Context.Reservations
                                   join apprenants in _Context.ReservationUsers
                                    on reservat.Student.Id equals apprenants.Id
                                   where (reservat.Date > startDateTime && reservat.Date <= endDateTime)
                          select new ReservationViewModel
                          {
                              id = reservat.id,
                              Date = reservat.Date,
                              Fname = apprenants.FName,
                              Lname = apprenants.LName,
                              Classe = apprenants.Classe,
                              Type = reservat.TypeReservation,
                              Status = reservat.Status,
                              absentcount = apprenants.abscount


                          }
                          ).ToList();
            return View(ResDemainResult);
        }

        //liste des réservations en attente de confirmation
        public IActionResult pending()
        {

            var pendingResult = (from reservat in _Context.Reservations
                                 join apprenants in _Context.ReservationUsers
                                    on reservat.Student.Id equals apprenants.Id
                                 where reservat.Status == "pending"
                          select new ReservationViewModel
                          {
                              id = reservat.id,
                              Date = reservat.Date,
                              Fname = apprenants.FName,
                              Lname = apprenants.LName,
                              Classe = apprenants.Classe,
                              Type = reservat.TypeReservation,
                              Status = reservat.Status,
                              absentcount = apprenants.abscount

                          }
                          ).ToList();
            
            return View(pendingResult);
        }

       

        // la liste des réservations d'un apprenant (dashboard student)
        [Authorize(Roles = " Student")]
        public IActionResult StudentIndex()
        {
            var ReservationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ReservationStudentResult = (from reservat in _Context.Reservations
                          join apprenants in _Context.ReservationUsers
                           on reservat.Student.Id equals apprenants.Id
                          where reservat.Student.Id == ReservationUserId
                          select new ReservationViewModel
                          {
                              id = reservat.id,
                              Date = reservat.Date,
                              Fname = apprenants.FName,
                              Lname = apprenants.LName,
                              Classe = apprenants.Classe,
                              Type = reservat.TypeReservation,
                              Status = reservat.Status,
                              absentcount = apprenants.abscount


                          }
                          ).ToList();

            return View(ReservationStudentResult);
        }


        // GET: ReservationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //Ajouter une nouvelle réservation 
        [Authorize(Roles = " Student")]
        public IActionResult Create()
        {
            return View();
        }

        // la création d'une réservation par l'apprenant
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Reservation crtreservation)
        {
            var Student = await _userManager.GetUserAsync(User);
           
            crtreservation.Student = Student;

            if (ModelState.IsValid)
            {
                _Context.Add(crtreservation);
                await _Context.SaveChangesAsync();
                toastNotification.AddSuccessToastMessage("Vous avez créer votre réservation, veillez attendre la confiramation de votre demande");
                return RedirectToAction("StudentIndex");

            }
            return View(crtreservation);
        }

        //la modification d'une réservation
        [Authorize(Roles = " Admin , Staff")]

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            var resrervationFait = _Context.Reservations.Find(Id);
            ViewBag.gettype = _Context.ReservationsType.ToList();
            if (resrervationFait.Status == "pending")
            {
                if (Id == null)
                {
                    return RedirectToAction("StudentIndex");
                }
                var all = await _Context.Reservations.FindAsync(Id);
                return View(all);

            }
            else
            {
                toastNotification.AddErrorToastMessage("Impossible de modifier cette modification !!");
            }
            return RedirectToAction("StudentIndex");
        }

        [Authorize(Roles = " Student")]
        [HttpPost]
        public async Task<IActionResult> Edit(Reservation updtReservation)
        {
            if (ModelState.IsValid)
            {
                _Context.Update(updtReservation);
                await _Context.SaveChangesAsync();
                toastNotification.AddSuccessToastMessage("Vous avez modifié votre demande !");
                return RedirectToAction("StudentIndex");
            }
            return View(updtReservation);
        }
        // Supprimer une réservation

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Delete(int? id)
        {
            var resrervationDelete = _Context.Reservations.Find(id);
            if (resrervationDelete.Status == "pending")
            {
                if (id == null)
                {
                    return RedirectToAction("StudentIndex");
                }
                var all = await _Context.Reservations.FindAsync(id);
                return View(all);
            }

            else
            {
                toastNotification.AddErrorToastMessage("Vous ne pouvez plus supprimer cette réservation !");
            }
            return RedirectToAction("StudentIndex");
        }
        [Authorize(Roles = " Student")]

        [HttpPost]
        public async Task<IActionResult> Delete(Reservation suppReservation)
        {
            if (ModelState.IsValid)
            {
                _Context.Remove(suppReservation);
                await _Context.SaveChangesAsync();
                toastNotification.AddSuccessToastMessage("Vous avez supprimé votre demande !");
                return RedirectToAction("StudentIndex");
            }
            return View(suppReservation);
        }


        [Authorize(Roles = " Admin ")]

        // confirmer une réservation
        public async Task<IActionResult> Confirmer(int id)
        {
            var resrvationConfirmée = _Context.Reservations.Find(id);
            if (resrvationConfirmée.Status != "approuved")
            {

                resrvationConfirmée.Status = "approuved";
                _Context.Update(resrvationConfirmée);
                await _Context.SaveChangesAsync();
                toastNotification.AddSuccessToastMessage("Vous avez validé cette réservation");
            }
            else
            {
                toastNotification.AddErrorToastMessage("Cette réservation est déjà confirmée");
            }

            return RedirectToAction("Demain");
        }
        [Authorize(Roles = " Admin ")]

        //Refuser une réservation
        public IActionResult Rejected(int id)
        {
            var resrvationRejected = _Context.Reservations.Find(id);

            if (resrvationRejected.Status != "Rejected")
            {
                resrvationRejected.Status = "Rejected";
                _Context.Update(resrvationRejected);
                _Context.SaveChanges();
                toastNotification.AddWarningToastMessage("Vous avez refusé cette réservation");

            }
            else
            {
                toastNotification.AddErrorToastMessage("cette réservation est déjà refusée");
            }

            return RedirectToAction("Demain");

        }

        [Authorize(Roles = " Admin , Staff")]
        //marquer l'absence
        public void Absence(int id)
        {
            var studentAbs = _Context.Reservations.Find(id);
            var u = _Context.ReservationUsers.FirstOrDefault(s => s.Id == studentAbs.ReservationUserId);
            var inc = studentAbs.Student.abscount;
            var st = studentAbs.Status;
            if (st != "present")
            {
                u.abscount = inc + 1;
                _Context.Update(studentAbs);
                _Context.Update(u);
                _Context.SaveChanges();
            }

        }
        [Authorize(Roles = " Admin , Staff")]

        public void presence(int id)
        {
            var studentAbs = _Context.Reservations.Find(id);
            var u = _Context.ReservationUsers.FirstOrDefault(s => s.Id == studentAbs.ReservationUserId);
            var inc = studentAbs.Student.abscount;
            var st = studentAbs.Status;
            if (st == "present")
            {
                u.abscount = inc + 0;
                _Context.Update(studentAbs);
                _Context.Update(u);
                _Context.SaveChanges();
            }

        }
        [Authorize(Policy = " Admin")]
        public void bloque(int id)
        {
            var studentAbs = _Context.Reservations.Find(id);
            var u = _Context.ReservationUsers.FirstOrDefault(s => s.Id == studentAbs.ReservationUserId);
            var inc = studentAbs.Student.abscount;
            var st = studentAbs.Status;
            if (st == "present")
            {
                u.abscount = inc + 0;
                _Context.Update(studentAbs);
                _Context.Update(u);
                _Context.SaveChanges();
            }

        }
    }
}
