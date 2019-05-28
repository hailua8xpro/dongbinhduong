using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;

namespace NamTanUyen.Areas.cms.Controllers
{
    public class ViewCountsController : Controller
    {
        private JewelryContext db = new JewelryContext();

        // GET: cms/ViewCounts
        public ActionResult Index()
        {
            return View(db.ViewCounts.ToList());
        }

        // GET: cms/ViewCounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewCount viewCount = db.ViewCounts.Find(id);
            if (viewCount == null)
            {
                return HttpNotFound();
            }
            return View(viewCount);
        }

        // GET: cms/ViewCounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cms/ViewCounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ViewCountId,Url,CreatedDate,Note")] ViewCount viewCount)
        {
            if (ModelState.IsValid)
            {
                db.ViewCounts.Add(viewCount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viewCount);
        }

        // GET: cms/ViewCounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewCount viewCount = db.ViewCounts.Find(id);
            if (viewCount == null)
            {
                return HttpNotFound();
            }
            return View(viewCount);
        }

        // POST: cms/ViewCounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ViewCountId,Url,CreatedDate,Note")] ViewCount viewCount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewCount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewCount);
        }

        // GET: cms/ViewCounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewCount viewCount = db.ViewCounts.Find(id);
            if (viewCount == null)
            {
                return HttpNotFound();
            }
            return View(viewCount);
        }

        // POST: cms/ViewCounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewCount viewCount = db.ViewCounts.Find(id);
            db.ViewCounts.Remove(viewCount);
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
