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
using Microsoft.Extensions.Configuration;


namespace HUS_project.Controllers
{
    public class BookingController : Controller
    {
        private readonly IConfiguration configuration;

        // constructor of bookingcontroller
        public BookingController(IConfiguration config)
        {
            this.configuration = config;
        }


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
            DBManagerBooking dBManager = new DBManagerBooking(configuration);

            // Acquiring the booking itself (Does not fill DeviceModel list or ItemLineModel list)
            BookingModel booking = dBManager.GetBooking(Convert.ToInt32(bookingID));

            // Acquiring the models requested for the booking
            booking.Items = dBManager.GetItemLines(booking.BookingID);

            // List of models, and how many devices of it are available in storage.
            List<ItemLineModel> orderedModels = new List<ItemLineModel>();
            
            // FIlling orderedModels
            foreach(ItemLineModel ilm in booking.Items)
            {
                orderedModels.Add(new ItemLineModel(
                    dBManager.GetCountDevicesOfModelInStorage(ilm.Model.ModelName),
                    ilm.Model)
                    );
            }

            // StoredLocation for each requested device Model.
            Dictionary<ItemLineModel, StorageLocationModel> storageLocations = new Dictionary<ItemLineModel, StorageLocationModel>();
            foreach(ItemLineModel ilm in orderedModels)
            {
                storageLocations.Add(ilm, dBManager.GetModelLocation(ilm.Model.ModelName));
            }

            // Creation and filling of ViewModel for BookedDevicesCreateReadUpdate
            BookedDevicesCRUModel bookedDevicesCRUModel = new BookedDevicesCRUModel(
                booking,
                orderedModels,
                storageLocations
                );
            return View("BookedDevicesCRU", bookedDevicesCRUModel);
        }


        public IActionResult GoToBooking(string bookingID)
        {
            DBManagerBooking dBManager = new DBManagerBooking(configuration);
            BookingModel booking = dBManager.GetBooking(Convert.ToInt32(bookingID));

            return View("BookingRUD", booking);
        }
    }
}
