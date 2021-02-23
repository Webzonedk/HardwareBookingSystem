using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HUS_project.Models;
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
            
            EditStorageLocationModel dropDowns = new EditStorageLocationModel();
            dropDowns.Rooms= new List<BuildingModel>
            //List<BuildingModel> buildings = new List<BuildingModel>
            {
                new BuildingModel() { Building = "Modtagelse", RoomNr = 1 },
                new BuildingModel() { Building = "A", RoomNr = 2 },
                new BuildingModel() { Building = "B", RoomNr = 3 },
                new BuildingModel() { Building = "C", RoomNr = 4 },
                new BuildingModel() { Building = "D", RoomNr = 5 },
                new BuildingModel() { Building = "E", RoomNr = 6 },
                new BuildingModel() { Building = "F", RoomNr = 7 },
                new BuildingModel() { Building = "G", RoomNr = 8 },
                new BuildingModel() { Building = "H", RoomNr = 9 },
                new BuildingModel() { Building = "I", RoomNr = 10 },
                new BuildingModel() { Building = "J", RoomNr = 11 }
            };





            return View(dropDowns);
        }
         
        public void CreateQRCode()
        {
          
        }

        public void PrintQRCode()
        {

        }

    }
}
