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
            DBManagerAdministration manager = new DBManagerAdministration(configuration);
            List<EditStorageLocationModel> dropDowns = manager.GetDropDowns();
           





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
