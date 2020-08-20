using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryProject.Models
{
  public class Kategoria
  {
    public int id { get; set; }
    [Required(ErrorMessage ="Duhet te shkruani kategorine.")]
    [StringLength(30, ErrorMessage ="Kategoria nuk te kete me shume se 30 karaktere.")]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage ="Kategoria duhet te permbaje vetem shkronja.")]
    [Display(Name ="Kategoria")]
    public string kategoria { get; set; }
    public virtual ICollection<Artwork> veprat { get; set; }
  }
}