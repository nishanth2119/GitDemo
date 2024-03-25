using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationApplication.Models;

namespace ReservationApplication.Controllers
{
    public class UserController : Controller
    {
        List<SelectListItem> ObjItem = new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Aadhar Card", Value="1"},
                new SelectListItem{ Text = "Pan Card", Value="2"},
                new SelectListItem{ Text = "Election Card", Value="3"},
            };

        BusReservationEntities db = new BusReservationEntities();
        // GET: User
        public ActionResult SelectBus()
        {
            ViewData["ListItem"] = new List<SelectBusListModel>();
            return View();
        }
        [HttpPost]
        public ActionResult SelectBus(SelectBusModel model)
        {
            List<ScheduleDetails> schedule = db.ScheduleDetails.ToList();
            List<SelectBusListModel> ObjItem = new List<SelectBusListModel>();
            
            foreach (ScheduleDetails ele in schedule)
            {
                if (ele.Origin == model.Origin && ele.Destination == model.Destination && ele.Date == model.Date)
                {
                    ObjItem.Add(new SelectBusListModel
                    {
                        BusId = ele.BusId,
                        ScheduleId  = ele.ScheduleId,
                        BusType = db.BusDetails.Single(x => x.BusId == ele.BusId).BusType,
                        AvailableSeats = ele.AvailableSeats,
                        DepartTime = ele.DepartTime
                    });
                }
            }
            ViewData["ListItem"] = ObjItem;
            return View();
        }
        public ActionResult ReserveSeats(int BusId, int ScheduleId)
        {
            TempData["BusId"] = BusId;
            TempData["ScheduleId"] = ScheduleId;
            ViewBag.BusDetails = db.BusDetails.Single(x => x.BusId == BusId);
            ViewBag.ScheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == ScheduleId);
            ViewData["ListItem"] = this.ObjItem;
            return View();
        }
        [HttpPost]
        public ActionResult ReserveSeats(FormCollection form)
        {
            if (form["fname1"] != "" & form["lname1"] != "")
            {
                this.ConfirmReservation(form["fname1"], form["lname1"], form["idproof1"]);
            }
            if (form["fname2"] != "" & form["lname2"] != "")
            {
                this.ConfirmReservation(form["fname2"], form["lname2"], form["idproof2"]);
            }
            if (form["fname3"] != "" & form["lname3"] != "")
            {
                this.ConfirmReservation(form["fname3"], form["lname3"], form["idproof3"]);
            }
            if (form["fname4"] != "" & form["lname4"] != "")
            {
                this.ConfirmReservation(form["fname4"], form["lname4"], form["idproof4"]);
            }
            if (form["fname5"] != "" & form["lname5"] != "")
            {
                this.ConfirmReservation(form["fname5"], form["lname5"], form["idproof5"]);
            }
            if (form["fname6"] != "" & form["lname6"] != "")
            {
                this.ConfirmReservation(form["fname6"], form["lname6"], form["idproof6"]);
            }
            return RedirectToAction("Reservations", "Reservations");

        }
        private void ConfirmReservation(string fname, string lname, string idproof)
        {
            BookingDetails booking = new BookingDetails();
            booking.RegId = db.UserDetail.Single(x => x.EmailId == User.Identity.Name).RegId;
            booking.BusId = (int)TempData["BusId"];
            booking.Schedule = (int)TempData["ScheduleId"];
            booking.Fname = fname;
            booking.Lname = lname;
            booking.IdProof = this.ObjItem.Single(x => x.Value == idproof).Text;
            booking.SeatNo = db.ScheduleDetails.Single(x => x.ScheduleId == booking.Schedule).BookedSeats + 1;
            db.BookingDetails.Add(booking);

            ScheduleDetails schedule = db.ScheduleDetails.Single(x => x.ScheduleId == booking.Schedule);
            schedule.BookedSeats += 1;
            schedule.AvailableSeats -= 1;
            db.Entry(schedule).State = EntityState.Modified;
            db.SaveChanges();
        }





    }
}