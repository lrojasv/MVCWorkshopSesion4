using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using WorkshopUsuarios.Controllers;

[assembly: OwinStartupAttribute(typeof(WorkshopUsuarios.Startup))]
namespace WorkshopUsuarios
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(new LocalizedControllerActivator()));
        }

    }
}

