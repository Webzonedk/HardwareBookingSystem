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
            string[] substring = deleteLocation.Split('.');
            string locationID = substring[0];
            string selectedID = substring[1];
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            string alert = manager.DeleteLocation(int.Parse(locationID));
            if (alert == "occupied")
            {
                ViewBag.alert = "occupied";
            }
            ViewBag.selectedID = selectedID;

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



        //Sending QRCode to QRview for a single location
        [HttpPost]
        public IActionResult PrintSingleLocationQR(string printQR)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            EditStorageLocationModel locations = manager.GetSpecificStorageLocation(printQR);

            string[] locationArray = new string[1];

            string location = $"{locations.StorageLocation.Location.Building}." +
                       $"{locations.StorageLocation.Location.RoomNumber}." +
                       $"{locations.StorageLocation.ShelfName}." +
                       $"{locations.StorageLocation.ShelfLevel}." +
                       $"{locations.StorageLocation.ShelfSpot}";
            locationArray[0] = location;
            TempData["QRData"] = locationArray;

            return RedirectToAction("PrintQR", "QRCode");

        }


        //Sending QRCodes to QRview for all locations
        [HttpPost]
        public IActionResult PrintAllQRCodes(EditStorageLocationModel QRdata)
        {
            EditStorageLocationModel locations = GetStorageLocations();
            List<string> locationStrings = new List<string>();

            for (int i = 0; i < locations.StorageLocations.Count; i++)
            {
                string location = $"{locations.StorageLocations[i].Location.Building}." +
                      $"{locations.StorageLocations[i].Location.RoomNumber}." +
                      $"{locations.StorageLocations[i].ShelfName}." +
                      $"{locations.StorageLocations[i].ShelfLevel}." +
                      $"{locations.StorageLocations[i].ShelfSpot}";
                locationStrings.Add(location);
            }

            TempData["QRData"] = locationStrings.ToArray();

            return RedirectToAction("PrintQR", "QRCode");

        }





        //------------------------------------
        //This one is not finish
        //------------------------------------
        //Delete building, RoomNumber or specifik room in the massdestruction area.
        [HttpPost]
        public IActionResult DeleteBuilding(EditStorageLocationModel deleteBuildingData)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            string deleteMessage = manager.DeleteBuilding(deleteBuildingData);
            if (deleteMessage != null)
            {
                ViewBag.deleteMessage = deleteMessage;
            }
            else
            {
                ViewBag.deleteMessage = "";
            }
            return View("LocationAdmin", GetStorageLocations());
        }




        //------------------------------------
        //This one is not finish
        //------------------------------------
        //Delete building, RoomNumber or specifik room in the massdestruction area.
        [HttpPost]
        public IActionResult DeleteRoomNumber(EditStorageLocationModel deleteRoomNumberData)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            string deleteMessage = manager.DeleteRoomNumber(deleteRoomNumberData);
            if (deleteMessage != null)
            {
                ViewBag.deleteMessage = deleteMessage;
            }
            return View("LocationAdmin", GetStorageLocations());
        }




        //------------------------------------
        //This one is not finish
        //------------------------------------
        //Delete building, RoomNumber or specifik room in the massdestruction area.
        [HttpPost]
        public IActionResult DeleteRoom(EditStorageLocationModel deleteRoomData)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            string deleteMessage = manager.DeleteRoom(deleteRoomData);
            if (deleteMessage != null)
            {
                ViewBag.deleteMessage = deleteMessage;
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







    }
}
