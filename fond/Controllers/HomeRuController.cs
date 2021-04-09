using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fond.DbFolder;
using fond.Models;
using Microsoft.AspNetCore.Mvc;

namespace fond.Controllers
{
    public class HomeRuController : Controller
    {
        private readonly AppDb db;

        public HomeRuController(AppDb _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Project()
        {
            return View(db.Projects.ToList());
        }

        public IActionResult ProjectDetail(int Id)
        {
            ViewBag.Photos = db.ProjectImages.Where(p => p.ProjectId == Id).Select(o => o.ImageUrl).ToList();
            return View(db.Projects.FirstOrDefault(o => o.Id == Id));
        }


        public IActionResult Event()
        {

            return View(db.Events.Where(p => p.CMI != true).ToList());
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
            return View(db.Events.Where(p => p.CMI == true).ToList());
        }

        public IActionResult Otchet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Otchet(string num)
        {
            var res = db.Converts.Where(p => p.Id == System.Convert.ToInt32(num)).FirstOrDefault();
            if (res != null)
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














    }
}
