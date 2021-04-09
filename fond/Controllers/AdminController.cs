using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fond.DbFolder;
using fond.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fond.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDb db;
        private IHostingEnvironment env;
        public AdminController(AppDb _db, IHostingEnvironment _env)
        {
            db = _db;
            env = _env;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            const string NameC = "Fadmin";
            const string Password = "Fadmin";

            if ((model.Login == NameC) && (Password == model.Password))
            {
                return RedirectToAction("Project", "Admin");
            }
            ViewBag.Error = "Логин немесе пароль қате";
            return View();
        }

        #region Project

        public IActionResult Project()
        {
            return View(db.Projects.ToList());
        }

        public IActionResult AddProject()
        {
            return View(new Project());
        }

        [HttpPost]
        public IActionResult AddProject(Project model)
        {
            Project pro = new Project { ProjectName = model.ProjectName, Title = model.Title, Information = model.Information, VideoUrl = model.VideoUrl };

            db.Projects.Add(pro);
            db.SaveChanges();

            return RedirectToAction("Project");
        }

        public IActionResult EditProject(int Id)
        {
            ViewBag.Photos = db.ProjectImages.Where(p => p.ProjectId == Id).AsNoTracking().ToList();

            return View(db.Projects.Where(p=>p.Id == Id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult EditProject(Project model)
        {
            var pro = db.Projects.Where(o => o.Id == model.Id).FirstOrDefault();
            pro.ProjectName = model.ProjectName;
            pro.Title = model.Title;
            pro.Information = model.Information;
            pro.VideoUrl = model.VideoUrl;

            db.Projects.Update(pro);
            db.SaveChanges();

            return RedirectToAction("Project");
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoProject(IFormFile image, int Id)
        {
            ProjectImage pi = new ProjectImage { ProjectId = Id };
            if (image != null)
            {
                // путь к папке Files
                string path = "/Project/" + image.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(env.WebRootPath + path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                pi.ImageUrl = path;
                db.ProjectImages.Add(pi);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("EditProject", "Admin", new { Id });
        }


        [HttpPost]
        public async Task<IActionResult> AddGlavPhotoProject(IFormFile image, int Id)
        {
            Project pro = db.Projects.Where(p => p.Id == Id).FirstOrDefault();
            if(pro == null)
            {
                return RedirectToAction("Project");
            }
            if (image != null)
            {
                string path = "/Project/" + image.FileName;
                using (var fileStream = new FileStream(env.WebRootPath + path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                pro.Photo = path;
                db.Projects.Update(pro);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("EditProject", "Admin", new { Id });
        }
        #endregion


        #region Event

        public IActionResult Event()
        {
            return View(db.Events.Where(p => p.CMI == false).ToList());
        }

        public IActionResult AddEvent()
        {
            ViewBag.Project = db.Projects.ToList();
            return View(new Event());
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event model)
        {
            Event ev = new Event { EventName = model.EventName, CMI = false,
                                    Information = model.Information, Title = model.Title,
                                    VideoUrl = model.VideoUrl,
                                    ProjectId = model.ProjectId
                                  };

            db.Events.Add(ev);
            await db.SaveChangesAsync();

            return RedirectToAction("Event","Admin");
        }


        public async Task<IActionResult> EditEvent(int Id)
        {
            ViewBag.Project = db.Projects.AsNoTracking().ToList();
            return View(await db.Events.Include(p=>p.Project).FirstOrDefaultAsync(p=>p.Id == Id));
        }

        [HttpPost]
        public IActionResult EditEvent(Event model)
        {
            Event ev = db.Events.FirstOrDefault(o => o.Id == model.Id);

            ev.EventName = model.EventName;
            ev.CMI = false;
            ev.Information = model.Information;
            ev.Title = model.Title;
            ev.VideoUrl = model.VideoUrl;
            ev.ProjectId = model.ProjectId;

            db.Events.Update(ev);
            db.SaveChanges();

            return RedirectToAction("EditEvent", new { Id = model.Id });
        }

        #endregion

        #region Сурак Жауап

        public IActionResult Ques()
        {
            QuesModel qm = new QuesModel();
            qm.QuestionTypes = db.QuestionTypes.ToList();
            qm.Questions = db.Questions.ToList();
            return View(qm);
        }

        public IActionResult AddQuesType()
        {
            return View(new QuestionType());
        }

        [HttpPost]
        public IActionResult AddQuesType(QuestionType model)
        {
            QuestionType qt = new QuestionType { TypeName = model.TypeName  };
            db.QuestionTypes.Add(qt);
            db.SaveChanges();
            return RedirectToAction("Ques", "Admin");
        }


        public IActionResult AddQues(int Id)
        {
            ViewBag.Id = Id;
            return View(new Question { QuestionTypeId = Id });
        }

        [HttpPost]
        public IActionResult AddQues(Question model)
        {
            Question q = new Question { QuestionName = model.QuestionName, Answer = model.Answer, QuestionTypeId = model.QuestionTypeId };

            db.Questions.Add(q);
            db.SaveChanges();

            return RedirectToAction("Ques", "Admin");
        }


        #endregion

        #region CMI

        public IActionResult EventCmi()
        {
            return View(db.Events.Where(p => p.CMI == true).ToList());
        }

        public IActionResult AddEventCmi()
        {
            ViewBag.Project = db.Projects.ToList();
            return View(new Event());
        }

        [HttpPost]
        public async Task<IActionResult> AddEventCmi(Event model)
        {
            Event ev = new Event
            {
                EventName = model.EventName,
                CMI = true,
                Information = model.Information,
                Title = model.Title,
                VideoUrl = model.VideoUrl,
                ProjectId = model.ProjectId
            };

            db.Events.Add(ev);
            await db.SaveChangesAsync();

            return RedirectToAction("EventCmi", "Admin");
        }


        public async Task<IActionResult> EditEventCmi(int Id)
        {
            ViewBag.Project = db.Projects.AsNoTracking().ToList();
            return View(await db.Events.Include(p => p.Project).FirstOrDefaultAsync(p => p.Id == Id));
        }

        [HttpPost]
        public IActionResult EditEventCmi(Event model)
        {
            Event ev = db.Events.FirstOrDefault(o => o.Id == model.Id);

            ev.EventName = model.EventName;
            ev.CMI = true;
            ev.Information = model.Information;
            ev.Title = model.Title;
            ev.VideoUrl = model.VideoUrl;
            ev.ProjectId = model.ProjectId;

            db.Events.Update(ev);
            db.SaveChanges();

            return RedirectToAction("EditEventCmi", new { Id = model.Id });
        }

        #endregion


        #region Convert

        public IActionResult Convert()
        {
            return View(db.Converts.ToList());
        }

        public async Task<IActionResult> AddConvert()
        {
            DbFolder.Convert con = new DbFolder.Convert();
            db.Converts.Add(con);
            await db.SaveChangesAsync();

            return RedirectToAction("Convert", "Admin");
        }

        public IActionResult EditConvert(int Id)
        {
            return View(db.Converts.FirstOrDefault(o => o.Id == Id));
        }

        [HttpPost]
        public IActionResult EditConvert(DbFolder.Convert model)
        {
            DbFolder.Convert con = db.Converts.Where(p => p.Id == model.Id).FirstOrDefault();

            con.City = model.City;
            con.Monay = model.Monay;
            con.Text = model.Text;

            db.Converts.Update(con);
            db.SaveChanges();

            return RedirectToAction("Convert", "Admin");
        }

        #endregion
    }
}
