﻿using Microsoft.Extensions.Logging;
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
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        // constructor of homecontroller
        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }


        //not currently in use! should be removed at some time or repurposed
        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Task()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Log out the user. Session data is blanked and user is sent back to login page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            if(HttpContext.Session.GetString("uniLogin") != "")
            {
                HttpContext.Session.SetString("uniLogin", "");
            }
            if(HttpContext.Session.GetInt32("accessLevel") != null)
            {
                HttpContext.Session.SetInt32("accessLevel", 0);
            }
            
            return View("Index");
        }

        /// <summary>
        /// Verifies login credentials, acquires accesslevel and stores both (Except for password) in session.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>Relocates user as appropriate.</returns>
        [HttpPost]
        public IActionResult Login(UserLoginDataModel userLogin)
        {
            // New attempt at logging in, means old login attempt errors are irrelevant.
            HttpContext.Session.SetString("loginError", "");
            LDAPManager lDAPMan = new LDAPManager();

            if((userLogin.UNILogin != "" && userLogin.UNILogin != null) && (userLogin.Password != "" && userLogin.Password != null))
            {
                // Verify and acquire the user's relevant groups for access and any errors encountered in this endeavour (i.e. "Unable to establish connection")
                List<string> responses = lDAPMan.TestLogin(userLogin.UNILogin, userLogin.Password);

                bool attemptedLDAP = false;
                if(responses.Count == 0)
                {
                    attemptedLDAP = true;
                    responses = lDAPMan.GetAccessResponses(userLogin.UNILogin, userLogin.Password);
                }


                // Set Session data accordingly.
                if (responses.Count > 0)
                {
                    HttpContext.Session.SetString("uniLogin", userLogin.UNILogin);

                    // 0 (Impossible) = No access to anything. 1 = Teacher, access to frontend.
                    // 2 = SKP Student, access to most backend. 3 = SKP Teacher, full backend access.
                    int accessLevel = 0;
                    foreach (string response in responses)
                    {
                        if (response == "ZBC-Ri-skpElev")
                        {
                            accessLevel += 2;
                        }
                        else if (response == "zbc-riah-Data-IT")
                        {
                            accessLevel += 1;
                        }
                        else if (response.Contains("FEJL: "))
                        {
                            HttpContext.Session.SetString("loginError", response.Substring(6) + (attemptedLDAP ? "\nLDAP was attempted!":""));
                        }
                    }

                    HttpContext.Session.SetInt32("accessLevel", accessLevel);
                }

                // If the user is not a member of any groups, and there is no existing explanation as to why (i.e. error saying username or password incorrect)
                // -Then it means that the user is neither a SKP student or a ZBC Employee.
                if (responses.Count == 0 && HttpContext.Session.GetString("loginError") == "")
                {
                    HttpContext.Session.SetString("loginError", "Adgang Nægtet: Du har ikke medlemskab af relevante grupper");
                }
            }
            else
            {
                HttpContext.Session.SetString("loginError", "Udfyld uniLogin og kodeord. UniLogin er din ZBC email uden \"@zbc.dk\".");
            }

            return RelocateUser();
        }

        /// <summary>
        /// Used for correctly relocating people, depending on their access level.
        /// </summary>
        /// <returns></returns>
        public IActionResult RelocateUser()
        {
            if (HttpContext.Session.GetInt32("accessLevel") > 1)
            {
                // The Task view: For the actions that the SKP Students in the hardware room needs to take today.
                DBManagerTask DBTasker = new DBManagerTask(configuration);

                TasksModel tasks = new TasksModel(
                        // bookingsToBeRetrieved
                        DBTasker.GetBookingRetrievalsToBeMade(),
                        // bookingsToBeDelivered
                        DBTasker.GetBookingDeliveriesToBeMade()
                        
                    );
                return View("Task", tasks);
            }
            else if(HttpContext.Session.GetInt32("accessLevel") == 1)
            {
                return RedirectToAction("InventorySearch", "CreateBooking");
            }
            else
            {
                return Logout();
            }
        }
    }
}
