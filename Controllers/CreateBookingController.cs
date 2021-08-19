using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Controllers
{
    public class CreateBookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
