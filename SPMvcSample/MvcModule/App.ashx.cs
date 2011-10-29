using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint;
using SPMvc.Core;
using SPMvcSample.Web;

namespace SPMvcSample.MvcModule
{
    public class App : IHttpHandler
    {
        const string AREA_NAME = "mvcapp";

        static App()
        {
            var webUrl = SPContext.Current.Web.ServerRelativeUrl;
            if (!string.IsNullOrEmpty(webUrl))
                webUrl = webUrl.Trim(new[] { '/' });

            var bootstrapper = new Bootstrapper();
            //init controller factory for this area
            SPMvcControllerFactory.Init(AREA_NAME);

            //areaName should be a part of webUrl
            bootstrapper.Init(new AppRoutesMapper(webUrl, AREA_NAME));
        }

        public void ProcessRequest(HttpContext httpContext)
        {
            IHttpHandler httpHandler = new MvcHttpHandler();
            httpHandler.ProcessRequest(httpContext);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}