using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGalleryProject.Models;

namespace ArtGalleryProject.ViewModel
{
  public class HomePageViewModel
  {
    public List<string> kategorite { get; set; }
    public List<string> stilet { get; set; }
    public List<string> subjektet { get; set; }
    //per rezultatet e kerkimit
    public IEnumerable<ApplicationUser> artistet { get; set; }
    public IEnumerable<Artwork> veprat { get; set; }
  }
}