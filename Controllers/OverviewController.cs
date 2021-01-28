using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HUS_project.Models;
using HUS_project.DAL;
using Microsoft.AspNetCore.Http;

namespace HUS_project.Controllers
{
    public class OverviewController : Controller
    {

        public IActionResult History()
        {
            return View();
        }
        
        public IActionResult MySite()
        {
            return View();
        }
         
        public IActionResult InactiveLogs()
        {
            return View();
        }
          
        public IActionResult Inventory()
        {
            return View();
        }

    }
}
