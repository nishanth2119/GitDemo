using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationApplication.Models;
namespace ReservationApplication.Controllers
{
    
    public class ReservationsController : Controller
    {
        BusReservationEntities db = new BusReservationEntities();

        // GET: Reservations
        public ActionResult Reservations()
        {
            List<BookingDetails> booking = new List<BookingDetails>();
            int regId;
            if (db.UserDetail.Single(x => x.EmailId == User.Identity.Name).Role.ToLower() != "admin")
            {
                regId = db.UserDetail.Single(x => x.EmailId == User.Identity.Name).RegId;
                booking = (from bd in db.BookingDetails where bd.RegId == regId select bd).ToList();
            }
            else
            {
                booking = db.BookingDetails.ToList();
            }
            
            
            List<ReservationsModel> reservations = new List<ReservationsModel>();
            foreach (var item in booking)
            {
                ReservationsModel res = new ReservationsModel();
                res.bookingDetails = item;
                res.busDetails = db.BusDetails.SingleOrDefault(x => x.BusId == item.BusId);
                if (res.busDetails == null)
                {
                    continue;
                }
                res.scheduleDetails = db.ScheduleDetails.SingleOrDefault(x => x.ScheduleId == item.Schedule);
                if (res.scheduleDetails == null)
                {
                    continue;
                }
                reservations.Add(res);
            }
            return View(reservations);
        }

        public ActionResult ReservationDetails(int Id)
        {
            ReservationsModel reservation = new ReservationsModel();
            reservation.bookingDetails = db.BookingDetails.Single(x => x.BookingId == Id);
            reservation.busDetails = db.BusDetails.Single(x => x.BusId == reservation.bookingDetails.BusId);
            reservation.scheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == reservation.bookingDetails.Schedule);

            return View(reservation);
        }

        public ActionResult ReservationDelete(int Id)
        {
            ReservationsModel reservation = new ReservationsModel();
            reservation.bookingDetails = db.BookingDetails.Single(x => x.BookingId == Id);
            reservation.busDetails = db.BusDetails.Single(x => x.BusId == reservation.bookingDetails.BusId);
            reservation.scheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == reservation.bookingDetails.Schedule);

            return View(reservation);
        }
        [HttpPost]
        [ActionName("ReservationDelete")]
        public ActionResult ReservationDeleteConfirm(int Id)
        {
            BookingDetails bookingDetails = db.BookingDetails.Single(x => x.BookingId == Id);
            db.BookingDetails.Remove(bookingDetails);

            ScheduleDetails scheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == bookingDetails.Schedule);
            scheduleDetails.BookedSeats -= 1;
            scheduleDetails.AvailableSeats += 1;
            db.Entry(scheduleDetails).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Reservations");
        }
    }
}