using System;
using Specter.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

namespace Specter.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Timesheet()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        public IActionResult Templates()
        {
            var model = new TimesheetModel
            {
                Date = DateTime.Now
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
