using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkshopUsuarios.Startup))]
namespace WorkshopUsuarios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
