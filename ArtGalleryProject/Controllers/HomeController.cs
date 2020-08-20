using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtGalleryProject.ViewModel;
using ArtGalleryProject.Models;

namespace ArtGalleryProject.Controllers
{

  public class HomeController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult Index(string kerkimi)
    {
      
      HomePageViewModel homeModel = new HomePageViewModel();
      IEnumerable<ApplicationUser> artistet = null;
      IEnumerable<Artwork> veprat = null;
      if (!string.IsNullOrEmpty(kerkimi))
      {
        kerkimi = kerkimi.ToUpper();

        artistet = db.Users.Where(u => u.Emri.ToUpper().Contains(kerkimi)
          || u.Mbiemri.ToUpper().Contains(kerkimi) );
        veprat = db.Artworks.Where(a => a.titulli.ToUpper().Contains(kerkimi)
          || a.pershkrimi.ToUpper().Contains(kerkimi));
      }
      homeModel.artistet = artistet;
      homeModel.veprat = veprat;
      homeModel.kategorite = db.Kategorite.Select(n => n.kategoria).ToList();
      homeModel.stilet= db.Stilet.Select(n => n.stiili).ToList();
      homeModel.subjektet= db.Subjektet.Select(n => n.subjekti).ToList();
      return View(homeModel);
    }

    public ActionResult About()
    {
      return View();
    }
  }
}