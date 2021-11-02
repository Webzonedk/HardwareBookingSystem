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
        public IActionResult AddToBasket(CreateBookingModel data, string submitData)
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


            //split data
            string[] splittedData = submitData.Split('-');

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

            }
           

            //calculate basket count
            for (int j = 0; j < newdata.ItemLines.Count; j++)
            {
                newdata.BasketCount += newdata.ItemLines[j].Quantity;
               
            }

            //clear model binding
            ModelState.Clear();

            return View("InventorySearch", newdata);
        }

        [HttpPost]
        public IActionResult MyBasket(CreateBookingModel data)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);

            CreateBookingModel tempData = dbManagerBooking.GetInventory(data.SearchModel);

            //sort tempData
            List<BookingSearchModel> temp = new List<BookingSearchModel>();
            for (int i = 0; i < data.ItemLines.Count; i++)
            {
                for (int j = 0; j < tempData.InventoryBooking.Count; j++)
                {
                    if(data.ItemLines[i].Model.ModelID == tempData.InventoryBooking[j].ModelID)
                    {
                        temp.Add(tempData.InventoryBooking[j]);
                    }
                }
            }

            data.CategoryDropdown = dbShared.GetCategories();
            data.LocationDropdown = dbShared.GetRooms();
            data.LocationDropdown.RemoveRange(0, 3);

            return View(data);
        }

        [HttpGet]
        public ActionResult test(CreateBookingModel data)
        {
            return RedirectToAction("Inspect", data);
        }

        public IActionResult Inspect(CreateBookingModel data)
        {
            return View(data);
        }

        public IActionResult CreateBooking()
        {
            return View();
        }
    }
}
