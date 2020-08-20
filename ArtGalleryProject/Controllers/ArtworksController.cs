using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.IO;
using System.Web.Mvc;
using System.Web.Helpers;
using ArtGalleryProject.Models;
using ArtGalleryProject.ViewModel;

namespace ArtGalleryProject.Controllers
{
    public class ArtworksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private bool ValidateFile(HttpPostedFileBase file)
        {
          string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
          string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
          if ((file.ContentLength > 0 && file.ContentLength < 7097152) &&
          allowedFileTypes.Contains(fileExtension))
          {
            return true;
          }
          return false;
        }

        private void RuajFile(HttpPostedFileBase file)
        {
          WebImage img = new WebImage(file.InputStream);
          img.Save(Konstante.VepraImazhPath + file.FileName);
        }

        // GET: Artworks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = await db.Artworks.FindAsync(id);
            /////////////////////////////////////////////////////////////
            var komentelikes = db.KomenteLike.Where(a => a.VepraId == id); //te gjitha komentet dhe like qe i jane bere vepres
            ArtworkDetails model = new ArtworkDetails
            {
              komenteLike = komentelikes,
              vepra = artwork
            };

            if (artwork == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [Authorize(Roles = "Users")]
        // GET: Artworks/Create
        public ActionResult Create()
        {
            
            ViewBag.KategoriaId = new SelectList(db.Kategorite, "id", "kategoria");
            ViewBag.SubjektiId = new SelectList(db.Subjektet, "id", "subjekti");
            ViewBag.StiliId = new SelectList(db.Stilet, "id", "stiili");
    
            return View();
        }

    // POST: Artworks/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Users")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file,[Bind(Include = "id,titulli,fotoEmri,pershkrimi,KategoriaId,SubjektiId,AutoriId,StiliId")] Artwork artwork)
        {
            if (file != null)
            {
              if (!ValidateFile(file))
              {
                ModelState.AddModelError("fotoEmri", "File duhet te jete imazh dhe me pak se 7MB");
              }
            }
            else
            {
              ModelState.AddModelError("fotoEmri", "Zgjidhni nje file");
            }
            if (ModelState.IsValid)
            {
                try
                {
                  RuajFile(file);
                }
                catch (Exception)
                {
                  ModelState.AddModelError("fotoEmri", "Nuk u ruajt file ne disk, provo perseri");
                }
                //perdoruesi mund te shtoje vepra vetem ne listen e tij te veprave
                artwork.AutoriId = User.Identity.GetUserId();
                artwork.fotoEmri = file.FileName;
                db.Artworks.Add(artwork);
                await db.SaveChangesAsync();
                return Redirect("/Manage/Index");
            }
            
            ViewBag.KategoriaId = new SelectList(db.Kategorite, "id", "kategoria", artwork.KategoriaId);
            ViewBag.SubjektiId = new SelectList(db.Subjektet, "id", "subjekti", artwork.SubjektiId);
            ViewBag.StiliId = new SelectList(db.Stilet, "id", "stiili");
            return View(artwork);
        }
    [Authorize(Roles = "Users")]
    // GET: Artworks/Edit/5
    public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = await db.Artworks.FindAsync(id);
            //faqet e editimit mund te aksesohen vetem nga autoret e veprave
            if (artwork == null || User.Identity.GetUserId()!=artwork.autori.Id)
            {
                return HttpNotFound();
            }
            ViewBag.AutoriId = new SelectList(db.Users, "Id", "bio", artwork.AutoriId);
            ViewBag.KategoriaId = new SelectList(db.Kategorite, "id", "kategoria", artwork.KategoriaId);
            ViewBag.SubjektiId = new SelectList(db.Subjektet, "id", "subjekti", artwork.SubjektiId);
            ViewBag.StiliId = new SelectList(db.Stilet, "id", "stiili", artwork.StiliId);
            return View(artwork);
        }


