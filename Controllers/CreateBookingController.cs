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

            newdata = AddToBasket(newdata, submitData);

           

            //clear model binding
            ModelState.Clear();

            return View("InventorySearch", newdata);
        }
       
        [HttpPost]
        public IActionResult InspectToBasket(CreateBookingModel data,string submitData)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);
            CreateBookingModel newdata = new CreateBookingModel();

           

            newdata = dbManagerBooking.GetInventory(data.SearchModel);
            newdata.ItemLines = data.ItemLines;
            newdata = AddToBasket(newdata, submitData);

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
           // newdata.ItemLines = data.ItemLines;
            newdata.Location = data.Location;
            newdata.LocationDropdown.RemoveRange(0, 3);
            ModelState.Clear();

            return View("Inspect", newdata);
        }

        [HttpPost]
        public IActionResult MyBasket(CreateBookingModel data)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);

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
         //   data.Notes = "";
            
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

            return View(newdata);
        }


        public IActionResult CreateBooking(CreateBookingModel data)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);

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
            if(booking.Notes.Length > 0)
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

            if ( dbManagerBooking.CreateBooking(data))
            {
                Debug.WriteLine("success, you have made an order");
            }
           else
            {
                Debug.WriteLine("ooops, something happened while booking");
            }

            return View("MyBasket",data);
        }

        public IActionResult DeleteItemLine(CreateBookingModel data,string submitData)
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
        private CreateBookingModel AddToBasket(CreateBookingModel newdata, string submitData)
        {
            DBManagerShared dbShared = new DBManagerShared(configuration);

            //split data
            string[] splittedData = submitData.Split('-');

            if (splittedData.Length > 1)
            {
                int id = int.Parse(splittedData[0]);
                int stock = int.Parse(splittedData[1]);
                int quantity = int.Parse(splittedData[2]);
                //string viewTitle = splittedData[3];

                //check if model exists in itemlines
                bool found = false;
                for (int i = 0; i < newdata.ItemLines.Count; i++)
                {
                    int curId = newdata.ItemLines[i].Model.ModelID;

                    //add quantity if already exists
                    if (curId == id)
                    {

                        //test if quantity is <= than stock
                        int basketCapacity = stock - (newdata.ItemLines[i].Quantity + quantity);
                        found = true;


                        if (basketCapacity >= 0)
                        {
                            newdata.ItemLines[i].Quantity += quantity;
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

                    ViewBag.id = id;
                }


                //calculate basket count
                for (int j = 0; j < newdata.ItemLines.Count; j++)
                {
                    newdata.BasketCount += newdata.ItemLines[j].Quantity;

                }

            }

            return newdata;
        }
    }
}
