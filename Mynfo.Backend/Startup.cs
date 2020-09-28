using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mynfo.Backend.Startup))]
namespace Mynfo.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
