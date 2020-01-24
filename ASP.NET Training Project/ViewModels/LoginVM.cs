using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Training_Project.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "You need to enter a username.")]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You need to enter a password.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
