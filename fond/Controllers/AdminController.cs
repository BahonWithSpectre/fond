using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fond.DbFolder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace fond.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDb db;
        public AdminController(AppDb _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string Name, string Pass)
        {
            
            const string NameC = "FondAdmin";
            const string Password = "fond123";

            if ((Name == NameC) && (Password == Pass))
            {
                // создаем один claim
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, NameC)
                };
                // создаем объект ClaimsIdentity
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                // установка аутентификационных куки
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }

            if (User.Identity.IsAuthenticated)
            {
                return Content(User.Identity.Name);
            }
            return Content("не аутентифицирован");
        }


    }
}
