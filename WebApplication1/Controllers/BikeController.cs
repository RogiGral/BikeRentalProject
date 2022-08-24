using PagedList;
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
    public class BikeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TypeSortParm = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.PriceSortParm = sortOrder == "Address" ? "ddress_desc" : "Address";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var bikes = from s in db.Bikes
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                bikes = bikes.Where(s => s.Description.Contains(searchString)
                                       || s.Type.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "type_desc":
                    bikes = bikes.OrderByDescending(s => s.Type);
                    break;
                case "Price":
                    bikes = bikes.OrderBy(s => s.PricePerHour);
                    break;
                case "price_desc":
                    bikes = bikes.OrderByDescending(s => s.PricePerHour);
                    break;
                default:
                    bikes = bikes.OrderBy(s => s.Type);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(bikes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Bikes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // GET: Bikes/Create
        public ActionResult Create()
        {
            ViewBag.ParkingID = new SelectList(db.Parkings, "ParkingID", "Address");
            return View();
        }

        // POST: Bikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParkingID,Type,Description,PricePerHour")] Bike bike)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Bikes.Add(bike);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.ParkingID = new SelectList(db.Parkings, "ParkingID", "Address", bike.ParkingID);
            return View(bike);
        }

        // GET: Bikes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParkingID = new SelectList(db.Parkings, "ParkingID", "Address", bike.ParkingID);
            return View(bike);
        }

        // POST: Bikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BikeID,ParkingID,Type,Description,PricePerHour")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParkingID = new SelectList(db.Parkings, "ParkingID", "Address", bike.ParkingID);
            return View(bike);
        }

        // GET: Bikes/Delete/5
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
            Bike bike = db.Bikes.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // POST: Bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike bike = db.Bikes.Find(id);
            db.Bikes.Remove(bike);
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
