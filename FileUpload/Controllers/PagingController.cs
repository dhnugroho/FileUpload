using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileUpload.Models;
using System.Linq.Dynamic;

namespace FileUpload.Controllers
{
    public class PagingController : Controller
    {
        private dbFilesEntities db = new dbFilesEntities();

        // GET: Paging
        public ActionResult Index(int page = 1, string sort = "Name", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetData(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }


        public List<tbl_registration> GetData(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (dbFilesEntities dc = new dbFilesEntities())
            {
                var v = (from a in dc.tbl_registration
                         where
                                 a.Email.Contains(search) ||
                                 a.Password.Contains(search) ||
                                 a.Name.Contains(search) ||
                                 a.Address.Contains(search) ||
                                 a.City.Contains(search)
                         select a
                                );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }

        // GET: Paging/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_registration tbl_registration = db.tbl_registration.Find(id);
            if (tbl_registration == null)
            {
                return HttpNotFound();
            }
            return View(tbl_registration);
        }

        // GET: Paging/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paging/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sr_no,Email,Password,Name,Address,City,Age,Status")] tbl_registration tbl_registration)
        {
            if (ModelState.IsValid)
            {
                db.tbl_registration.Add(tbl_registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_registration);
        }

        // GET: Paging/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_registration tbl_registration = db.tbl_registration.Find(id);
            if (tbl_registration == null)
            {
                return HttpNotFound();
            }
            return View(tbl_registration);
        }

        // POST: Paging/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sr_no,Email,Password,Name,Address,City,Age,Status")] tbl_registration tbl_registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_registration);
        }

        // GET: Paging/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_registration tbl_registration = db.tbl_registration.Find(id);
            if (tbl_registration == null)
            {
                return HttpNotFound();
            }
            return View(tbl_registration);
        }

        // POST: Paging/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_registration tbl_registration = db.tbl_registration.Find(id);
            db.tbl_registration.Remove(tbl_registration);
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
