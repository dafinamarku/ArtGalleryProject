using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGalleryProject.Models;

namespace ArtGalleryProject.ViewModel
{
  public class ProfiliPerdoruesit
  {
    public string bio { get; set; }
    public string email { get; set; }
    public IEnumerable<Artwork> veprat { get; set; }
  }
}