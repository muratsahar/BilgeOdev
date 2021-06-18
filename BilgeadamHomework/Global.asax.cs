using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BilgeadamHomework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());//yetkilendirme için 2 alternatif var: ilgili controller veya action seviyesinde [Authorize] yazmak veya global.asaxta filtre ekleyip istisnalara [Allowanonymous] yazmak. 
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
