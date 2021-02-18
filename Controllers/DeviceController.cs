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

        public IActionResult CreateDevice(DeviceModel deviceData)
        {
            //generate an instance of the database manager
            DBManagerDevice DBDevice = new DBManagerDevice(configuration);
            //send data to the manager

            return View("EditDevice");
        }


         public IActionResult EditDevice()
        {
            return View();
        }


         public IActionResult MoveLocation()
        {
            return View();
        }


    }
}
