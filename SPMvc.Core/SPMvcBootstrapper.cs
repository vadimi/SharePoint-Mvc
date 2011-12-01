using System.Web.Mvc;
using System.Web.Routing;

namespace SPMvc.Core
{
    /// <summary>
    /// Bootstrapper for SharePoint MVC application
    /// </summary>
    public sealed class SPMvcBootstrapper
    {
        #region Fields

        readonly string webUrl;

        ISPMvcAreaRegistration areaRegistration;
        SPMvcAreaRegistrationContext areaRegistrationContext;

        #endregion

        #region Constructors

        static SPMvcBootstrapper()
        {
            //register view engine only once to find views in Layouts folder
            RegisterViewEngine();

            //register controller factory only once per all sites
            RegisterControllerFactory();
        }

        public SPMvcBootstrapper(string webUrl)
        {
            this.webUrl = webUrl;
        }

        #endregion

        #region Methods

        /// <summary>
        /// register view engine, register controller factory and routes
        /// </summary>
        public void Init<T>() where T : ISPMvcAreaRegistration, new()
        {
            areaRegistration = new T();
            areaRegistrationContext = new SPMvcAreaRegistrationContext(
                webUrl, areaRegistration.AreaName, RouteTable.Routes);

            SPMvcControllerFactory.Init(areaRegistration);

            //register routes
            RegisterRoutes();
        }

        /// <summary>
        /// Registers routes for area application
        /// </summary>
        void RegisterRoutes()
        {
            areaRegistrationContext.Routes.RouteExistingFiles = true;
            areaRegistration.RegisterRoutes(areaRegistrationContext);
        }

        /// <summary>
        /// Set controller factory that distinguishes controllers for areas
        /// </summary>
        static void RegisterControllerFactory()
        {
            ControllerBuilder.Current.SetControllerFactory(typeof(SPMvcControllerFactory));
        }

        /// <summary>
        /// use just areas for mvc in SharePoint projects. This allows to use MVC in multiple site collections/sites
        /// </summary>
        static void RegisterViewEngine()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new SPMvcViewEngine());
        }

        #endregion
    }
}