using SPMvc.Core;

namespace SPMvcSample.Web
{
    internal class AppRoutesMapper : RoutesMapper
    {
        public AppRoutesMapper(string webUrl, string areaName) : base(webUrl, areaName)
        {
        }

        public override void RegisterRoutes()
        {
            RegisterRoute("Home", "app.ashx/home", new {controller = "Home", action = "Index"},
                          new[] {"SPMvcSample.Web.Controllers"});

            RegisterRoute("About", "app.ashx/about", new { controller = "Home", action = "About" },
                          new[] { "SPMvcSample.Web.Controllers" });
        }
    }
}