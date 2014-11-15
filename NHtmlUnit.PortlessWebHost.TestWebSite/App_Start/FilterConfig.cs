using System.Web;
using System.Web.Mvc;

namespace NHtmlUnit.PortlessWebHost.TestWebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
