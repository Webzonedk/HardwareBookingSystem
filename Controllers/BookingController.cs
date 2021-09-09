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
                            else if(device1.ReturnedBy == null || device1.ReturnedBy == "")
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

            // List of models, and how many devices of it are available in storage.
            List<ItemLineModel> modelsInStorage = new List<ItemLineModel>();
            foreach (ItemLineModel ilm in booking.Items)
            {
                modelsInStorage.Add(new ItemLineModel(
                    dBManager.GetCountDevicesOfModelInStorage(ilm.Model.ModelName),
                    ilm.Model)
                    );
            }


            // StoredLocation for each requested device modelName.
            Dictionary<string, StorageLocationModel> storageLocations = new Dictionary<string, StorageLocationModel>();
            foreach (ItemLineModel ilm in booking.Items)
            {
                storageLocations.Add(ilm.Model.ModelName, dBManager.GetModelLocation(ilm.Model.ModelName));
            }

            // Creation and filling of ViewModel for BookedDevicesCreateReadUpdate
            BookedDevicesCRUModel bookedDevicesCRUModel = new BookedDevicesCRUModel(
                booking,
                modelsInStorage,
                storageLocations
                );
            return View("BookedDevicesCRU", bookedDevicesCRUModel);
        }

        /// <summary>
        /// Go to the booking edit page.
        /// </summary>
        /// <param name="bookingID"></param>
        /// <returns></returns>
        public IActionResult GoToBooking(string bookingID)
        {
            DBManagerBooking dBManager = new DBManagerBooking(configuration);
            BookingModel booking = dBManager.GetBooking(Convert.ToInt32(bookingID));
            booking.Items = dBManager.GetItemLines(booking.BookingID);
            booking.Devices = dBManager.GetBookedDevices(booking.BookingID);

            return View("BookingRUD", booking);
        }


        public IActionResult DeleteBooking(string bookingID)
        {
            return GoToBooking(bookingID);
        }

        /// <summary>
        /// Deletes the itemline for this booking and model, and deletes the booking if there are no more ItemLines.
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public IActionResult DeleteItemLine(BookingModel booking, string modelName)
        {
            DBManagerBooking dBManager = new DBManagerBooking(configuration);
            dBManager.DeleteItemLine(booking.BookingID, modelName);

            if(booking.Items.Count < 2)
            {
                // Delete booking also
                return DeleteBooking(booking.BookingID.ToString());
            }
            return GoToBooking(booking.BookingID.ToString());
        }

        /// <summary>
        /// Determines which kind of button was pressed and engagages the appropriate function.
        /// </summary>
        /// <param name="bookingModel"></param>
        /// <param name="location"></param>
        /// <param name="updateBooking"></param>
        /// <param name="disableBooking"></param>
        /// <param name="deleteItemLine"></param>
        /// <returns></returns>
        public IActionResult ProcessBookingEditSubmit(BookingModel bookingModel, string location, string updateBooking, string deleteBooking, string deleteItemLine)
        {
            if(updateBooking != null)
            {
                return UpdateBooking(bookingModel, location);
            }
            else if(deleteBooking != null)
            {
                // DISABLE or DELETE BOOKING
                return DeleteBooking(bookingModel.BookingID.ToString());
            }
            else if(deleteItemLine != null)
            {
                // Delete ItemLine, no fuss, maybe?
                return DeleteItemLine(bookingModel, deleteItemLine);
            }
            else
            {
                // Should Never happen, but refreshes page
                return GoToBooking(bookingModel.BookingID.ToString());
            }
        }


        /// <summary>
        /// Only updates startDate, returnDate, location and ItemLine.Quantity
        /// </summary>
        /// <param name="bookingModel"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public IActionResult UpdateBooking(BookingModel bookingModel, string location)
        {
            // First we check to see if anything needs changing with the booking itself

            DBManagerBooking dBManager = new DBManagerBooking(configuration);
            BookingModel originalBooking = dBManager.GetBooking(bookingModel.BookingID);
            originalBooking.Items = dBManager.GetItemLines(bookingModel.BookingID);
            string errorMessages = "Fejl:";
            

            if (HttpContext.Session.GetString("uniLogin") == originalBooking.Customer && originalBooking.PlannedReturnDate.Date > DateTime.Now.Date)
            {
                // Input Data checks, to see if the data inputted has valid shapes.
                DateTime newStartDate = bookingModel.PlannedBorrowDate;
                bool validNewStartDate = bookingModel.PlannedBorrowDate != null && newStartDate.Date > DateTime.Now.Date && newStartDate.Date != originalBooking.PlannedBorrowDate.Date;
                DateTime newReturnDate = bookingModel.PlannedReturnDate;
                bool validNewReturnDate = bookingModel.PlannedReturnDate != null && newReturnDate.Date != originalBooking.PlannedReturnDate.Date;

                BuildingModel newRoom = new BuildingModel();
                bool validNewLocation = false;
                if (location != null && location != "")
                {
                    try
                    {
                        newRoom = new BuildingModel(location.Split('.')[0], location.Split('.')[1]);
                        validNewLocation = newRoom.Building != originalBooking.Location.Building || newRoom.RoomNumber != originalBooking.Location.RoomNumber;
                    }
                    catch
                    {
                        errorMessages += ("\nLokale blev ikke ændret, da det ikke havde en valid struktur, så som: \"D.32\"");
                    }
                }

                // Determining if ItemLines changed.
                bool validItemLinesChange = false;
                foreach (ItemLineModel ilm in bookingModel.Items)
                {
                    foreach (ItemLineModel ilmO in originalBooking.Items)
                    {
                        if (ilm.Model.ModelName == ilmO.Model.ModelName)
                        {
                            if (ilm.Quantity != ilmO.Quantity)
                            {
                                // CHange Detected!
                                validItemLinesChange = true;
                            }
                            break;
                        }
                    }
                    if (validItemLinesChange)
                    {
                        break;
                    }
                }


                // Logic check, if the dates make somewhat basic sense in relation to now and each other (We don't approve of no time-travellers or non-euclideans).

                // If new Start date is valid so far, is earlier than new potential/existing return date, and isn't today or prior, it can still be changed.
                if (validNewStartDate && (validNewReturnDate ? newStartDate.Date > newReturnDate.Date : newStartDate.Date > originalBooking.PlannedReturnDate.Date))
                {
                    errorMessages += "\nStart Dato blev ikke ændret fordi start dato er efter slut dato";
                    validNewStartDate = false;
                }

                // If new return date is valid so far, is later than new potential/existing start date, and is later than Today, it can still be changed.
                if (validNewReturnDate && ((validNewStartDate ? newReturnDate.Date < newStartDate.Date : newReturnDate.Date < originalBooking.PlannedBorrowDate.Date) ||
                    newReturnDate.Date <= DateTime.Now.Date))
                {
                    errorMessages += "\nSlut Dato blev ikke ændret: slut dato er før start dato, eller er i dag eller før.";
                    validNewReturnDate = false;

                    // As validNewReturnDate suddenly turned false, newValidStartDate needs to check against existing ReturnDate.
                    if(validNewStartDate && newStartDate.Date > originalBooking.PlannedReturnDate.Date)
                    {
                        validNewStartDate = false;
                    }
                }


                // Database Check

                // Date & ItemLine.Quantity Database Check:
                if (validNewStartDate || validNewReturnDate || validItemLinesChange)
                {
                    foreach (ItemLineModel ilm in validItemLinesChange ? bookingModel.Items : originalBooking.Items)
                    {
                        // If the amount requested is greater than the amount available in that period, the dates cannot be changed.
                        int quantityAvailable = dBManager.GetModelQuantityAvailableExcludingBooking(validNewStartDate ? newStartDate : originalBooking.PlannedBorrowDate, validNewReturnDate ? newReturnDate : originalBooking.PlannedReturnDate, ilm.Model.ModelName, bookingModel.BookingID);
                        if (ilm.Quantity > quantityAvailable || ilm.Quantity <= 0)
                        {
                            validNewStartDate = false;
                            validNewReturnDate = false;
                            validItemLinesChange = false;

                            errorMessages += "\nVi har kun " + quantityAvailable + " stk. " + ilm.Model.ModelName + " ledige i den periode (Inklusiv dem allerede bestilt), eller du skal minimum bestille 1.";
                            break;
                        }
                    }
                }
                // Room Database Check:
                if (validNewLocation) 
                {
                    DBManagerAdministration dBMAdmin = new DBManagerAdministration(configuration);
                    validNewLocation = dBMAdmin.DoesRoomExist(newRoom);
                    if (!validNewLocation)
                    {
                        errorMessages += "\nLokale eksisterer ikke. F.eks. \"D.32\"";
                    }
                }


                // Apply Update as Appropriate
                if(validItemLinesChange || validNewStartDate || validNewReturnDate || validNewLocation)
                {
                    BookingModel finalBooking = new BookingModel(
                        originalBooking.BookingID, "", 
                        validItemLinesChange ? bookingModel.Items : originalBooking.Items,
                        new List<DeviceModel>(),
                        validNewLocation ? newRoom : originalBooking.Location,
                        validNewStartDate ? bookingModel.PlannedBorrowDate : originalBooking.PlannedBorrowDate, 
                        validNewReturnDate ? bookingModel.PlannedReturnDate : originalBooking.PlannedReturnDate, 1
                        );

                    int bookingLogID = dBManager.UpdateBookingAndLog(finalBooking, HttpContext.Session.GetString("uniLogin"));

                    foreach(ItemLineModel ilm in finalBooking.Items)
                    {
                        dBManager.UpdateItemLineAndLog(finalBooking.BookingID, bookingLogID, ilm.Model.ModelName, ilm.Quantity);
                    }
                }

            }
            else
            {
                // You are not authorized to make these changes and/or the booking has expired.
                errorMessages += "\nDu har ikke rettigheder til at lave nogen ændringer, eller ordren er udløbet.";
            }

            if(errorMessages.Length > 6)
            {
                HttpContext.Session.SetString("bookingEditError", errorMessages);
            }

            return GoToBooking(bookingModel.BookingID.ToString());
        }
    }
}
