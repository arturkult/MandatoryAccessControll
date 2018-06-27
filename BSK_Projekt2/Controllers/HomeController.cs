using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BSK_Projekt2.Controllers
{
    public class HomeController : Controller
    {
        private App_Start.AppContext db = new App_Start.AppContext();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            
            string login = Request.Form["email"];
            string password = Request.Form["password"];
            PasswordHasher passwordHasher = new PasswordHasher();
            var user = db.UsersTable.Where(u => u.UserName == login).FirstOrDefault();
            if (user != null)
                if(passwordHasher.VerifyHashedPassword(user.Password, password) == PasswordVerificationResult.Success)
                {
                    Session["loggedUser"] = user;
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
            return new HttpStatusCodeResult(HttpStatusCode.Conflict);
        }

        public ActionResult Logout()
        {
            if(Session["loggedUser"] !=null)
            {
                Session["loggedUser"] = null;
            }
            Console.WriteLine("logout");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}