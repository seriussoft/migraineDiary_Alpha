using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MigraineDiaryMVC
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
			);


			////may need to remove if we have trouble (added this from SO: http://stackoverflow.com/questions/19882260/web-api-attribute-routes-in-mvc-5-exception-the-object-has-not-yet-been-initial
			//GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());
		}
	}
}
