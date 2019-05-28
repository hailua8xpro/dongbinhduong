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
    public class ProjectImagesController : BaseController
    {
        private JewelryContext db = new JewelryContext();
        private string _projectImageFolder = "/Uploads/images/projectimage";

        // GET: cms/ProjectImages
        public ActionResult Index()
        {
            return View(db.ProjectImages.ToList());
        }

        // GET: cms/ProjectImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectImage projectImage = db.ProjectImages.Find(id);
            if (projectImage == null)
            {
                return HttpNotFound();
            }
            return View(projectImage);
        }

        // GET: cms/ProjectImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cms/ProjectImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectImage projectImage, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                projectImage.CreatedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _projectImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        projectImage.ImageUrl = imageurl;
                    }
                }
                db.ProjectImages.Add(projectImage);
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

        // GET: cms/ProjectImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectImage projectImage = db.ProjectImages.Find(id);
            if (projectImage == null)
            {
                return HttpNotFound();
            }
            return View(projectImage);
        }

        // POST: cms/ProjectImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectImage projectImage, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                projectImage.ModifiedUserId = CurrentUser.AdminUserId;
                if (ImageFile != null)
                {
                    if (ImageFile != null && ImageFile.ContentLength > 0)
                    {
                        string error = "";
                        string imageurl = "";
                        var isupload = UploadImage(ref error, ref imageurl, ImageFile, "", _projectImageFolder);
                        if (!isupload)
                            return ReturnJson(error);
                        projectImage.ImageUrl = imageurl;
                    }
                }
                db.Entry(projectImage).State = EntityState.Modified;
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

        // GET: cms/ProjectImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectImage projectImage = db.ProjectImages.Find(id);
            if (projectImage == null)
            {
                return HttpNotFound();
            }
            return View(projectImage);
        }

        // POST: cms/ProjectImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectImage projectImage = db.ProjectImages.Find(id);
            db.ProjectImages.Remove(projectImage);
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
