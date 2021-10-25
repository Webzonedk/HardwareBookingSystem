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
            
            //add new itemline
            ItemLineModel itemline = new ItemLineModel();
            itemline.Model = dbShared.GetSingleModel(int.Parse(submitData));
            data.ItemLines.Add(itemline);

            return View("InventorySearch", data);
        }

        public IActionResult CreateBooking()
        {
            return View();
        }
    }
}
