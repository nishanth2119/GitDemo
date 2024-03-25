﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservationApplication.Models
{
    public class AuthModel
    {
        [Display(Name = "Email ID")]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
