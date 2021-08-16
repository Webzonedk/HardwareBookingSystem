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
            return View(GetStorageLocations());
        }


        //Returns the searchresult when something has been choosen in the search fields
        public IActionResult LocationAdminResult(EditStorageLocationModel dataFromView)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            return View("LocationAdmin", manager.GetLocations(dataFromView));
        }


        [HttpPost]
        public IActionResult DeleteSingleLocation(string deleteLocation)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            manager.DeleteLocation(int.Parse(deleteLocation));
            return View("LocationAdmin", GetStorageLocations());
        }

        [HttpPost]
        public IActionResult CreateLocation(EditStorageLocationModel dataFromView)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            manager.CreateLocation(dataFromView);
            return View("LocationAdmin", GetStorageLocations());
        }






        public void CreateQRCode()
        {
          
        }

        public void PrintQRCode()
        {

        }
        
        private EditStorageLocationModel GetStorageLocations()
        {
            EditStorageLocationModel dropDownData = new EditStorageLocationModel();
            StorageLocationModel selectedStorageLocation = new StorageLocationModel();
            BuildingModel buildingModel = new BuildingModel();
            selectedStorageLocation.Location = buildingModel;
            dropDownData.StorageLocation = selectedStorageLocation;
           
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            List<string> buildings = manager.GetBuildings();
            List<byte> roomNumbers = manager.GetRoomNumbers();
            List<string> shelfNames = manager.GetShelfName();
            List<byte> shelfLevels = manager.GetShelfLevel();
            List<byte> shelfspots = manager.GetShelfSpot();
            List<StorageLocationModel> storageLocations = manager.GetSelectedStorageLocations(dropDownData);

            dropDownData.Buildings = buildings;
            dropDownData.RoomNumbers = roomNumbers;
            dropDownData.ShelfNames = shelfNames;
            dropDownData.ShelfLevels = shelfLevels;
            dropDownData.ShelfSpots = shelfspots;
            dropDownData.StorageLocations = storageLocations;

            return dropDownData;
        }

    }
}
