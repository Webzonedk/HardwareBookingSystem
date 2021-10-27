using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HUS_project.DAL;
using HUS_project.Models;
using HUS_project.Models.ViewModels;

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

            data = dbManagerBooking.GetInventory(data.SearchModel);
            data.CategoryDropdown = dbShared.GetCategories();

            return View(data);
        }

        [HttpPost]
        public IActionResult AddToBasket(CreateBookingModel data, string submitData)
        {
            DBManagerCreateBooking dbManagerBooking = new DBManagerCreateBooking(configuration);
            DBManagerShared dbShared = new DBManagerShared(configuration);




            CreateBookingModel oldData = dbManagerBooking.GetInventory(data.SearchModel);
            oldData.CategoryDropdown = dbShared.GetCategories();
            data.SearchModel = oldData.SearchModel;
            data.InventoryBooking = oldData.InventoryBooking;
            data.CategoryDropdown = oldData.CategoryDropdown;


            //split data
            string[] splittedData = submitData.Split('-');
         
            int id = int.Parse(splittedData[0]);
            int quantity = int.Parse(splittedData[1]);


            //check if model exists in itemlines
            bool found = false;
            for (int i = 0; i < data.ItemLines.Count; i++)
            {
                int curId = data.ItemLines[i].Model.ModelID;
                if (curId == id)
                {
                    data.ItemLines[i].Quantity += quantity;
                    found = true;
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
                ItemLineModel itemline = new ItemLineModel();
                itemline.Model = dbShared.GetSingleModel(id);
                itemline.Quantity = quantity;
                data.ItemLines.Add(itemline);
            }

            //calculate basket count
            for (int j = 0; j < data.ItemLines.Count; j++)
            {
                data.BasketCount += data.ItemLines[j].Quantity;
            }



            return View("InventorySearch", data);
        }

        public IActionResult CreateBooking()
        {
            return View();
        }
    }
}
