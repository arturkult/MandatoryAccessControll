using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BSK_Projekt2.App_Start;
using BSK_Projekt2.Models;

namespace BSK_Projekt2.Controllers
{
    public class RepairsController : Controller
    {
        private App_Start.AppContext db = new App_Start.AppContext();

        // GET: Repairs
        public ActionResult Index()
        {
            User user = null;
            if ((user = IsAuthorizedUser("read")) != null)
                return View(db.Repairs.ToList());
            return RedirectToAction("About", "Home");
        }

        // GET: Repairs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair repair = db.Repairs.Find(id);
            if (repair == null)
            {
                return HttpNotFound();
            }
            User user = null;
            if ((user = IsAuthorizedUser("read")) != null)
                return View(repair);
            return RedirectToAction("About", "Home");
        }

        // GET: Repairs/Create
        public ActionResult Create()
        {
            Session["availableCars"] = db.Cars.ToList();
            User user = null;
            if ((user = IsAuthorizedUser("create")) != null)
                return View();
            return RedirectToAction("About", "Home");
        }

        // POST: Repairs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,RepairedCar")] Repair repair)
        {
            User user = null;
            if ((user = IsAuthorizedUser("create")) != null)
                if (ModelState.IsValid)
                {
                    db.Repairs.Add(repair);
                    db.SaveChanges();
                    db = new App_Start.AppContext();
                    return RedirectToAction("Index");
                }
                else return View(repair);
            return RedirectToAction("About", "Home");
        }

        // GET: Repairs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair repair = db.Repairs.Find(id);
            if (repair == null)
            {
                return HttpNotFound();
            }
            User user = null;
            if ((user = IsAuthorizedUser("update")) != null)
                return View(repair);
            return RedirectToAction("About", "Home");
        }

        // POST: Repairs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,RepairedCar")] Repair repair)
        {
            User user = null;
            if ((user = IsAuthorizedUser("update")) != null)
                if (ModelState.IsValid)
                {
                    db.Entry(repair).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return View(repair);
            return RedirectToAction("About", "Home");
        }

        // GET: Repairs/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repair repair = db.Repairs.Find(id);
            if (repair == null)
            {
                return HttpNotFound();
            }
            User user = null;
            if ((user = IsAuthorizedUser("delete")) != null)
                return View(repair);
            return RedirectToAction("About", "Home");
        }

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = null;
            if ((user = IsAuthorizedUser("delete")) != null)
            {
                Repair repair = db.Repairs.Find(id);
                db.Repairs.Remove(repair);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("About", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private User IsAuthorizedUser(string operation)
        {
            if (Session["loggedUser"] != null)
            {
                User user = ((User)Session["loggedUser"]);
                var classification = db.TableClearances.First(e => e.EntityName == "repairs");
                if (classification.CanDoIt(operation, user))
                    return user;
                else TempData.Add("errorMessage", "Nie masz odpowiednich uprawnień aby oglądać te dane lub wykonać tę operację");
            }
            else
            {
                TempData.Add("errorMessage", "Jesteś niezalogowany");
            }
            return null;
        }
    }
}
