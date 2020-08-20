using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryProject.Models
{
  public class Subjekti
  {
    public int id { get; set; }
    [Required(ErrorMessage ="Duhet te zgjidhni nje subjekt.")]
    [StringLength(30, ErrorMessage = "Subjekti nuk te kete me shume se 30 karaktere.")]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "Subjekti duhet te permbaje vetem shkronja.")]
    [Display(Name ="Subjekti")]
    public string subjekti { get; set; }
    public virtual ICollection<Artwork> veprat { get; set; }
  }
}