using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Training_Project.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
