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
using WebApplication1.Security;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lastName_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "firstName_desc" : "FirstName";
            ViewBag.FullAddressSortParm = sortOrder == "FullAddress" ? "fullAddress_desc" : "FullAddress";
            ViewBag.EmailAddressSortParm = sortOrder == "EmailAddress" ? "emailAddress_desc" : "EmailAddress";
            ViewBag.RegisterDateSortParm = sortOrder == "RegisterDate" ? "registerDate_desc" : "RegisterDate";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var users = from s in db.Users
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)
                                       || s.FullAddress.Contains(searchString)
                                       || s.EmailAddress.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "lastName_desc":
                    users = users.OrderByDescending(s => s.LastName);
                    break;
                case "FirstName":
                    users = users.OrderBy(s => s.FirstName);
                    break;
                case "firstName_desc":
                    users = users.OrderByDescending(s => s.FirstName);
                    break;
                case "FullAddress":
                    users = users.OrderBy(s => s.FullAddress);
                    break;
                case "fullAddress_desc":
                    users = users.OrderByDescending(s => s.FullAddress);
                    break;
                case "EmailAddress":
                    users = users.OrderByDescending(s => s.EmailAddress);
                    break;
                case "emailAddress_desc":
                    users = users.OrderByDescending(s => s.EmailAddress);
                    break;
                case "RegisterDate":
                    users = users.OrderByDescending(s => s.RegisterDate);
                    break;
                case "registerDate_desc":
                    users = users.OrderByDescending(s => s.RegisterDate);
                    break;
                default:
                    users = users.OrderBy(s => s.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstName,FullAddress,EmailAddress,Password,RegisterDate,Role")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PasswordEncryption passwordEncryption = new PasswordEncryption();
                    user.Password = passwordEncryption.Encode(user.Password);
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,LastName,FirstName,FullAddress,EmailAddress,Password,RegisterDate,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
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
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
