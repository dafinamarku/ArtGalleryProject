using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ArtGalleryProject.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUser> following { get; set; }
        public virtual ICollection<ApplicationUser> followers { get; set; }
        public virtual ICollection<KomenteLike> komente { get; set; }
        public virtual ICollection<Artwork> veprat { get; set; }
        [Display(Name = "Rreth Autorit")]
        [StringLength(500, ErrorMessage = "Pershkrimi nuk duhet te jet me i gjate se 500 karaktere.")]
        public string bio { get; set; }
        [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "{0} duhet te permbaje vetem shkronja.")]
        [StringLength(30, ErrorMessage = "{0} nuk duhet te jet me i gjate se 30 karaktere.")]
        public string Emri { get; set; }
        [RegularExpression("^[a-z A-Z]*$", ErrorMessage = "{0} duhet te permbaje vetem shkronja.")]
        [StringLength(30, ErrorMessage = "{0} nuk duhet te jet me i gjate se 30 karaktere.")]
        public string Mbiemri { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Artwork> Artworks { get; set; }
        public DbSet<VepraImazh> VepraImazhe { get; set; }
        public DbSet<Kategoria> Kategorite { get; set; }
        public DbSet<Stili> Stilet { get; set; }
        public DbSet<Subjekti> Subjektet { get; set; }
        public DbSet<KomenteLike> KomenteLike { get; set; }
    /// <summary>
    /// /////////////
    /// </summary>

    public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}