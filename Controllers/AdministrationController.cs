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


        //Delete a single location from the overview
        [HttpPost]
        public IActionResult DeleteSingleLocation(string deleteLocation)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            string alert = manager.DeleteLocation(int.Parse(deleteLocation));
            if (alert == "occupied")
            {
                ViewBag.alert = "occupied";
            }
            return View("LocationAdmin", GetStorageLocations());
        }


        //Creating a new location if not exist, based on input fields in Blue oister bar
        [HttpPost]
        public IActionResult CreateLocation(EditStorageLocationModel dataFromView)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            manager.CreateLocation(dataFromView);
            return View("LocationAdmin", GetStorageLocations());
        }


        //------------------------------------
        //This one is not finish
        //------------------------------------
        //Delete a single location from the overview
        [HttpPost]
        public IActionResult MassDestructionDeleteBuildingOrRoom(string buildingName, string roomNumber)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            string alert = manager.DeleteBuildingOrRoom(buildingName, roomNumber);
            if (alert == "occupied")
            {
                ViewBag.alert = "occupied";
            }
            return View("LocationAdmin", GetStorageLocations());
        }

        //Getting the locations from DB and listing dropdowns in Blue oister bar, and also listing the selected Storagelocation.
        private EditStorageLocationModel GetStorageLocations()
        {
            EditStorageLocationModel dropDownData = new EditStorageLocationModel();
            StorageLocationModel selectedStorageLocation = new StorageLocationModel();
            BuildingModel buildingModel = new BuildingModel();
            selectedStorageLocation.Location = buildingModel;
            dropDownData.StorageLocation = selectedStorageLocation;

            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            List<string> buildings = manager.GetBuildings();
            List<string> roomNumbers = manager.GetRoomNumbers();
            List<string> shelfNames = manager.GetShelfName();
            List<string> shelfLevels = manager.GetShelfLevel();
            List<string> shelfspots = manager.GetShelfSpot();
            List<StorageLocationModel> storageLocations = manager.GetSelectedStorageLocations(dropDownData);

            dropDownData.Buildings = buildings;
            dropDownData.RoomNumbers = roomNumbers;
            dropDownData.ShelfNames = shelfNames;
            dropDownData.ShelfLevels = shelfLevels;
            dropDownData.ShelfSpots = shelfspots;
            dropDownData.StorageLocations = storageLocations;

            return dropDownData;
        }





        //QR code generation
        public void CreateQRCode()
        {

        }

        public void PrintQRCode()
        {

        }


    }
}
