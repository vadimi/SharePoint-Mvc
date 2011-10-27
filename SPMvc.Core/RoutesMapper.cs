using System.Web.Mvc;
using System.Web.Routing;

namespace SPMvc.Core
{
    /// <summary>
    /// register routes for MVC area only
    /// </summary>
    public abstract class RoutesMapper
    {
        private readonly string webUrl;
        private readonly string areaName;
        readonly AreaRegistrationContext areaContext;

        protected RoutesMapper(string webUrl, string areaName)
        {
            this.webUrl = webUrl;
            this.areaName = areaName;
            areaContext = new AreaRegistrationContext(areaName, RouteTable.Routes);
            RouteTable.Routes.RouteExistingFiles = true;
        }

        /// <summary>
        /// Registers new route through AreaRegistrationContext
        /// </summary>
        /// <param name="routeName">Route name, area name will be added automatically to achieve route name uniqueness</param>
        /// <param name="url">route url</param>
        /// <param name="defaults">route defaults</param>
        /// <param name="namespaces">namespaces where to search for controllers</param>
        public void RegisterRoute(string routeName, string url, object defaults, string[] namespaces)
        {
            areaContext.MapRoute(
                string.Format("{0}_{1}", areaName, routeName),
                string.Format("{0}/{1}", webUrl, url), defaults, namespaces);
        }

        public abstract void RegisterRoutes();
    }
}
