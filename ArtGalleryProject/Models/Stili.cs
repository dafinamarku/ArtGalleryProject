using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryProject.Models
{
  public class Stili
  {
    public int id { get; set; }
    [Required(ErrorMessage ="Duhet te zgjidhni te pakten nje stil.")]
    [StringLength(31, ErrorMessage = "Stili nuk te kete me shume se 30 karaktere.")]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "Stili duhet te permbaje vetem shkronja.")]
    [Display(Name = "Stili")]
    public string stiili { get; set; }
    public virtual ICollection<Artwork> veprat { get; set; }
  }
}