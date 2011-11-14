using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint;

namespace SPMvc.Core
{
    public abstract class SPMvcHttpHandler<T> : IHttpHandler where T : IAreaConfiguration, new()
    {
        static SPMvcHttpHandler()
        {
            var config = new T();
            var webUrl = SPContext.Current.Web.ServerRelativeUrl;
            if (!string.IsNullOrEmpty(webUrl))
                webUrl = webUrl.Trim(new[] { '/' });

            //init controller factory for this area
            SPMvcControllerFactory.Init(config.AreaName, typeof(T).Assembly);

            new Bootstrapper(config).Init(webUrl);
        }

        public void ProcessRequest(HttpContext context)
        {
            IHttpHandler httpHandler = new MvcHttpHandler();
            httpHandler.ProcessRequest(context);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
