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
    public class PropertiesController : BaseController
    {
        private JewelryContext db = new JewelryContext();

        // GET: cms/Properties
        public ActionResult Index()
        {
            return View(db.Properties.ToList());
        }

        // GET: cms/Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: cms/Properties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cms/Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Property property)
        {
            if (ModelState.IsValid)
            {
                property.CreatedUserId = CurrentUser.AdminUserId;
                db.Properties.Add(property);
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
            return ReturnJson(error: errors);
        }

        // GET: cms/Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: cms/Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Property property)
        {
            if (ModelState.IsValid)
            {
                var oldproperty = db.Properties.Find(property.PropertyId);
                property.ModifiedUserId = CurrentUser.AdminUserId;
                property.ModifiedDate = DateTime.Now;
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                var param = new SqlParameter[] {
                new SqlParameter {ParameterName="Mode",Value=1 },
                new SqlParameter {ParameterName="Id",Value=property.PropertyId},
                new SqlParameter {ParameterName="OldValue",Value=oldproperty.IsMainProp?1:0 },
                new SqlParameter{ParameterName="NewValue",Value=oldproperty.IsMainProp?1:0}
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

        // GET: cms/Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: cms/Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ProductProperty(int productId)
        {
            ViewBag.ProductId = productId;
            var property = db.Properties.Where(x => x.IsActive == true);
            ViewBag.ListpropertyId = db.Product_Properties.Where(x => x.ProductId == productId).Select(y => y.PropertyDetailId);
            return View(property);
        }
        [HttpPost]
        public ActionResult UpdateProductProperty(int productId, IEnumerable<int> lstPropDetailid)
        {
            try
            {
                var existedprop = db.Product_Properties.Where(x => x.ProductId == productId);
                if ((existedprop == null || !existedprop.Any()) && lstPropDetailid == null)
                {
                    return ReturnJson(error: "Bạn chưa chọn thuộc tính nào");
                }
                if (lstPropDetailid != null && lstPropDetailid.Any())
                {
                    db.Database.ExecuteSqlCommand("delete from Product_Property where ProductId={0}", productId);
                    foreach (var item in lstPropDetailid)
                    {
                        var prop = new ProductProperty { ProductId = productId, PropertyDetailId = item };
                        db.Product_Properties.Add(prop);
                    }
                    db.SaveChanges();
                }
                return ReturnJson(result: 1);
            }
            catch (Exception ex)
            {

                return ReturnJson(error: ex.Message);
            }

        }

        [HttpPost]
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
