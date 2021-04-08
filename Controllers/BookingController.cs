using Microsoft.AspNetCore.Mvc;
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
        
        public IActionResult BookingRUD()
        {
            return View();
        }

        public IActionResult MyBookings()
        {
            return View();
        }

        
        public IActionResult ProcessDeviceForBooking(string deviceID, string bookingID)
        {
            // FIrst, try and see if it's possible to convert and then do so.
            if (int.TryParse(deviceID, out int deviceIDInteger))
            {
                DBManagerBooking dBManager = new DBManagerBooking(configuration);
                DBManagerDevice dBManagerDevice = new DBManagerDevice(configuration);

                // Second, find out if the deviceID is a valid one (if it exists and isn't Disabled)
                DeviceModel device = dBManagerDevice.GetDeviceInfoWithLocation(deviceIDInteger);
                BookingModel booking = dBManager.GetBooking(int.Parse(bookingID));
                booking.Devices = dBManager.GetBookedDevices(int.Parse(bookingID));

                if (device.Model.ModelName != null)
                {
                    // Find out if you're supposed to Create the bookedDevice (
                    if(booking.DeliveredBy == null)
                    {
                        // Check if the Device is still booked somewhere else
                        // Create BookedDevice
                    }
                    else
                    {
                        //  or if you're supposed to Update the bookedDevice.. if it's in the booking????
                        // Update BookedDevice to be Returned
                    }
                }
                else
                {
                    // This device does not exist, or is disabled
                }
            }
            else
            {
                // It is not possible to convert the input to an integer, therefore we can do nothing with it.
            }


            return GoToScanDevices(bookingID);
        }


        /// <summary>
        /// Takes you to the BookedDevicesCRU of the booking you want to add/return BookedDevices to.
        /// </summary>
        /// <param name="bookingID">BookingID of the booking you want to add/return devices</param>
        /// <returns></returns>
        public IActionResult GoToScanDevices(string bookingID)
        {
            DBManagerBooking dBManager = new DBManagerBooking(configuration);

            // Acquiring the booking itself (Does not fill DeviceModel list or ItemLineModel list)
            BookingModel booking = dBManager.GetBooking(Convert.ToInt32(bookingID));

            // Acquiring the models requested for the booking
            booking.Items = dBManager.GetItemLines(booking.BookingID);
            // Acquiring the existing, if any, bookedDevices for the booking
            booking.Devices = dBManager.GetBookedDevices(booking.BookingID);

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

            // This is to ensure, that even BookedDevice models which haven't been ordered are represented.
            bool included;
            foreach (DeviceModel device in booking.Devices)
            {
                included = false;
                foreach (ItemLineModel item in booking.Items)
                {
                    // If the Device-in-question's Model already exists in the booking ItemLines, then...
                    if (device.Model.ModelName == item.Model.ModelName)
                    {
                        // It is Included, and there's no reason to compare it against the rest of the rest of the ItemLines, thus "break;"
                        included = true;
                        break;
                    }
                }
                if (!included)
                {
                    // If this Device's Model is not Included in the Booking's ItemLines, then it is added, with 0 as the requested quantity.
                    booking.Items.Add(new ItemLineModel(0, device.Model));
                }
            }

            // StoredLocation for each requested device modelName.
            Dictionary<string, StorageLocationModel> storageLocations = new Dictionary<string, StorageLocationModel>();
            foreach (ItemLineModel ilm in orderedModels)
            {
                storageLocations.Add(ilm.Model.ModelName, dBManager.GetModelLocation(ilm.Model.ModelName));
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
