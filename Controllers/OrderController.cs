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
    public class OrderController : Controller
    {

        public IActionResult ExecuteOrder()
        {
            return View();
        }

        public IActionResult SeeBooking(string bookingID)
        {
            BookingModel booking = new BookingModel();
            booking.BookingID = Convert.ToInt32(bookingID);
            return View("ExecuteOrder", booking);
        }

        public IActionResult ReturnDevices()
        {
            return View();
        }
        
        public IActionResult EditOrder()
        {
            return View();
        }

    }
}
