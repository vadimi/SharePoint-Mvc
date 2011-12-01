using System.Web.Mvc;

namespace SPMvc.Core
{
    public class SPMvcViewEngine : WebFormViewEngine
    {
        public SPMvcViewEngine()
        {
            AreaMasterLocationFormats =
                new[]
                    {
                        "~/_layouts/{2}/Views/{1}/{0}.master",
                        "~/_layouts/{2}/Views/Shared/{0}.master"
                    };
            AreaViewLocationFormats =
                new[]
                    {
                        "~/_layouts/{2}/Views/{1}/{0}.aspx",
                        "~/_layouts/{2}/Views/{1}/{0}.ascx",
                        "~/_layouts/{2}/Views/Shared/{0}.aspx",
                        "~/_layouts/{2}/Views/Shared/{0}.ascx"
                    };
            MasterLocationFormats = null;
            ViewLocationFormats = null;
            PartialViewLocationFormats = null;
            AreaPartialViewLocationFormats = AreaViewLocationFormats;
        }
    }
}