using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace WorkshopUsuarios.Controllers
{
    public class LocalizedControllerActivator : IControllerActivator
    {
        private string _DefaultLanguage = "en";

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            //Get the {language} parameter in the RouteData
            string lang = requestContext.RouteData.Values["lang"]==null ? _DefaultLanguage: requestContext.RouteData.Values["lang"].ToString();

            if (lang != _DefaultLanguage)
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                }
                catch (Exception e)
                {
                    throw new NotSupportedException(String.Format("ERROR: Invalid language code '{0}'.", lang));
                }
            }

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}