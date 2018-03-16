using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataSuit.SampleAspNetCore.Models;

namespace DataSuit.SampleAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenerator<PersonViewModel> _personGenerator;

        public HomeController(IGenerator<Models.PersonViewModel> personGenerator)
        {
            _personGenerator = personGenerator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetPersons()
        {
            return Json(_personGenerator.Generate(count: 5));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
