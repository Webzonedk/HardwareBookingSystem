﻿using Microsoft.AspNetCore.Mvc;
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
    public class AdministrationController : Controller
    {

        public IActionResult CategoryAdmin()
        {
            return View();
        }
        
        public IActionResult LocationAdmin()
        {
            return View();
        }
         
        public void CreateQRCode()
        {
          
        }

        public void PrintQRCode()
        {

        }

    }
}
