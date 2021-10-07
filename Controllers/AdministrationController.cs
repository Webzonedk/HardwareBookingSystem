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





        //----------------------------------------------------------------------------
        //Returns the main view for location admin
        //----------------------------------------------------------------------------
        public IActionResult LocationAdmin()
        {
            return View(GetStorageLocations());
        }





        //----------------------------------------------------------------------------
        //Returns the searchresult when something has been choosen in the search fields
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult LocationAdminResult(EditStorageLocationModel dataFromView)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            return View("LocationAdmin", manager.GetLocations(dataFromView));
        }





        //----------------------------------------------------------------------------
        //Getting the locations from DB and listing dropdowns in Blue oister bar, and also listing the selected Storagelocation.
        //----------------------------------------------------------------------------
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
            List<string> rooms = manager.GetRooms();
            List<string> shelfNames = manager.GetShelfName();
            List<string> shelfLevels = manager.GetShelfLevel();
            List<string> shelfspots = manager.GetShelfSpot();
            List<StorageLocationModel> storageLocations = manager.GetSelectedStorageLocations(dropDownData);

            dropDownData.Buildings = buildings;
            dropDownData.RoomNumbers = roomNumbers;
            dropDownData.Rooms = rooms;
            dropDownData.ShelfNames = shelfNames;
            dropDownData.ShelfLevels = shelfLevels;
            dropDownData.ShelfSpots = shelfspots;
            dropDownData.StorageLocations = storageLocations;

            return dropDownData;
        }





        //----------------------------------------------------------------------------
        //Creating a new location if not exist, based on input fields in Blue oister bar
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult CreateLocation(EditStorageLocationModel dataFromView)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);

            //Create building if filled in
            if (dataFromView.StorageLocation.Location.Building != null)
            {
                string buildingNameFeedBack = manager.CreateBuilding(dataFromView);
                if (buildingNameFeedBack != null)
                {
                    ViewBag.buildingNameFeedBack = buildingNameFeedBack;
                }
                else
                {
                    ViewBag.buildingNameFeedBack = "";
                }
            }

            //Creating a room if both building and room is filled in. Else only create RoomNumber if filled in
            if (dataFromView.StorageLocation.Location.RoomNumber != null)
            {
                string roomNumberFeedBack;
                string roomFeedBack = null;
                if (dataFromView.StorageLocation.Location.Building != null)
                {
                    roomNumberFeedBack = manager.CreateRoomNumber(dataFromView);
                    roomFeedBack = manager.CreateRoom(dataFromView);
                }
                else
                {
                    roomNumberFeedBack = manager.CreateRoomNumber(dataFromView);
                }

                if (roomNumberFeedBack != null)
                {
                    ViewBag.roomNumberFeedBack = roomNumberFeedBack;
                }
                else
                {
                    ViewBag.roomNumberFeedBack = "";
                }

                if (roomFeedBack != null)
                {
                    ViewBag.roomFeedBack = roomFeedBack;
                }
                else
                {
                    ViewBag.roomFeedBack = "";
                }
            }

            //Create shelf name if filled in
            if (dataFromView.StorageLocation.ShelfName != null)
            {
                string shelfNameFeedBack = manager.CreateShelfName(dataFromView);
                if (shelfNameFeedBack != null)
                {
                    ViewBag.shelfNameFeedBack = shelfNameFeedBack;
                }
                else
                {
                    ViewBag.shelfNameFeedBack = "";
                }
            }

            //Create shelfLevel if filled in
            if (dataFromView.StorageLocation.ShelfLevel != null)
            {
                string shelfLevelFeedBack = manager.CreateShelfLevel(dataFromView);
                if (shelfLevelFeedBack != null)
                {
                    ViewBag.shelfLevelFeedBack = shelfLevelFeedBack;
                }
                else
                {
                    ViewBag.shelfLevelFeedBack = "";
                }
            }

            //Create shelfSpot if filled in
            if (dataFromView.StorageLocation.ShelfSpot != null)
            {
                string shelfSpotFeedBack = manager.CreateShelfSpot(dataFromView);
                if (shelfSpotFeedBack != null)
                {
                    ViewBag.shelfSpotFeedBack = shelfSpotFeedBack;
                }
                else
                {
                    ViewBag.shelfSpotFeedBack = "";
                }
            }

            //Create storagelocation if all fields are filled in.
            if ((dataFromView.StorageLocation.ShelfSpot != null) && (dataFromView.StorageLocation.Location.Building != null) && (dataFromView.StorageLocation.Location.RoomNumber != null)
                && (dataFromView.StorageLocation.ShelfName != null) && (dataFromView.StorageLocation.ShelfLevel != null))
            {
                string storageLocationFeedBack = manager.CreateLocation(dataFromView);
                if (storageLocationFeedBack != null)
                {
                    ViewBag.storageLocationFeedBack = storageLocationFeedBack;
                }
                else
                {
                    ViewBag.storageLocationFeedBack = "";
                }
            }

            return View("LocationAdmin", GetStorageLocations());
        }





        //----------------------------------------------------------------------------
        //Sending QRCode to QRview for a single location
        //----------------------------------------------------------------------------
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





        //----------------------------------------------------------------------------
        //Sending QRCodes to QRview for all locations
        //----------------------------------------------------------------------------
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





        //----------------------------------------------------------------------------
        //Delete a single location from the overview
        //----------------------------------------------------------------------------
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





        //----------------------------------------------------------------------------
        //Delete building in the massdestruction area.
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult DeleteBuilding(EditStorageLocationModel deleteBuildingData)
        {
            if (deleteBuildingData.DeleteBuilding != null)
            {
                try
                {
                    DBManagerAdministration manager = new DBManagerAdministration(configuration);
                    string deleteMessage = manager.DeleteBuilding(deleteBuildingData);
                    if (deleteMessage != null)
                    {
                        ViewBag.deleteBuildingAndRoomFeedback = deleteMessage;
                    }
                    else
                    {
                        ViewBag.deleteBuildingAndRoomFeedback = "";
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ViewBag.deleteBuildingAndRoomFeedback = "Der er ikke valgt en bygning.";
            }
            return View("LocationAdmin", GetStorageLocations());
        }





        //----------------------------------------------------------------------------
        //Delete RoomNumber in the massdestruction area.
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult DeleteRoomNumber(EditStorageLocationModel deleteRoomNumberData)
        {
            if (deleteRoomNumberData.DeleteRoomNumber != null)
            {
                try
                {
                    DBManagerAdministration manager = new DBManagerAdministration(configuration);
                    string deleteMessage = manager.DeleteRoomNumber(deleteRoomNumberData);
                    if (deleteMessage != null)
                    {
                        ViewBag.deleteBuildingAndRoomFeedback = deleteMessage;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ViewBag.deleteBuildingAndRoomFeedback = "Der er ikke valgt et lokalenummer.";
            }
            return View("LocationAdmin", GetStorageLocations());
        }





        //----------------------------------------------------------------------------
        //Delete room in the massdestruction area.
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult DeleteRoom(EditStorageLocationModel deleteRoomData)
        {
            if (deleteRoomData.DeleteRoom != null)
            {
                try
                {

                    DBManagerAdministration manager = new DBManagerAdministration(configuration);
                    string[] substring = deleteRoomData.DeleteRoom.Split('.');
                    if (substring[0] != null)
                    {
                        deleteRoomData.DeleteBuilding = substring[0];
                    }
                    if (substring[1] != null)
                    {
                        deleteRoomData.DeleteRoomNumber = substring[1];
                    }
                    if (deleteRoomData.DeleteBuilding != null && deleteRoomData.DeleteRoomNumber != null)
                    {
                        try
                        {
                            string deleteMessage = manager.DeleteRoom(deleteRoomData);
                            if (deleteMessage != null)
                            {
                                ViewBag.deleteBuildingAndRoomFeedback = deleteMessage;
                            }
                            else
                            {
                                ViewBag.deleteBuildingAndRoomFeedback = "Det indtastede input i feltet rum er i forkert format.";
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        ViewBag.deleteBuildingAndRoomFeedback = "Det indtastede input i feltet rum er i forkert format.";
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ViewBag.deleteBuildingAndRoomFeedback = "Der er ikke valgt et rum.";
            }
            return View("LocationAdmin", GetStorageLocations());
        }





        //----------------------------------------------------------------------------
        //Delete shelfName in the massdestruction area.
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult DeleteShelfName(EditStorageLocationModel deleteShelfNameData)
        {
            if (deleteShelfNameData.DeleteShelfName != null)
            {
                try
                {
                    DBManagerAdministration manager = new DBManagerAdministration(configuration);
                    string deleteMessage = manager.DeleteShelfName(deleteShelfNameData);
                    if (deleteMessage != null)
                    {
                        ViewBag.deleteLocationFeedback = deleteMessage;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ViewBag.deleteLocationFeedback = "Der er ikke valgt et hylde navn.";
            }
            return View("LocationAdmin", GetStorageLocations());
        }





        //----------------------------------------------------------------------------
        //Delete shelfLevel in the massdestruction area.
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult DeleteShelfLevel(EditStorageLocationModel deleteShelfLevelData)
        {
            if (deleteShelfLevelData.DeleteShelfLevel != null)
            {
                try
                {
                    DBManagerAdministration manager = new DBManagerAdministration(configuration);
                    string deleteMessage = manager.DeleteShelfLevel(deleteShelfLevelData);
                    if (deleteMessage != null)
                    {
                        ViewBag.deleteLocationFeedback = deleteMessage;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ViewBag.deleteLocationFeedback = "Der er ikke valgt en etage.";
            }
            return View("LocationAdmin", GetStorageLocations());
        }





        //----------------------------------------------------------------------------
        //Delete shelfLevel in the massdestruction area.
        //----------------------------------------------------------------------------
        [HttpPost]
        public IActionResult DeleteShelfSpot(EditStorageLocationModel deleteShelfSpotData)
        {
            if (deleteShelfSpotData.DeleteShelfSpot != null)
            {
                try
                {
                    DBManagerAdministration manager = new DBManagerAdministration(configuration);
                    string deleteMessage = manager.DeleteShelfSpot(deleteShelfSpotData);
                    if (deleteMessage != null)
                    {
                        ViewBag.deleteLocationFeedback = deleteMessage;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                ViewBag.deleteLocationFeedback = "Der er ikke valgt en plads.";
            }
            return View("LocationAdmin", GetStorageLocations());
        }


    }
}
