using System.Web.Mvc;
using System.Web.Routing;

namespace WorkshopUsuarios
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "DefaultLocalized",
                url: "{lang}/{controller}/{action}/{id}",
                constraints: new { lang = @"(\w{2})|(\w{2}-\w{2})" },
                defaults: new { controller = "Usuario", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
