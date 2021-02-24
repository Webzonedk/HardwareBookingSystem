using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
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



        public IActionResult Inventory(ModelInfoModel infoList)
        {
            //generate an instance of the database manager
            DBManagerDevice DBDevice = new DBManagerDevice(configuration);
            //get data from the manager


            //send data to the manager


            return View(infoList);
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
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
            DeviceModel data = deviceData.Device;
            data.ChangedBy = HttpContext.Session.GetString("uniLogin");
            Debug.Write($"modelname: {deviceData.Device.Model.ModelName}/modelDescription: {deviceData.Device.Model.ModelDescription}/category: {deviceData.Device.Model.Category.Category}/changedBy {deviceData.Device.ChangedBy}/serialnumber: {deviceData.Device.SerialNumber} ");

            int deviceID = dbManager.CreateDevice(data);
            data = dbManager.GetDeviceInfo(deviceID);
            List<DeviceModel> logs = dbManager.GetDeviceLogs(deviceID);
            EditDeviceModel editdata = new EditDeviceModel();


            return View("EditDevice", editdata);
        }

        public IActionResult EditDevice(DeviceModel deviceData)
        {
            return View(deviceData);
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
