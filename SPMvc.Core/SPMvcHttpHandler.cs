using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint;

namespace SPMvc.Core
{
    public abstract class SPMvcHttpHandler<T> : IHttpHandler where T : ISPMvcAreaRegistration, new()
    {
        static SPMvcHttpHandler()
        {
            var webUrl = SPContext.Current.Web.ServerRelativeUrl;
            if (!string.IsNullOrEmpty(webUrl))
                webUrl = webUrl.Trim(new[] {'/'});

            new SPMvcBootstrapper(webUrl).Init<T>();
        }

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. 
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            IHttpHandler httpHandler = new MvcHttpHandler();
            httpHandler.ProcessRequest(context);
        }
    }
}