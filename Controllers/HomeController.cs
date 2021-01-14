using Microsoft.Extensions.Logging;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CheckSession();
            return View();
        }

        public IActionResult Main()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Log out the user. Session is Cleared and user is sent back to login page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        /// <summary>
        /// Verifies login credentials, acquires accesslevel and stores both (Except for password) in session.
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(UserLoginDataModel userLogin)
        {
            // Verify user using LDAP and acquire all the relevant groups the user is a member of, if any.
            // ################################## TESTING CONTENT: ################################
            List<string> groups = LDAPManager.TestLogin(userLogin.UNILogin, userLogin.Password); // LDAPManager.AcquireAccessLevel(userLogin.UNILogin, userLogin.Password);

            // Set Session data accordingly.
            if (groups.Count > 0)
            {
                HttpContext.Session.SetString("uniLogin", userLogin.UNILogin);

                // 0 (Impossible) = No access to anything. 1 = Teacher, access to frontend.
                // 2 = SKP Student, access to most backend. 3 = SKP Teacher, full backend access.
                int accessLevel = 0;
                foreach (string group in groups)
                {
                    if (group == "ZBC-Ri-skpElev")
                    {
                        accessLevel += 2;
                    }
                    else if (group == "ZBC-RIAH-Ansatte")
                    {
                        accessLevel += 1;
                    }
                }

                HttpContext.Session.SetInt32("accessLevel", accessLevel);
            }


            if (HttpContext.Session.GetInt32("accessLevel") > 1)
            {
                return View("Task");
            }
            else if (HttpContext.Session.GetInt32("accessLevel") == 1)
            {
                return View("Main");
            }
            else
            {
                return View("Test");
            }
        }

        public IActionResult RelocateUser()
        {
            if (HttpContext.Session.GetInt32("accessLevel") > 1)
            {
                return View("Task");
            }
            else if(HttpContext.Session.GetInt32("accessLevel") == 1)
            {
                return View("Main");
            }
            else
            {
                return View("Test");
            }
        }

        public void CheckSession()
        {
            if(HttpContext.Session.GetString("uniLogin") != null)
            {
                RelocateUser();
            }
        }
    }
}
