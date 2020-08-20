using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGalleryProject.Models;

namespace ArtGalleryProject.ViewModel
{
  public class ArtworkDetails
  {
    public Artwork vepra { get; set; }
    public IEnumerable<KomenteLike> komenteLike { get; set; }
  }
}