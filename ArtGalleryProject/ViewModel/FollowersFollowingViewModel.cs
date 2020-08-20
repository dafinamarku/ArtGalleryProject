using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtGalleryProject.Models;

namespace ArtGalleryProject.ViewModel
{
  public class FollowersFollowingViewModel
  {
    public IEnumerable<ApplicationUser> following { get; set; }
    public IEnumerable<ApplicationUser> followers { get; set; }
  }
}