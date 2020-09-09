using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ballazza.Startup))]
namespace Ballazza
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
