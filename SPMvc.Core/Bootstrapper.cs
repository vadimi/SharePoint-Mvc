using System;
using System.Web.Mvc;

namespace SPMvc.Core
{
    /// <summary>
    /// Bootstrapper for SharePoint MVC application
    /// </summary>
    public class Bootstrapper
    {
        public virtual void Init(string webUrl)
        {
            //register view engine to find views in Layouts folder
            RegisterViewEngine();

            //register routes
            SetControllerFactory();
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
        /// <param name="routesMapper"></param>
        public virtual void RegisterRoutes(RoutesMapper routesMapper)
        {
            if (routesMapper == null)
                throw new ArgumentNullException("routesMapper");

            routesMapper.RegisterRoutes();
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