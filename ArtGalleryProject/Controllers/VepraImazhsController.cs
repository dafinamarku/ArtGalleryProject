using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtGalleryProject.Models;
using System.Web.Helpers;

namespace ArtGalleryProject.Controllers
{
    [Authorize]
    public class VepraImazhsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        private bool ValidateFile(HttpPostedFileBase file)
            {
              string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
              string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
              if ((file.ContentLength > 0 && file.ContentLength < 2097152) &&
              allowedFileTypes.Contains(fileExtension))
              {
                return true;
              }
              return false;
         }

        private void RuajFile(HttpPostedFileBase file)
        {
          WebImage img = new WebImage(file.InputStream);
          if (img.Width > 190)
          {
            img.Resize(190, img.Height);
          }
          img.Save(Konstante.VepraImazhPath + file.FileName);
        }


    // GET: VepraImazhs
    public ActionResult Index()
        {
            return View(db.VepraImazhe.ToList());
        }

        // GET: VepraImazhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VepraImazh vepraImazh = db.VepraImazhe.Find(id);
            if (vepraImazh == null)
            {
                return HttpNotFound();
            }
            return View(vepraImazh);
        }

        // GET: VepraImazhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VepraImazhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file)
        {
            if (file != null)
            {
              if (ValidateFile(file))
              {
                try
                { RuajFile(file);
                }
                catch (Exception)
                {
                  ModelState.AddModelError("EmriFile", "Nuk u ruajt file ne disk, provo perseri");
                }
              }
              else
              {
                ModelState.AddModelError("EmriFile", "File duhet te jete imazh dhe me pak se 2MB");
              }
            }
            else
            {
              ModelState.AddModelError("EmriFile", "Zgjidhni nje file");
            }
            if (ModelState.IsValid)
            {
              db.VepraImazhe.Add(new VepraImazh { EmriFile = file.FileName });
              db.SaveChanges();
              return RedirectToAction("Index");
            }
            return View();
         }

        // GET: VepraImazhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VepraImazh vepraImazh = db.VepraImazhe.Find(id);
            if (vepraImazh == null)
            {
                return HttpNotFound();
            }
            return View(vepraImazh);
        }

        // POST: VepraImazhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmriFile")] VepraImazh vepraImazh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vepraImazh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vepraImazh);
        }

        // GET: VepraImazhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VepraImazh vepraImazh = db.VepraImazhe.Find(id);
            if (vepraImazh == null)
            {
                return HttpNotFound();
            }
            return View(vepraImazh);
        }

        // POST: VepraImazhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VepraImazh vepraImazh = db.VepraImazhe.Find(id);
            db.VepraImazhe.Remove(vepraImazh);
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
