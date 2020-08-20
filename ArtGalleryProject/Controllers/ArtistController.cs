using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ArtGalleryProject.Models;
using ArtGalleryProject.ViewModel;

namespace ArtGalleryProject.Controllers
{
      
      public class ArtistController : Controller
      {
          public ApplicationDbContext db=new ApplicationDbContext();
          // GET: Artist
          public ActionResult Index()
          {
              return View();
          }
        
          [Route("VizitoProfil/{id:string}")]
          public async Task<ActionResult> VizitoProfil(string id)//id e user eshte string ne tab AspNetUsers
          {
            string userId = User.Identity.GetUserId();
            if (id == null || userId==id)//nje perdorues nuk mund te shikoje profilin e tij si nje vizitor
            {
              return HttpNotFound();
            }
            else
            {
              ApplicationUser Ivizituari = db.Users.Find(id);
              if (Ivizituari == null)
              {
                return HttpNotFound();
              }

              if (Request.IsAuthenticated)
              {
                ViewBag.vizituesi = db.Users.Find(userId);
              }

              //renditja behet ne rendin zbrites, pra te parat do te shfaqen ato vepra qe jane postuar te fundit
              var veprat = db.Artworks.Where(a => a.autori.Id == id).OrderByDescending(a => a.id).AsEnumerable();
              var nrfollowing = Ivizituari.following.Count();
              var nrfollowers = Ivizituari.followers.Count();
              var model = new IndexViewModel
              {
                user = Ivizituari,
                veprat = veprat,
                nrfollowers=nrfollowers,
                nrfollowing=nrfollowing
              };
              return View(model);
            }
          }



          [Authorize(Roles = "Users")]
          //merret si parameter id e artistit te cilit user-i do ti bej follow/unfollow
          public ActionResult FollowUnfollow(string id)
          {
              string userId = User.Identity.GetUserId();
              //nje perdorues nuk mund ti bej follow/unfollow vetevetes
              if(id==null || id == userId)
              {
                return HttpNotFound();
              }
              ApplicationUser perdoruesiAktual = db.Users.Find(userId);
              //artisti te cilit perdoruesi aktual do ti bej follow/unfollow
              ApplicationUser p = db.Users.Find(id);
              //perdoruesi akrual e ka p ne listen e tij following, pra ai ka klikuar butonin per ta hequr ate nga kjo liste
              if (perdoruesiAktual.following.Contains(p))
              {
                perdoruesiAktual.following.Remove(p);
                p.followers.Remove(perdoruesiAktual);
              }
              else //perdoruesi aktual do ta shtoje p ne listen e tij following
              {
                perdoruesiAktual.following.Add(p);
                p.followers.Add(perdoruesiAktual);
              }
              db.SaveChangesAsync(); 
              return RedirectToAction("VizitoProfil", new { id = id });

          }



          [Authorize(Roles ="Users")]
          //tipi mund te marre vlerat 'following' ose 'followers', kurse id merr id e profilit per te cilin
          //ne duam te shohim listen e followers/following
          [Route("{tipi:string}/{id:string}")]
          public ActionResult FollowingFollowers(string tipi,string id)
          {
            if(tipi==null || id == null || (tipi!="following" && tipi!="followers"))
            {
              return HttpNotFound();
            }
            //perdoruesi per te cilin duam listen e following/followers
            ApplicationUser perdoruesi = db.Users.Find(id);
            FollowersFollowingViewModel model = new FollowersFollowingViewModel();
            if (tipi == "following")
            {
              model.followers = null;
              model.following = perdoruesi.following;
            }
            else //tipi=="followers"
            {
              model.followers = perdoruesi.followers;
              model.following = null;
            }
            return View(model);
          }

          [Authorize(Roles = "Users")]
          public ActionResult Timeline()
          {
            string userId = User.Identity.GetUserId();
            ApplicationUser perdoruesi = db.Users.Find(userId);
            var veprat = perdoruesi.following.SelectMany(f => f.veprat).OrderByDescending(f=>f.id);
            return View(veprat);
          }
      }
}