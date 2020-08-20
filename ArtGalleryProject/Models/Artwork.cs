using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ArtGalleryProject.Bonus;

namespace ArtGalleryProject.Models
{
  public class Artwork
  {
    public int id { get; set; }
    [Display(Name = "Titulli")]
    [MaxChars(30, ErrorMessage ="Titulli nuk mund te permbaje me shume se 30 karaktere")] //validatori i personalizuar nuk lejon qe titulli te kete fjale me me shume se 30 karaktere
    [Required(ErrorMessage = "Duhet te plotesoni fushen {0}.")]
    [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "Titulli duhet te permbaje vetem shkronja.")]
    public string titulli { get; set; }
    [Display(Name ="Foto")]
    public string fotoEmri { get; set; }
    [Display(Name = "Pershkrimi")]
    [StringLength(500, ErrorMessage = "Pershkrimi nuk duhet te jet me i gjate se 500 karaktere.")]
    public string pershkrimi { get; set; }
    public int KategoriaId { get; set; }
    public int SubjektiId { get; set; }
    public string AutoriId { get; set; }
    public int StiliId { get; set; }
    public virtual Kategoria kategoriaVepra { get; set; }
    public virtual Subjekti subjektiVepra { get; set; }
    public virtual ApplicationUser autori { get; set; }
    public virtual Stili stiliVepra { get; set; }
    public virtual ICollection<KomenteLike> komenteLikeNga { get; set; }
  }
}