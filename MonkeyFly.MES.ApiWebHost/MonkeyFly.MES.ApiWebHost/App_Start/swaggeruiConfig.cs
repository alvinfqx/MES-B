using MFSwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(swaggeruiConfig), "PreStart")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(swaggeruiConfig), "PostStart")]
namespace MFSwaggerUI
{
    public class swaggeruiConfig
    {
        public static void PreStart()
        {
            RouteTable.Routes.MapHttpRoute(
                name: "SwaggerApi",
                routeTemplate: "api/docs/{controller}/{action}",
                defaults: new { swagger = true }
            );
        }

        public static void PostStart()
        {
            var config = GlobalConfiguration.Configuration;
            config.Filters.Add(new MFSwaggerUI.SwaggerActionFilter());
        }
    }
}