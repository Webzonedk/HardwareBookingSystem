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

            // Verify and acquire the user's relevant groups for access and any errors encountered in this endeavour (i.e. "Unable to establish connection")
            //List<string> reponses = LDAPManager.GetAccessResponses(userLogin.UNILogin, userLogin.Password);
            List<string> reponses = LDAPManager.TestLogin(userLogin.UNILogin, userLogin.Password);

            // Set Session data accordingly.
            if (reponses.Count > 0)
            {
                HttpContext.Session.SetString("uniLogin", userLogin.UNILogin);

                // 0 (Impossible) = No access to anything. 1 = Teacher, access to frontend.
                // 2 = SKP Student, access to most backend. 3 = SKP Teacher, full backend access.
                int accessLevel = 0;
                foreach (string reponse in reponses)
                {
                    if (reponse == "ZBC-Ri-skpElev")
                    {
                        accessLevel += 2;
                    }
                    else if (reponse == "ZBC-RIAH-Ansatte")
                    {
                        accessLevel += 1;
                    }
                    else if(reponse.Contains("FEJL: "))
                    {
                        HttpContext.Session.SetString("loginError", reponse.Substring(6));
                    }
                }

                HttpContext.Session.SetInt32("accessLevel", accessLevel);
            }

            // If the user is not a member of any groups, and there is no existing explanation as to why (i.e. error saying username or password incorrect)
            // -Then it means that the user is neither a SKP student or a ZBC Employee.
            if (reponses.Count == 0 && HttpContext.Session.GetString("loginError") == "")
            {
                HttpContext.Session.SetString("loginError", "Adgang Nægtet: Du har ikke medlemskab af relevante grupper");
            }

            return RelocateUser();
        }

        /// <summary>
        /// Used on the Index page for properly relocating people who have logged in, depending on their access level.
        /// </summary>
        /// <returns></returns>
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
                return Logout();
            }
        }
    }
}
