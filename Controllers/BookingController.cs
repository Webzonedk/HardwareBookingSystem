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

        /// <summary>
        /// Contextively processes what to do with this device id for this booking id. Be it create BookedDevice, return BookedDevice, or even delete.
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        public IActionResult ProcessDeviceForBooking(string deviceID, string bookingID)
        {
            HttpContext.Session.SetString("bookedDeviceError", "");
            // FIrst, try and see if it's possible to convert and then do so.
            if (int.TryParse(deviceID, out int deviceIDInteger))
            {
                DBManagerBooking dBManager = new DBManagerBooking(configuration);
                DBManagerDevice dBManagerDevice = new DBManagerDevice(configuration);

                // Second, find out if the deviceID is a valid one (if it exists and is Enabled (status = 1))
                DeviceModel device = dBManagerDevice.GetDeviceInfoWithLocation(deviceIDInteger);
                BookingModel booking = dBManager.GetBooking(int.Parse(bookingID));
                booking.Devices = dBManager.GetBookedDevices(int.Parse(bookingID));

                if (device.Model.ModelName != null && device.Status == 1 && booking.BookingStatus == 1)
                {
                    // Find out if you're supposed to Create the bookedDevice, or undo the bookedDevice or return the device
                    bool deviceInBookingAlready = false;
                    foreach(DeviceModel device1 in booking.Devices)
                    {
                        if(device1.DeviceID == device.DeviceID)
                        {
                            if (booking.DeliveredBy == null)
                            {
                                // The delivery for this booking has not been made yet, ergo the bookedDevice may be Deleted. An Undo.
                                // "DeleteBookedDevice"
                                dBManager.DeleteBookedDevice(device.DeviceID, booking.BookingID);
                            }
                            else if(device1.ReturnedBy != null && device1.ReturnedBy != "")
                            {
                                // Delivery has already been made. Update BookedDevice to be Returned.
                                // "ReturnBookedDevice"
                                dBManager.ReturnBookedDevice(device.DeviceID, booking.BookingID, HttpContext.Session.GetString("uniLogin"));
                            }
                            else
                            {
                                // This device has already been returned
                                HttpContext.Session.SetString("bookedDeviceError", "Denne Enhed er allerede blevet returneret.");

                            }
                            deviceInBookingAlready = true;
                            break;
                        }
                    }


                    if (!deviceInBookingAlready)
                    {
                        // THe device does not already exist for this booking, therefore it should be created.. if the device is available.
                        // CreateBookedDevice() will perform an availability check on its own.
                        if(!dBManager.CreateBookedDevice(device.DeviceID, booking.BookingID))
                        {
                            // The requested device is not available! Perhaps not returned from another Booking, or being repaired.
                            HttpContext.Session.SetString("bookedDeviceError", "Denne enhed er allerede udlånt til en anden bestilling, eller er på værkstedet.");
                        }
                    }
                }
                else
                {
                    // This device does not exist or is Disabled
                    HttpContext.Session.SetString("bookedDeviceError", "Denne enhed eksisterer ikke, eller er slået fra. Eller denne booking er deaktiveret.");
                }
            }
            else
            {
                // It is not possible to convert the input to an integer, therefore we can do nothing with it.
                HttpContext.Session.SetString("bookedDeviceError", "Input kunne ikke konverteres til et helt tal.");

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
