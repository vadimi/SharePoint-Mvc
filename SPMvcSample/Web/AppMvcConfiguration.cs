using SPMvc.Core;

namespace SPMvcSample.Web
{
    public class AppMvcConfiguration : IAreaConfiguration
    {
        public string AreaName
        {
            get { return "mvcapp"; }
        }

        public void RegisterRoutes(RoutesMapper routesMapper)
        {
            routesMapper.RegisterRoute("Home", "app.ashx/home", new { controller = "Home", action = "Index" });
            routesMapper.RegisterRoute("About", "app.ashx/about", new { controller = "Home", action = "About" });
        }
    }
}
