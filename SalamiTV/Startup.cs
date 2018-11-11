using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalamiTV.Startup))]
namespace SalamiTV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
