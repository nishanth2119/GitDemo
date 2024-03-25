using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationApplication.Models
{
    public class SelectBusModel
    {
        public string Origin { get; set; }

        public string Destination { get; set; }

        public System.DateTime Date { get; set; }
    }
}