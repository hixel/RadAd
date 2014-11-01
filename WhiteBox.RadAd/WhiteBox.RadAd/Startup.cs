using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhiteBox.RadAd.Startup))]
namespace WhiteBox.RadAd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
