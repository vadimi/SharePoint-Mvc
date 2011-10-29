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
            typesCache.TryGetValue(typeCacheKey, out type);
            return type;
        }

        /// <summary>
        /// Init controllers cache
        /// </summary>
        /// <param name="areaName"></param>
        public static void Init(string areaName)
        {
            AddControllersToCache(areaName, Assembly.GetCallingAssembly().GetTypes());
        }

        private static void AddControllersToCache(string areaName, IEnumerable<Type> types)
        {
            var controllerTypes = types.Where(IsControllerType);
            foreach (var controllerType in controllerTypes)
            {
                var typeCacheKey = string.Format("{0}_{1}", areaName, controllerType.Name);
                lock (syncObject)
                {
                    if (!typesCache.ContainsKey(typeCacheKey))
                        typesCache.Add(typeCacheKey, controllerType);
                }
            }
        }

        private static bool IsControllerType(Type type)
        {
            return ((((type != null) && type.IsPublic) &&
                     (type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) && !type.IsAbstract)) &&
                    typeof (IController).IsAssignableFrom(type));
        }
    }
}