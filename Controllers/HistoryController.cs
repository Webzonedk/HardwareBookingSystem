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
    public class HistoryController : Controller
    {
        //public bool field for checking state of login & field for configuration 
        private readonly IConfiguration configuration;

        // constructor of homecontroller
        public HistoryController(IConfiguration config)
        {
            this.configuration = config;
        }



        public IActionResult SeachBookings()
        {
            HistoryModel dropDownData = new HistoryModel();
            BookingSearchCriteriaModel searchCriteria = new BookingSearchCriteriaModel();
            dropDownData.SearchCriteria = searchCriteria;

            DBManagerShared sharedManager = new DBManagerShared(configuration);
            List<string> rooms = sharedManager.GetRooms();
            dropDownData.Rooms = rooms;

            return View("History", dropDownData);
        }


    }
}
