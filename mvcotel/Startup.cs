using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvcotel.Startup))]
namespace mvcotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
