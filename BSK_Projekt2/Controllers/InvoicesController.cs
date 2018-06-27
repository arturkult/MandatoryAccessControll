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
    public class InvoicesController : Controller
    {
        private BSK_Projekt2.App_Start.AppContext db = new BSK_Projekt2.App_Start.AppContext();

        // GET: Invoices
        public ActionResult Index()
        {
            User user;
            if ((user = IsAuthorizedUser("read")) != null)
                return View(db.Invoices.ToList());
            return RedirectToAction("About", "Home");
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            User user;
            if ((user = IsAuthorizedUser("read")) != null)
                return View(invoice);
            return RedirectToAction("About", "Home");
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            User user;
            if ((user = IsAuthorizedUser("create")) != null)
                return View();
            return RedirectToAction("About", "Home");
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cost")] Invoice invoice)
        {
            User user;
            if ((user = IsAuthorizedUser("create")) != null)
                if (ModelState.IsValid)
                    {
                        db.Invoices.Add(invoice);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
            return RedirectToAction("About", "Home");
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            User user;
            if ((user = IsAuthorizedUser("update")) != null)
                return View(invoice);
            return RedirectToAction("About", "Home");
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cost")] Invoice invoice)
        {
            User user;
            if ((user = IsAuthorizedUser("update")) != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(invoice).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else return View(invoice);
            }
            return RedirectToAction("About", "Home");
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            User user;
            if ((user = IsAuthorizedUser("delete")) != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Invoice invoice = db.Invoices.Find(id);
                if (invoice == null)
                {
                    return HttpNotFound();
                }
                return View(invoice);
            }
            return RedirectToAction("About", "Home");
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user;
            if ((user = IsAuthorizedUser("delete")) != null)
            {
                Invoice invoice = db.Invoices.Find(id);
                db.Invoices.Remove(invoice);
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
                var classification = db.TableClearances.First(e => e.EntityName == "invoices");
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
