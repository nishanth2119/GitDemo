using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationApplication.Models;

namespace ReservationApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        BusReservationEntities db = new BusReservationEntities();

        public ActionResult AddBus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBus(BusDetails model)
        {
            db.BusDetails.Add(model);
            db.SaveChanges();
            return RedirectToAction("BusList");
        }

        public ActionResult BusList()
        {
            List<BusDetails> bus = db.BusDetails.ToList();
            return View(bus);
        }

        public ActionResult EditBus(int id)
        {
            BusDetails busDetails = db.BusDetails.Single(x => x.BusId == id);
            return View(busDetails);
        }

        [HttpPost]
        public ActionResult EditBus(BusDetails model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusList");
            }
            return View(model);
        }

        public ActionResult DetailBus(int id)
        {
            BusDetails busDetails = db.BusDetails.Single(x => x.BusId == id);
            return View(busDetails);
        }

        public ActionResult DeleteBus(int id)
        {
            BusDetails busDetails = db.BusDetails.Single(x => x.BusId == id);
            return View(busDetails);
        }

        [HttpPost]
        [ActionName("DeleteBus")]
        public ActionResult DeleteBusConfirm(int id)
        {
            BusDetails busDetails = db.BusDetails.Single(x => x.BusId == id);
            db.BusDetails.Remove(busDetails);
            db.SaveChanges();
            return RedirectToAction("BusList");
        }

        public ActionResult ScheduleBus()
        {
            List<BusDetails> bus = db.BusDetails.ToList();
            List<SelectListItem> ObjItem = new List<SelectListItem>();
            foreach (BusDetails ele in bus)
            {
                ObjItem.Add(new SelectListItem { Text = ele.BusNo, Value = ele.BusId.ToString() });
            }
            ViewData["ListItem"] = ObjItem;
            return View();
        }

        [HttpPost]
        public ActionResult ScheduleBus(ScheduleDetails model)
        {
            model.BookedSeats = 0;
            model.AvailableSeats = db.BusDetails.Single(x => x.BusId == model.BusId).TotalSeats;
            db.ScheduleDetails.Add(model);
            db.SaveChanges();
            return RedirectToAction("ScheduleBusList");

        }
        public ActionResult ScheduleBusList()
        {
            List<ScheduleDetails> schedules = db.ScheduleDetails.ToList();
            return View(schedules);
        }
        public ActionResult EditSchedule(int id)
        {
            ScheduleDetails scheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == id);
            List<BusDetails> bus = db.BusDetails.ToList();
            List<SelectListItem> ObjItem = new List<SelectListItem>();
            foreach (BusDetails ele in bus)
            {
                ObjItem.Add(new SelectListItem { Text = ele.BusNo, Value = ele.BusId.ToString() });
            }
            ViewData["ListItem"] = ObjItem;

            return View(scheduleDetails);
        }
        [HttpPost]
        public ActionResult EditSchedule(ScheduleDetails model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ScheduleBusList");
            }
            return View(model);
        }

        public ActionResult DetailsSchedule(int Id)
        {
            ScheduleDetails scheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == Id);
            return View(scheduleDetails);
        }

        public ActionResult DeleteSchedule(int Id)
        {
            ScheduleDetails scheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == Id);
            return View(scheduleDetails);
        }
        [HttpPost]
        [ActionName("DeleteSchedule")]
        public ActionResult ScheduleDeleteConfirm(int Id)
        {
            ScheduleDetails scheduleDetails = db.ScheduleDetails.Single(x => x.ScheduleId == Id);
            db.ScheduleDetails.Remove(scheduleDetails);
            db.SaveChanges();
            return RedirectToAction("ScheduleBusList");
        }
    }
}
