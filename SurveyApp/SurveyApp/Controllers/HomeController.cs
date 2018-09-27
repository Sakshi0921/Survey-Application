using SurveyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SurveyApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult AdminList()
        {
            var roleid = db.Roles.Where(r => r.Name == "Admin").Select(r=>r.Id).First();
            // db.Users.Select(x=>x.Roles.)
             ViewBag.AdminList =  db.Users.Where(x => (x.Roles.Any(y=>y.RoleId == roleid))).Select(u=> new UserViewModel { Email=u.Email,OrgName=u.Organizations.OrgName }).ToList();

            return View();
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}