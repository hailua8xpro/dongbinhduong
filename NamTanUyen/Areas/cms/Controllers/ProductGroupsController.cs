using Library.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamTanUyen.Filters;
using NamTanUyen.Models;

namespace NamTanUyen.Areas.cms.Controllers
{
    [CustomAuthorizer]

    public class ProductGroupsController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _productImageFolder = "/Uploads/images/product/group";
        // GET: cms/ProductGroups
        public ActionResult Index()
        {
            var productGroups = db.ProductGroups.Include(p => p.ProductCategory);
            return View(productGroups.ToList());
        }

        // GET: cms/ProductGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // GET: cms/ProductGroups/Create
        public ActionResult Create()
        {
            ViewBag.ProductObjectId = new SelectList(db.ProductObjects, "ProductObjectId", "ProductObjectName");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "ProductCategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ProductGroup productGroup, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                productGroup.ProductGroupUrl = CommonHelper.BuildUrl(productGroup.ProductGroupName);
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productGroup.ImageUrl = imageurl;
                    }
                }
                db.ProductGroups.Add(productGroup);
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
        // GET: cms/ProductGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories.Where(x => x.ProductObjectId == productGroup.ProductObjectId), "ProductCategoryId", "ProductCategoryName", productGroup.ProductCategoryId);
            ViewBag.ProductObjectId = new SelectList(db.ProductObjects, "ProductObjectId", "ProductObjectName", productGroup.ProductObjectId);
            return View(productGroup);
        }

        // POST: cms/ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductGroup productGroup, HttpPostedFileBase ImageFile)
        {


            if (ModelState.IsValid)
            {
                productGroup.ProductGroupUrl = CommonHelper.BuildUrl(productGroup.ProductGroupName);
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _productImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        productGroup.ImageUrl = imageurl;
                    }
                }
                db.Entry(productGroup).State = EntityState.Modified;
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

        // GET: cms/ProductGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductGroup productGroup = db.ProductGroups.Find(id);
            if (productGroup == null)
            {
                return HttpNotFound();
            }
            return View(productGroup);
        }

        // POST: cms/ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGroup productGroup = db.ProductGroups.Find(id);
            db.ProductGroups.Remove(productGroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetGroupDropdownByCateId(int cateId)
        {
            var groups = db.ProductGroups.Where(x => x.ProductCategoryId == cateId);
            return PartialView(groups);
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
