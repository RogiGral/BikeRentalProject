using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RentalInfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RentalInfoes
        public ActionResult Index()
        {
            var rentalInfos = db.RentalInfoes.Include(r => r.Bike).Include(r => r.User);
            return View(rentalInfos.ToList());
        }

        // GET: RentalInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalInfo rentalInfo = db.RentalInfoes.Find(id);
            if (rentalInfo == null)
            {
                return HttpNotFound();
            }
            return View(rentalInfo);
        }

        // GET: RentalInfoes/Create
        public ActionResult Create()
        {
            ViewBag.BikeID = new SelectList(db.Bikes, "BikeID", "BikeID");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserID");
            return View();
        }

        // POST: RentalInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,BikeID")] RentalInfo rentalInfo)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.RentalInfoes.Add(rentalInfo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.BikeID = new SelectList(db.Bikes, "BikeID", "BikeID", rentalInfo.BikeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserID", rentalInfo.UserID);
            return View(rentalInfo);
        }

        // GET: RentalInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalInfo rentalInfo = db.RentalInfoes.Find(id);
            if (rentalInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.BikeID = new SelectList(db.Bikes, "BikeID", "BikeID", rentalInfo.BikeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserID", rentalInfo.UserID);
            return View(rentalInfo);
        }

        // POST: RentalInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalInfoID,UserID,BikeID")] RentalInfo rentalInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BikeID = new SelectList(db.Bikes, "BikeID", "BikeID", rentalInfo.BikeID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserID", rentalInfo.UserID);
            return View(rentalInfo);
        }

        // GET: RentalInfoes/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            RentalInfo rentalInfo = db.RentalInfoes.Find(id);
            if (rentalInfo == null)
            {
                return HttpNotFound();
            }
            return View(rentalInfo);
        }

        // POST: RentalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalInfo rentalInfo = db.RentalInfoes.Find(id);
            db.RentalInfoes.Remove(rentalInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
