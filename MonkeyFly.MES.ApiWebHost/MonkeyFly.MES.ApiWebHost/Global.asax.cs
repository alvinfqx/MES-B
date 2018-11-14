using System;
using System.Web.Http;

namespace MonkeyFly.MES.ApiWebHost
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
            name: "MESApi",
            routeTemplate: "api/{controller}/{Action}/{id}",
            defaults: new { Action = RouteParameter.Optional, id = RouteParameter.Optional });

            GlobalConfiguration.Configuration.EnableCors();
        }
    }
}
