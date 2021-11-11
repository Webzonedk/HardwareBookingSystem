using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HUS_project.DAL;
using HUS_project.Models;
using HUS_project.Models.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace HUS_project.Controllers
{
    public class CreateBookingController : Controller
    {
        private readonly IConfiguration configuration;


        public CreateBookingController(IConfiguration config)
        {
            this.configuration = config;
        }


        public IActionResult InventorySearch(CreateBookingModel data)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);



            //count number of items in basket
            data.BasketCount = 0;
            for (int i = 0; i < data.ItemLines.Count; i++)
            {
                data.BasketCount += data.ItemLines[i].Quantity;
            }

            CreateBookingModel newdata = dbManagerBooking.GetInventory(data.SearchModel);
            newdata.CategoryDropdown = dbShared.GetCategories();
            newdata.LocationDropdown = dbShared.GetRooms();
            newdata.LocationDropdown.RemoveRange(0, 3);
            newdata.ItemLines = data.ItemLines;
            newdata.SearchModel = data.SearchModel;
            newdata.BasketCount = data.BasketCount;

           
            return View(newdata);
        }

        [HttpPost]
        public IActionResult SearchToBasket(CreateBookingModel data, string submitData)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);


            CreateBookingModel newdata = new CreateBookingModel();


            newdata = dbManagerBooking.GetInventory(data.SearchModel);
            newdata.CategoryDropdown = dbShared.GetCategories();
            newdata.LocationDropdown = dbShared.GetRooms();

            newdata.SearchModel = data.SearchModel;
            newdata.InventoryBooking = newdata.InventoryBooking;
            newdata.CategoryDropdown = newdata.CategoryDropdown;
            newdata.ItemLines = data.ItemLines;
            newdata.Location = data.Location;
            newdata.LocationDropdown.RemoveRange(0, 3);

           

            //check if quantity is not zero
            if (submitData.Length > 1)
            {
                newdata = UpdateBasket(newdata, submitData);
            }
            else
            {
                ViewBag.quantityError = "intet indtastet";
                string[] split = submitData.Split('_');
                ViewBag.id = int.Parse(split[0]);
            }


            ////calculate basket count
            //newdata.BasketCount = 0;
            //for (int i = 0; i < newdata.ItemLines.Count; i++)
            //{
            //    newdata.BasketCount += newdata.ItemLines[i].Quantity;
            //}

            //clear model binding
            ModelState.Clear();

            return View("InventorySearch", newdata);
        }

        [HttpPost]
        public IActionResult InspectToBasket(CreateBookingModel data, string submitData)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);
            CreateBookingModel newdata = new CreateBookingModel();



            newdata = dbManagerBooking.GetInventory(data.SearchModel);
            newdata.ItemLines = data.ItemLines;
            newdata = UpdateBasket(newdata, submitData);

            //sort tempData & calculate basket capacity
            List<BookingSearchModel> temp = new List<BookingSearchModel>();

            for (int j = 0; j < newdata.InventoryBooking.Count; j++)
            {
                if (data.ModelName == newdata.InventoryBooking[j].ModelName)
                {
                    temp.Add(newdata.InventoryBooking[j]);
                }
            }
            newdata.InventoryBooking = temp;
            newdata.CategoryDropdown = dbShared.GetCategories();
            newdata.LocationDropdown = dbShared.GetRooms();

            newdata.SearchModel = data.SearchModel;
            newdata.CategoryDropdown = newdata.CategoryDropdown;
            
            newdata.Location = data.Location;
            newdata.ModelID = data.ModelID;
            newdata.ModelName = data.ModelName;
            newdata.LocationDropdown.RemoveRange(0, 3);
            ModelState.Clear();

            return View("Inspect", newdata);
        }

        [HttpPost]
        public IActionResult MyBasket(CreateBookingModel data)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);
            data.SearchModel.SearchName = "";

            CreateBookingModel tempData = dbManagerBooking.GetInventory(data.SearchModel);

            //sort tempData & calculate basket capacity
            List<BookingSearchModel> temp = new List<BookingSearchModel>();
            for (int i = 0; i < data.ItemLines.Count; i++)
            {
                for (int j = 0; j < tempData.InventoryBooking.Count; j++)
                {
                    if (data.ItemLines[i].Model.ModelID == tempData.InventoryBooking[j].ModelID)
                    {
                        temp.Add(tempData.InventoryBooking[j]);
                    }
                }
            }

            // get current stock
            data.InventoryBooking = temp;
            data.CategoryDropdown = dbShared.GetCategories();
            data.LocationDropdown = dbShared.GetRooms();
            data.LocationDropdown.RemoveRange(0, 3);
            ModelState.Clear();
            

            return View(data);
        }


        [HttpPost]
        public IActionResult Inspect(CreateBookingModel data, string link)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);

            //split data
            string[] split = link.Split("_");
            data.ModelName = split[0];
            data.ModelID = int.Parse(split[1]);
            data.SearchModel.SearchName = data.ModelName;
          
            //count number of items in basket
            data.BasketCount = 0;
            for (int i = 0; i < data.ItemLines.Count; i++)
            {
                data.BasketCount += data.ItemLines[i].Quantity;
            }

            //get inventory list
            CreateBookingModel newdata = dbManagerBooking.GetInventory(data.SearchModel);

            //sort tempData & calculate basket capacity
            List<BookingSearchModel> temp = new List<BookingSearchModel>();

            for (int j = 0; j < newdata.InventoryBooking.Count; j++)
            {
                if (data.ModelName == newdata.InventoryBooking[j].ModelName)
                {
                    temp.Add(newdata.InventoryBooking[j]);
                }
            }





            newdata.CategoryDropdown = dbShared.GetCategories();
            newdata.LocationDropdown = dbShared.GetRooms();
            newdata.LocationDropdown.RemoveRange(0, 3);
            newdata.ItemLines = data.ItemLines;
            newdata.SearchModel = data.SearchModel;
            newdata.BasketCount = data.BasketCount;
            newdata.ModelName = data.ModelName;
            newdata.ModelID = data.ModelID;

            ModelState.Clear();

            return View(newdata);
        }


        public IActionResult CreateBooking(CreateBookingModel data)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);

            //prep data for DBManager
            string[] splitLocation = data.Location.Split('.');
            BookingModel booking = new BookingModel();
            booking.Customer = HttpContext.Session.GetString("uniLogin");
            booking.Items = data.ItemLines;
            booking.Location = new BuildingModel() { Building = splitLocation[0], RoomNumber = splitLocation[1] };
            booking.Notes = data.Notes;
            booking.PlannedBorrowDate = data.SearchModel.RentDate;
            booking.PlannedReturnDate = data.SearchModel.ReturnDate;

            //prep notes
            if (booking.Notes != null)
            {
                string temp = booking.Notes;
                string newNote = $"Booking oprettet  \n{temp}";
                booking.Notes = newNote;
            }
            else
            {
                booking.Notes = "Booking oprettet";
            }

            data.BookingOrder = booking;

            if (dbManagerBooking.CreateBooking(data))
            {
                Debug.WriteLine("success, you have made an order");
                ViewBag.bookingSuccess = "Din ordre er nu bestilt";
            }
            else
            {
                Debug.WriteLine("ooops, something happened while booking");
            }
            
            data.ItemLines.Clear();
            data.Notes = "";

            ModelState.Clear();

            //get dropdowns
            CreateBookingModel newdata = dbManagerBooking.GetInventory(data.SearchModel);
            newdata.CategoryDropdown = dbShared.GetCategories();
            newdata.LocationDropdown = dbShared.GetRooms();
            newdata.LocationDropdown.RemoveRange(0, 3);
            data = newdata;

            return View("MyBasket", data);
        }

        public IActionResult DeleteItemLine(CreateBookingModel data, string submitData)
        {
            DBManagerShared dbShared = new DBManagerShared(configuration);

            int index = int.Parse(submitData);
            data.ItemLines.RemoveAt(index);

            data.CategoryDropdown = dbShared.GetCategories();
            data.LocationDropdown = dbShared.GetRooms();
            data.LocationDropdown.RemoveRange(0, 3);

            //calculate basketcount
            for (int i = 0; i < data.ItemLines.Count; i++)
            {
                data.BasketCount += data.ItemLines[i].Quantity;
            }

            ModelState.Clear();

            return View("MyBasket", data);
        }


        //helper methods
        private CreateBookingModel UpdateBasket(CreateBookingModel newdata, string submitData)
        {
            DBManagerShared dbShared = new DBManagerShared(configuration);

            //split data
            string[] splittedData = submitData.Split('_');

            if (splittedData.Length > 1)
            {
                int id = int.Parse(splittedData[0]);
                int stock = int.Parse(splittedData[1]);
                int quantity = int.Parse(splittedData[2]);
                

                //check if model exists in itemlines
                bool found = false;
                for (int i = 0; i < newdata.ItemLines.Count; i++)
                {
                    int curId = newdata.ItemLines[i].Model.ModelID;

                    //add quantity if already exists
                    if (curId == id)
                    {

                        //test if quantity is <= than stock
                      //  int basketCapacity = stock - (newdata.ItemLines[i].Quantity + quantity);
                        found = true;


                        if (quantity <= stock)
                        {
                            newdata.ItemLines[i].Quantity = quantity;
                        }
                        else
                        {
                            ViewBag.quantityError = "Der er desværre ikke nok enheder på lager.";

                        }


                        ViewBag.id = curId;
                        break;
                    }
                    else
                    {
                        found = false;
                    }
                }

                //add new itemline
                if (!found)
                {
                    if (quantity <= stock)
                    {
                        ItemLineModel itemline = new ItemLineModel();
                        itemline.Model = dbShared.GetSingleModel(id);
                        itemline.Quantity = quantity;
                        newdata.ItemLines.Add(itemline);
                    }
                    else
                    {
                        ViewBag.quantityError = "Der er desværre ikke nok enheder på lager.";
                    }

                }



                ////calculate basket count
                //for (int j = 0; j < newdata.ItemLines.Count; j++)
                //{
                //    newdata.BasketCount += newdata.ItemLines[j].Quantity;

                //}

                ViewBag.id = id;
            }

            //calculate basket count
            newdata.BasketCount = 0;
            for (int i = 0; i < newdata.ItemLines.Count; i++)
            {
                newdata.BasketCount += newdata.ItemLines[i].Quantity;
            }

            return newdata;
        }

      
    }
}
