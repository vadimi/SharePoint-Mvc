using System.Web.Mvc;

namespace SPMvc.Core
{
    /// <summary>
    /// Bootstrapper for SharePoint MVC application
    /// </summary>
    public class Bootstrapper
    {
        private readonly IAreaConfiguration configuration;

        public Bootstrapper(IAreaConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// register view engine, register controller factory and routes
        /// </summary>
        /// <param name="webUrl"></param>
        public virtual void Init(string webUrl)
        {
            //register view engine to find views in Layouts folder
            RegisterViewEngine();

            //controller factory
            SetControllerFactory();

            //register routes
            RegisterRoutes(webUrl);
        }

        /// <summary>
        /// set new controller factory to find controllers in correct locations
        /// </summary>
        public virtual void SetControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(typeof(SPMvcControllerFactory));
        }

        /// <summary>
        /// Registers routes for area application
        /// </summary>
        /// <param name="webUrl"></param>
        void RegisterRoutes(string webUrl)
        {
            var routesMapper = new RoutesMapper(webUrl, configuration.AreaName);
            configuration.RegisterRoutes(routesMapper);
        }

        /// <summary>
        /// use just areas for mvc in SharePoint projects. This allows to use MVC in multiple site collections/sites
        /// </summary>
        static void RegisterViewEngine()
        {
            var viewEngine = new WebFormViewEngine
                                 {
                                     AreaMasterLocationFormats =
                                         new[]
                                             {
                                                 "~/_layouts/{2}/Views/{1}/{0}.master",
                                                 "~/_layouts/{2}/Views/Shared/{0}.master"
                                             },
                                     AreaViewLocationFormats =
                                         new[]
                                             {
                                                 "~/_layouts/{2}/Views/{1}/{0}.aspx",
                                                 "~/_layouts/{2}/Views/{1}/{0}.ascx",
                                                 "~/_layouts/{2}/Views/Shared/{0}.aspx",
                                                 "~/_layouts/{2}/Views/Shared/{0}.ascx"
                                             },
                                     MasterLocationFormats = null,
                                     ViewLocationFormats = null,
                                     PartialViewLocationFormats = null
                                 };
            viewEngine.AreaPartialViewLocationFormats = viewEngine.AreaViewLocationFormats;
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(viewEngine);
        }
    }
}