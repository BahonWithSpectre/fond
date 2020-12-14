using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fond.Models;
using fond.DbFolder;

namespace fond.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDb db;

        public HomeController(AppDb _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Project()
        {
            return View();
        }


        public IActionResult Event()
        {
            return View();
        }

        public IActionResult EventDetail(int? Id)
        {
            return View();
        }

        public IActionResult Guide()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Smi()
        {
            return View();
        }

        public IActionResult Otchet()
        {
            return View();
        }

        public IActionResult OtchetTable()
        {
            return View();
        }


        public IActionResult Contact()
        {

            return View();
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
