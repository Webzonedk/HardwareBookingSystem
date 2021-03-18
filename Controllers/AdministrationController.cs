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
    public class AdministrationController : Controller
    {
        //public bool field for checking state of login & field for configuration 
        public bool IsLoggedIn;
        private readonly IConfiguration configuration;

        // constructor of homecontroller
        public AdministrationController(IConfiguration config)
        {
            this.configuration = config;
        }
        public IActionResult CategoryAdmin()
        {
            return View();
        }

        public IActionResult LocationAdmin()
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            List<string> buildings = manager.GetBuildings();
            List<byte> roomNumbers = manager.GetRoomNumbers();
            List<string> shelfNames = manager.GetShelfName();
            List<byte> shelfLevels = manager.GetShelfLevel();
            List<byte> shelfspots = manager.GetShelfSpot();


            EditStorageLocationModel dropDownData = new EditStorageLocationModel();
            dropDownData.Buildings = buildings;
            dropDownData.RoomNumbers = roomNumbers;
            dropDownData.ShelfNames = shelfNames;
            dropDownData.ShelfLevels = shelfLevels;
            dropDownData.ShelfSpots = shelfspots;

            return View(dropDownData);
        }



        public IActionResult LocationAdminResult(StorageLocationModel dataFromView)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
           

            List<StorageLocationModel> storageLocations = manager.GetSelectedStorageLocations(dataFromView);
          

            return View("LocationAdmin", storageLocations);
        }
         
        public void CreateQRCode()
        {
          
        }

        public void PrintQRCode()
        {

        }

    }
}
