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

        public IActionResult CreateDevice()
        {
            List<DeviceModel> list = new List<DeviceModel>();
            

            for (int i = 0; i < 10; i++)
            {
                DeviceModel deviceData = new DeviceModel();
                deviceData.Initialize();
                deviceData.Model.ModelName = "serial_" +i;
                list.Add(deviceData);
            }
            

            

            return View(list);
        }

        public IActionResult AddDeviceToDB (DeviceModel deviceData)
        {
            DBManagerDevice dbManager = new DBManagerDevice(configuration);
           int deviceID = dbManager.CreateDevice(deviceData);
            return View("EditDevice", deviceID);
        }

         public IActionResult EditDevice(DeviceModel deviceData)
        {
            return View(deviceData);
        }


         public IActionResult MoveLocation()
        {
            return View();
        }




    }
}