         [Authorize(Roles = "Users")]
        // POST: Artworks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( HttpPostedFileBase file, [Bind(Include = "id,titulli,pershkrimi,KategoriaId,SubjektiId,StiliId")] Artwork artwork)
        {
            if (file != null)
            {
              if (!ValidateFile(file))
              {
                ModelState.AddModelError("fotoEmri", "File duhet te jete imazh dhe me pak se 7MB");
              }
            }
           if (ModelState.IsValid)
           {
                if (file != null)
                {
                  try
                  {
                    RuajFile(file);
                    artwork.fotoEmri = file.FileName;
                  }
                  catch (Exception)
                  {
                    ModelState.AddModelError("fotoEmri", "Nuk u ruajt file ne disk, provo perseri");
                    return View(artwork);
                  }
                  
                }
                else
                {
          /////////////////////////////////////////////////////////////////////////////////////////
                  int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);
                  artwork.fotoEmri = db.Artworks.Where(a => a.id == id).Select(a => a.fotoEmri).ToString();

                }
                artwork.AutoriId = User.Identity.GetUserId();
                db.Entry(artwork).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { @id=artwork.id});
           }
            ViewBag.KategoriaId = new SelectList(db.Kategorite, "id", "kategoria", artwork.KategoriaId);
            ViewBag.SubjektiId = new SelectList(db.Subjektet, "id", "subjekti", artwork.SubjektiId);
            ViewBag.StiliId = new SelectList(db.Stilet, "id", "stiili", artwork.StiliId);
            return View(artwork);
        }
    [Authorize(Roles = "Users")]
    // GET: Artworks/Delete/5
    public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artwork artwork = await db.Artworks.FindAsync(id);
            //veprat mund te fshihen vetem nga autori i veprave
            if (artwork == null || User.Identity.GetUserId() != artwork.autori.Id)
            {
                return HttpNotFound();
            }
            return View(artwork);
        }
    [Authorize(Roles = "Users")]
    // POST: Artworks/Delete/5
    [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IEnumerable<KomenteLike> komente = db.KomenteLike.Where(k => k.VepraId == id);
            foreach(var koment in komente)
            {
              db.KomenteLike.Remove(koment);
            }
            Artwork artwork = await db.Artworks.FindAsync(id);
            db.Artworks.Remove(artwork);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Manage");
        }



    //tipi mund te marre vlerat 'kategori', 'subjekt', ose 'stil'
    //ndersa emri merr emrin e kategorise/stilit ose subjektit perkates te klikuar
    [Route("{tip:string}/{emri:string}")]
        public ActionResult Eksploro(string tip, string emri)
        {
          if(tip==null || emri==null)
          {
            return HttpNotFound();
          }
          IEnumerable<Artwork> veprat = null;
          if (tip == "kategori")
          {
            veprat = db.Artworks.Where(a => a.kategoriaVepra.kategoria == emri);
          }
          else {
            if (tip == "stil")
            {
              veprat = db.Artworks.Where(a => a.stiliVepra.stiili == emri);
            }
            else
            {
              if (tip == "subjekt")
              {
                veprat = db.Artworks.Where(a => a.subjektiVepra.subjekti == emri);
              }
              else
              {
                return HttpNotFound();
              }
            }
          }
          return View(veprat);
        }

        [Authorize]
        public ActionResult KomentoVepren(string komenti, int vepraId)
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser komentuesi = db.Users.Find(userId);
            KomenteLike komentIri = new KomenteLike();
            komentIri.artistiId = userId;
            komentIri.koment = komenti;
            komentIri.VepraId = vepraId;
            db.Artworks.Find(vepraId).komenteLikeNga.Add(komentIri);
            komentuesi.komente.Add(komentIri);
            db.KomenteLike.Add(komentIri);
            db.SaveChanges();
          return RedirectToAction("Details", "Artworks", new { id = vepraId });
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
