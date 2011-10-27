using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace SPMvc.Core
{
    public class SPMvcControllerFactory : DefaultControllerFactory
    {
        private static readonly Dictionary<string, Type> typesCache = new Dictionary<string, Type>();
        private static readonly object syncObject = new object();

        /// <summary>
        /// lazy load controller types, use namespaces and areas to allow multiple mvc applications
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerName"></param>
        /// <returns></returns>
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            Type type;
            var area = requestContext.RouteData.DataTokens["area"] as string;
            //area token must be present
            if (string.IsNullOrEmpty(area))
                return null;

            var typeCacheKey = string.Format("{0}_{1}Controller", area, controllerName);
            if (!typesCache.TryGetValue(typeCacheKey, out type))
            {
                var namespaces = requestContext.RouteData.DataTokens["Namespaces"] as string[];
                if (namespaces != null)
                {
                    foreach (var ns in namespaces)
                    {
                        var controllerType = GetControllerType(string.Format("{0}.{1}Controller", ns, controllerName));
                        if (controllerType != null)
                        {
                            lock (syncObject)
                            {
                                if (!typesCache.ContainsKey(typeCacheKey))
                                    typesCache.Add(typeCacheKey, controllerType);
                            }
                            return controllerType;
                        }
                    }
                }
            }
            return type;
        }

        private Type GetControllerType(string fullName)
        {
            var controllerType = from type in Assembly.GetExecutingAssembly().GetTypes()
                                 where
                                     type.FullName != null &&
                                     type.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase)
                                 select type;

            return controllerType.SingleOrDefault();
        }
    }
}