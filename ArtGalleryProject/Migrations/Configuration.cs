namespace ArtGalleryProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ArtGalleryProject.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

  internal sealed class Configuration : DbMigrationsConfiguration<ArtGalleryProject.Models.ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(ArtGalleryProject.Models.ApplicationDbContext context)
    {
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method
      //  to avoid creating duplicate seed data.
      var userStore = new UserStore<ApplicationUser>(context);
      var userManager = new UserManager<ApplicationUser>(userStore);
      var roleStore = new RoleStore<IdentityRole>(context);
      var roleManager = new RoleManager<IdentityRole>(roleStore);

      const string name = "admin@artgallery.com";
      const string password = "Pa$$w0rd";
      const string roleName = "Admin";
      //Krijon Rolin Admin nqs ai nuk ekziston
      var role = roleManager.FindByName(roleName);
      if (role == null)
      {
        role = new IdentityRole(roleName);
        var roleresult = roleManager.Create(role);
      }

      var user = userManager.FindByName(name);
       if (user == null)
       {
          user = new ApplicationUser
          {
            UserName = name,
            Email = name
          };
          var result = userManager.Create(user, password);
          result = userManager.SetLockoutEnabled(user.Id, false);
       }
       var rolesForUser = userManager.GetRoles(user.Id);
       if (!rolesForUser.Contains(role.Name))
       {
         var result = userManager.AddToRole(user.Id, role.Name);
       }
       const string userRoleName = "Users";
       role = roleManager.FindByName(userRoleName);
       if (role == null)
       {
              role = new IdentityRole(userRoleName);
       var roleresult = roleManager.Create(role);
       }
    }
  }
}
