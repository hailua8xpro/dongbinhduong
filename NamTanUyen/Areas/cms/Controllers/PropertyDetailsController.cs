using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Models;

namespace NamTanUyen.Areas.cms.Controllers
{
    public class PropertyDetailsController : BaseController
    {
        private JewelryContext db = new JewelryContext();

        // GET: cms/PropertyDetails
        public ActionResult Index(int propertyid)
        {
            var propertyDetails = db.PropertyDetails.Include(p => p.Property).Where(x => x.PropertyId == propertyid);
            ViewBag.propertyId = propertyid;
            return View(propertyDetails.ToList());
        }

        // GET: cms/PropertyDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyDetail propertyDetail = db.PropertyDetails.Find(id);
            if (propertyDetail == null)
            {
                return HttpNotFound();
            }
            return View(propertyDetail);
        }

        // GET: cms/PropertyDetails/Create
        public ActionResult Create(int propertyid)
        {
            ViewBag.propertyId = propertyid;
            return View();
        }

        // POST: cms/PropertyDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropertyDetail propertyDetail)
        {
            if (ModelState.IsValid)
            {
                propertyDetail.CreatedUserId = CurrentUser.AdminUserId;
                db.PropertyDetails.Add(propertyDetail);
                db.SaveChanges();
                return ReturnJson(result: 1);
            }
            string errors = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + Environment.NewLine;
                }
            }
            return ReturnJson(errors);

        }

        // GET: cms/PropertyDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyDetail propertyDetail = db.PropertyDetails.Find(id);
            if (propertyDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "PropertyId", propertyDetail.PropertyId);
            return View(propertyDetail);
        }

        // POST: cms/PropertyDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PropertyDetail propertyDetail)
        {
            if (ModelState.IsValid)
            {
                var oldpropertyDetail = db.PropertyDetails.Find(propertyDetail.PropertyDetailId);
                propertyDetail.ModifiedUserId = CurrentUser.AdminUserId;
                propertyDetail.ModifiedDate = DateTime.Now;
                db.Entry(propertyDetail).State = EntityState.Modified;
                db.SaveChanges();
                var param = new SqlParameter[] {
                new SqlParameter {ParameterName="Mode",Value=1 },
                new SqlParameter {ParameterName="Id",Value=propertyDetail.PropertyDetailId },
                new SqlParameter {ParameterName="OldValue",Value=oldpropertyDetail.PropertyValue },
                new SqlParameter{ParameterName="NewValue",Value=propertyDetail.PropertyValue}
            };
                db.Database.ExecuteSqlCommand("Product_UpdateListPropertyValue @Mode,@Id,@OldValue,@NewValue", param);
                return ReturnJson(result: 1);
            }
            string errors = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors += error.ErrorMessage + Environment.NewLine;
                }
            }
            return ReturnJson(error: errors);
        }

        // GET: cms/PropertyDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyDetail propertyDetail = db.PropertyDetails.Find(id);
            if (propertyDetail == null)
            {
                return HttpNotFound();
            }
            return View(propertyDetail);
        }

        // POST: cms/PropertyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropertyDetail propertyDetail = db.PropertyDetails.Find(id);
            db.PropertyDetails.Remove(propertyDetail);
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
