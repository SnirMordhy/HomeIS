using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeIS.Startup))]
namespace HomeIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
