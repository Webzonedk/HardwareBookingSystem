using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HUS_project.Models.ViewModels;
using HUS_project.Models;
using HUS_project.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace HUS_project.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IConfiguration configuration;

        // constructor of homecontroller
        public DeviceController(IConfiguration config)
        {
            this.configuration = config;
        }





        //get model name and category names before returning to view
        public IActionResult CreateDevice()
        {
            //get data from database
            DBManagerShared sharedDBManager = new DBManagerShared(configuration);
            List<string> categories = sharedDBManager.GetCategories();
            List<string> modelNames = sharedDBManager.GetModelNames();
            
            //create view model
            CreateDeviceModel deviceData = new CreateDeviceModel();
            deviceData.Categories = categories;
            deviceData.ModelNames = modelNames;
            deviceData.Device = new DeviceModel();
         
            return View(deviceData);
        }
        
        public IActionResult AddDeviceToDB(CreateDeviceModel deviceData)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            //Add device to database
            DeviceModel data = deviceData.Device;
            data.ChangedBy = HttpContext.Session.GetString("uniLogin");
            int deviceID = dbManager.CreateDevice(data);

            //return device info to Edit view
            data = dbManager.GetDeviceInfoWithLocation(deviceID);
            List<DeviceModel> logs = dbManager.GetDeviceLogs(deviceID);
            List<string> categories = dbsharedManager.GetCategories();
            List<string> modelNames = dbsharedManager.GetModelNames();
            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            editdata.Logs = logs;
            editdata.Categories = categories;
            editdata.ModelNames = modelNames;

            return View("EditDevice", editdata);
        }

        public IActionResult EditDevice(int deviceID)
        {
            //initializing DB managers
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DBManagerShared dbsharedManager = new DBManagerShared(configuration);

            //return device info to Edit view
            int ID = 1026;
            DeviceModel data = new DeviceModel();
            data = dbManager.GetDeviceInfoWithLocation(ID);
            List<DeviceModel> logs = dbManager.GetDeviceLogs(ID);
            List<string> categories = dbsharedManager.GetCategories();
            List<string> modelNames = dbsharedManager.GetModelNames();
            List<string> rooms = dbsharedManager.GetAllRooms();
            EditDeviceModel editdata = new EditDeviceModel();
            editdata.Device = data;
            editdata.Room = new string($"{data.Location.Location.Building}.{data.Location.Location.RoomNumber.ToString()}");
            editdata.Logs = logs;
            editdata.Categories = categories;
            editdata.ModelNames = modelNames;
            editdata.Rooms = rooms;

            return View(editdata);
        }

        // edits device location and returns to Edit view
        public IActionResult EditPlacement (EditDeviceModel data)
        {
            return View("EditDevice", data);
        }

        public IActionResult Inventory(ModelInfoModel infoList)
        {
            //generate an instance of the database manager
            DBManagerDevice DBDevice = new DBManagerDevice(configuration);
            //get data from the manager


            //send data to the manager
            

            return View(infoList);
        }

        public IActionResult MoveLocation()
        {
            return View();
        }

        //public IActionResult CreateQRCode()
        //{

        //}


    }
}
