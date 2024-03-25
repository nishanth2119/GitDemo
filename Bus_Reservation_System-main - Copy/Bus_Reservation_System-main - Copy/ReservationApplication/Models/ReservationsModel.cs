using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationApplication.Models
{
    public class ReservationsModel
    {
        public BusDetails busDetails { get; set; }

        public ScheduleDetails scheduleDetails { get; set; }

        public BookingDetails bookingDetails { get; set; }
    }
}