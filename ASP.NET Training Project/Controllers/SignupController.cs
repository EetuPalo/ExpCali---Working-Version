using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Training_Project.Controllers
{
    public class SignupController : Controller
    {
        [Route("signup")]
        public IActionResult Index()
        {
            return View();
        }
    }
}