using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationApplication.Models
{
    public class SelectBusListModel
    {
        public int BusId { get; set; }

        public int ScheduleId { get; set; }

        public string BusType { get; set; }

        public int AvailableSeats { get; set; }

        public System.TimeSpan DepartTime { get; set; }
    }
}