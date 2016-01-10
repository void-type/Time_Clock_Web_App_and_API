using Microsoft.Owin;
using Owin;
using Time_Clock_Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Time_Clock_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
