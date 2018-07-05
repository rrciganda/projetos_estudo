using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EstudoMVC.Startup))]
namespace EstudoMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
