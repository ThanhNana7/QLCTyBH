using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BH.Startup))]
namespace BH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
