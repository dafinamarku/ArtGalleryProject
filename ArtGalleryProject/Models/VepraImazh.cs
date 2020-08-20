using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryProject.Models
{
  public class VepraImazh
  {
    public int ID { get; set; }
    [Display(Name = "Emri File")]
    public string EmriFile { get; set; }
  }
}