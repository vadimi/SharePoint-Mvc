ASP.NET MVC 2.0 and SharePoint 2010 integration
===============================================

How to use this library
-----------------------

The source code contains two projects:
**SPMvc.Core** - all base classes for ASP.NET MVC 2.0 integration with SharePoint 2010
**SPMvcSample** - sample SharePoint solution that shows how to use the library and configure everything correctly

### Configuration steps
Follow the following guide.

1. Add SharePoint module with HttpHandler, for example App.ashx. The code behind of the handler should be the following:

```
public class App : SPMvcHttpHandler<AppMvcConfiguration>
{
}
```

2. Add AppMvcConfiguration class:
```csharp
    public class AppMvcConfiguration : IAreaConfiguration
	{
		public string AreaName
		{
			get { return "mvcapp"; }
		}

		public void RegisterRoutes(RoutesMapper routesMapper)
		{
			routesMapper.RegisterRoute("Home", "app.ashx/home", new { controller = "Home", action = "Index" });
			routesMapper.RegisterRoute("About", "app.ashx/about", new { controller = "Home", action = "About" });
		}
	}
```

3. Add controllers in exactly the same assembly where AppMvcConfiguration is defined:

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewData["Message"] = "Home action message";
			return View();
		}

		public ActionResult About()
		{
			ViewData["Message"] = "About action message";
			return View();
		}
	}

4. All MVC content like views, scripts, styles, etc. should be added to **Layouts/MvcApp** folder. This is very important to name this directory exactly the same as the name of MVC Area!
5. Deploy everything as Farm solution.

### Result

At the end of the day the configuration above will produce the following urls:

* http://spserver/sites/mvcapp/app.ashx/home
* http://spserver/sites/mvcapp/app.ashx/about

Notes
-----

To allow *.ashx handlers to be included into wsp package with replacement tokens add the following element into csproj file:

```xml
<PropertyGroup>
	<TokenReplacementFileExtensions>ashx</TokenReplacementFileExtensions>
</PropertyGroup>
```