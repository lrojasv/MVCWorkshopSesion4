using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WorkshopUsuarios.Helpers
{
    public static class SeleccionIdiomas
    {
        private static Dictionary<string, string> Idiomas = new Dictionary<string, string>
        {
            {"en-US","English"},
            {"es-PE","Spanish"}
        };

        public static IHtmlString Seleccion(this HtmlHelper helper, string lang)
        {
            StringBuilder componente = new StringBuilder();

            Idiomas.Where(x=> x.Key != lang).ToList().ForEach(x =>
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes.Add("href", string.Format("/{0}/Usuario/ChangeLanguage", x.Key));
                tb.SetInnerText(x.Value);
                componente.Append(tb.ToString());
            });

            return new MvcHtmlString(componente.ToString());
        }
    }
}