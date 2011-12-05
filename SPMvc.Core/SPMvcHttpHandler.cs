using System.Web.Mvc;
using Microsoft.SharePoint;

namespace SPMvc.Core
{
    public abstract class SPMvcHttpHandler<T> : MvcHttpHandler where T : ISPMvcAreaRegistration, new()
    {
        static SPMvcHttpHandler()
        {
            var webUrl = SPContext.Current.Web.ServerRelativeUrl;
            if (!string.IsNullOrEmpty(webUrl))
                webUrl = webUrl.Trim(new[] {'/'});

            new SPMvcBootstrapper(webUrl).Init<T>();
        }
    }
}