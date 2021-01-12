using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HUS_project.Models;
using HUS_project.DAL;
using Microsoft.AspNetCore.Http;

namespace HUS_project.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Booking()
        {
            return View();
        }
       
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Inspect()
        {
            return View();
        }

        public IActionResult Overview()
        {
            return View();
        }



    }
}
