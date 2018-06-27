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
    public class CarsController : Controller
    {
        private App_Start.AppContext db = new App_Start.AppContext();

        // GET: Cars
        public ActionResult Index()
        {
            User user = null;
            if ((user = IsAuthorizedUser("read"))!=null)
                return View(db.Cars.ToList());
            return RedirectToAction("About", "Home");
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            if(IsAuthorizedUser("read") != null)
                    return View(car);           
            return RedirectToAction("About","Home");
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            if (IsAuthorizedUser("create") != null)
                return View();
            return RedirectToAction("About", "Home");
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Brand,Model,ClassificationLvl")] Car car)
        {
            if (ModelState.IsValid)
            {
                if (IsAuthorizedUser("create") != null)
                {
                    db.Cars.Add(car);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("About", "Home");
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            if(IsAuthorizedUser("update") != null)
                return View(car);
            return RedirectToAction("About", "Home");
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Brand,Model,ClassificationLvl")] Car car)
        {
            if (ModelState.IsValid)
            {
                if (IsAuthorizedUser("update") != null)
                {
                    db.Entry(car).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("About", "Home");
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            if(IsAuthorizedUser("delete") != null)
                return View(car);
            return RedirectToAction("About", "Home");
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsAuthorizedUser("delete")!=null)
            {
                Car car = db.Cars.Find(id);
                db.Repairs.RemoveRange(car.Repairs);
                db.Cars.Remove(car);
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
                var classification = db.TableClearances.First(e => e.EntityName == "cars");
                if (classification.CanDoIt(operation, user))
                    return user;
                else {
                    TempData.Clear();
                    TempData.Add("errorMessage", "Nie masz odpowiednich uprawnień aby oglądać te dane lub wykonać tę operację");
                }
            }
            else
            {
                TempData.Clear();
                TempData.Add("errorMessage", "Jesteś niezalogowany");
            }
            return null;
        }
    }
}
