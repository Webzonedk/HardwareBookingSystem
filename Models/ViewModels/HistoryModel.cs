using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HUS_project.Models.ViewModels
{
    public class HistoryModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
