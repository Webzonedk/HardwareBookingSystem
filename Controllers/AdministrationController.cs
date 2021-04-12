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

        public IActionResult LocationAdmin(EditStorageLocationModel dataFromView)
        {
            //Add standard values to the model
            EditStorageLocationModel initialData = new EditStorageLocationModel();
            StorageLocationModel selectedStorageLocation = new StorageLocationModel();
            BuildingModel buildingModel = new BuildingModel();
            //SortFilterModel sortFilterModel = new SortFilterModel();
            selectedStorageLocation.Location=buildingModel;
            initialData.StorageLocation = selectedStorageLocation;
            initialData.StorageLocation.Location.Building = null;
            initialData.StorageLocation.Location.RoomNumber = 0;
            initialData.StorageLocation.ShelfName=null;
            initialData.StorageLocation.ShelfLevel = 0;
            initialData.StorageLocation.ShelfSpot = 0;
            //dummy.Filter = 1;

            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            List<string> buildings = manager.GetBuildings();
            List<byte> roomNumbers = manager.GetRoomNumbers();
            List<string> shelfNames = manager.GetShelfName();
            List<byte> shelfLevels = manager.GetShelfLevel();
            List<byte> shelfspots = manager.GetShelfSpot();
            List<StorageLocationModel> storageLocations = manager.GetSelectedStorageLocations(initialData);

            EditStorageLocationModel dropDownData = new EditStorageLocationModel();
            dropDownData.Buildings = buildings;
            dropDownData.RoomNumbers = roomNumbers;
            dropDownData.ShelfNames = shelfNames;
            dropDownData.ShelfLevels = shelfLevels;
            dropDownData.ShelfSpots = shelfspots;
            dropDownData.StorageLocations = storageLocations;
            //dropDownData.SortFilters = sortFilters;

            return View(dropDownData);
        }


        //Returns the searchresult when something has been choosen in the search fields
        public IActionResult LocationAdminResult(EditStorageLocationModel dataFromView)
        {
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            List<StorageLocationModel> storageLocations = manager.GetSelectedStorageLocations(dataFromView);
            EditStorageLocationModel searchResult = new EditStorageLocationModel();

            List<string> buildings = manager.GetBuildings();
            List<byte> roomNumbers = manager.GetRoomNumbers();
            List<string> shelfNames = manager.GetShelfName();
            List<byte> shelfLevels = manager.GetShelfLevel();
            List<byte> shelfspots = manager.GetShelfSpot();

           
            searchResult.Buildings = buildings;
            searchResult.RoomNumbers = roomNumbers;
            searchResult.ShelfNames = shelfNames;
            searchResult.ShelfLevels = shelfLevels;
            searchResult.ShelfSpots = shelfspots;
            searchResult.Filter = 0;
        


            searchResult.StorageLocations = storageLocations;
            searchResult.StorageLocation = dataFromView.StorageLocation;

            return View("LocationAdmin", searchResult);
        }








        public void CreateQRCode()
        {
          
        }

        public void PrintQRCode()
        {

        }

    }
}
