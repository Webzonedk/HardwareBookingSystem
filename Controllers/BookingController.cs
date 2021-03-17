﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HUS_project.Models;
using HUS_project.Models.ViewModels;
using HUS_project.DAL;
using Microsoft.AspNetCore.Http;

namespace HUS_project.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult CreateBooking()
        {
            return View();
        }
        
        public IActionResult BookingRUD()
        {
            return View();
        }

        public IActionResult MyBookings()
        {
            return View();
        }


        /// <summary>
        /// Takes you to the BookedDevicesCRUD of the booking you want to add/return BookedDevices to.
        /// </summary>
        /// <param name="bookingID">BookingID of the bookign you want to add/remove book</param>
        /// <returns></returns>
        public IActionResult GoToScanDevices(string bookingID)
        {
            BookingModel booking = new BookingModel();
            booking.BookingID = Convert.ToInt32(bookingID);

            BookedDevicesCRUDModel bookedDevicesCRUDModel = new BookedDevicesCRUDModel(
                booking,
                new List<ItemLineModel>(),
                new Dictionary<ItemLineModel, StorageLocationModel>()
                );
            return View("BookedDevicesCRUD", bookedDevicesCRUDModel);
        }


        public IActionResult GoToBooking(string bookingID)
        {
            BookingModel booking = new BookingModel();

            return View("BookingRUD", booking);
        }
    }
}
