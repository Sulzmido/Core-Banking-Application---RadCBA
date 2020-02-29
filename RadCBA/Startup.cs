using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RadCBA.Startup))]
namespace RadCBA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
