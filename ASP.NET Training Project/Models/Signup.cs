﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Training_Project.Models
{
    public class Signup
    {
        [Required]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        public string eMail { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
