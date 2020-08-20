using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtGalleryProject.Startup))]
namespace ArtGalleryProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
