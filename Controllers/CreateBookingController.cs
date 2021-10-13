using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Controllers
{
    public class CreateBookingController : Controller
    {
        private readonly IConfiguration configuration;


        public CreateBookingController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult InventorySearch()
        {
            return View();
        }

       public IActionResult CreateBooking()
        {
            return View();
        }
    }
}
