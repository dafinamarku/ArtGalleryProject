using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGalleryProject.Models
{
  public class KomenteLike
  {
    public int id { get; set; }
    public string koment { get; set; }
    public int VepraId { get; set; }
    public string artistiId { get; set; }
    public virtual ApplicationUser artisti { get; set; }
    public virtual Artwork vepra { get; set; }
  }
}