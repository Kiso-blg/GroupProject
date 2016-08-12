using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogSharpTeam.Startup))]
namespace BlogSharpTeam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //Filed
        }
    }
}
