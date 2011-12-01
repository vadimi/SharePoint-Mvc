using SPMvc.Core;

namespace SPMvcSample.Web
{
    public class AppMvcRegistration : ISPMvcAreaRegistration
    {
        public string AreaName
        {
            get { return "mvcapp"; }
        }

        public void RegisterRoutes(SPMvcAreaRegistrationContext context)
        {
            context.MapRoute("Home", "app.ashx/home", new { controller = "Home", action = "Index" });
            context.MapRoute("About", "app.ashx/about", new { controller = "Home", action = "About" });
        }
    }
}
