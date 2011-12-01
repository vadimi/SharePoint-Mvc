using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace SPMvc.Core
{
    /// <summary>
    /// Provides a way to register one or more areas in an ASP.NET MVC application
    /// </summary>
    public class SPMvcAreaRegistrationContext
    {
        #region Fields

        private readonly string webUrl;

        private readonly AreaRegistrationContext context;

        #endregion

        #region Constructors
        
        public SPMvcAreaRegistrationContext(string webUrl, string areaName, RouteCollection routes, object state = null)
        {
            this.webUrl = webUrl;

            context = new AreaRegistrationContext(areaName, routes, state);
        }

        #endregion

        #region Properties

        public string WebUrl
        {
            get { return webUrl; }
        }

        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        /// <returns>
        /// The name of the area to register.
        /// </returns>
        public string AreaName
        {
            get { return context.AreaName; }
        }

        /// <summary>
        /// Gets the namespaces for the application.
        /// </summary>
        /// <returns>
        /// An enumerable set of namespaces for the application.
        /// </returns>
        public ICollection<string> Namespaces
        {
            get { return context.Namespaces; }
        }

        /// <summary>
        /// Gets a collection of defined routes for the application.
        /// </summary>
        /// <returns>
        /// A collection of defined routes for the application.
        /// </returns>
        public RouteCollection Routes
        {
            get { return context.Routes; }
        }

        /// <summary>
        /// Gets an object that contains user-defined information to pass to the area.
        /// </summary>
        /// <returns>
        /// An object that contains user-defined information to pass to the area.
        /// </returns>
        public object State
        {
            get { return context.State; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="name">The name of the route.</param><param name="url">The URL pattern for the route.</param><exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        public Route MapRoute(string name, string url)
        {
            return MapRoute(name, url, null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="name">The name of the route.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        public Route MapRoute(string name, string url, object defaults)
        {
            return MapRoute(name, url, defaults, null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values and constraint.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="name">The name of the route.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><param name="constraints">A set of expressions that specify valid values for a URL parameter.</param><exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        public Route MapRoute(string name, string url, object defaults, object constraints)
        {
            return MapRoute(name, url, defaults, constraints, null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="name">The name of the route.</param><param name="url">The URL pattern for the route.</param><param name="namespaces">An enumerable set of namespaces for the application.</param><exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        public Route MapRoute(string name, string url, string[] namespaces)
        {
            return MapRoute(name, url, namespaces, null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values and namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="name">The name of the route.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><param name="namespaces">An enumerable set of namespaces for the application.</param><exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        public Route MapRoute(string name, string url, object defaults, string[] namespaces)
        {
            return MapRoute(name, url, defaults, null, namespaces);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified by the <see cref="P:System.Web.Mvc.AreaRegistrationContext.AreaName"/> property, using the specified route default values, constraints, and namespaces.
        /// </summary>
        /// <returns>
        /// A reference to the mapped route.
        /// </returns>
        /// <param name="name">The name of the route.</param><param name="url">The URL pattern for the route.</param><param name="defaults">An object that contains default route values.</param><param name="constraints">A set of expressions that specify valid values for a URL parameter.</param><param name="namespaces">An enumerable set of namespaces for the application.</param><exception cref="T:System.ArgumentNullException">The <paramref name="url"/> parameter is null.</exception>
        public Route MapRoute(string name, string url, object defaults, object constraints, string[] namespaces)
        {
            return context.MapRoute(string.Format("{0}_{1}", AreaName, name), string.Format("{0}/{1}", webUrl, url).TrimStart('/'), defaults, constraints, namespaces);
        }

        #endregion
    }
}
