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
            //ViewBag.Project = db.Projects.ToList();
            return View();
        }


        public IActionResult Project()
        {
            return View(db.Projects.ToList());
        }

        public IActionResult ProjectDetail(int Id)
        {
            ViewBag.Photos = db.ProjectImages.Where(p => p.ProjectId == Id).Select(o=>o.ImageUrl).ToList();
            ViewBag.Events = db.Events.Where(p => p.ProjectId == Id).ToList();
            return View(db.Projects.FirstOrDefault(o=>o.Id == Id));
        }


        public IActionResult Event()
        {
            return View(db.Events.Where(p=>p.CMI != true).ToList());
        }

        public IActionResult EventDetail(int? Id)
        {
            ViewBag.Photos = db.EventImages.Where(p => p.EventId == Id).Select(o => o.ImageUrl).ToList();
            return View(db.Events.FirstOrDefault(o => o.Id == Id));
        }

        public IActionResult Guide()
        {
            QuesModel qm = new QuesModel();
            qm.QuestionTypes = db.QuestionTypes.ToList();
            qm.Questions = db.Questions.ToList();
            return View(qm);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Smi()
        {
            return View(db.Events.Where(p=>p.CMI == true).ToList());
        }

        public IActionResult Otchet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Otchet(string num)
        {
            var res = db.Converts.Where(p => p.Id == System.Convert.ToInt32(num)).FirstOrDefault();
            if(res != null)
            {
                ViewBag.Data = res;
            }
            else
            {
                ViewBag.Error = "Конверт номері табылмады...";
            }
            
            
            return View();
        }

        public IActionResult OtchetTable()
        {
            return View();
        }

        public IActionResult Family()
        {
            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }








        public IActionResult Kino()
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
