ASP.NET MVC 2.0 and SharePoint 2010 integration
===============================================

How to use this library
-----------------------

The source code contains two projects:

- **SPMvc.Core** - all base classes for ASP.NET MVC 2.0 integration with SharePoint 2010
- **SPMvcSample** - sample SharePoint solution that shows how to use the library and configure everything correctly

### Configuration steps
#### Follow the following guide.

1\. Reference **SPMvc.Core.dll** in your solution.

2\. Add SharePoint module with HttpHandler, for example App.ashx. The code behind of the handler should be the following:

```cs
public class App : SPMvcHttpHandler<AppMvcRegistration>
{
}
```

3\. Add AppMvcRegistration class:

```cs
public class AppMvcRegistration : ISPMvcAreaRegistration
{
	public string AreaName
	{
		get { return "mvcapp"; }
	}

	public void RegisterRoutes(SPMvcAreaRegistrationContext context)
	{
		context.MapRoute("Home", "app.ashx/home", new { controller = "Home", action = "Index" });
		context.MapRoute("About", "app.ashx/about", new { controller = "Home", action = "About" });
	}
}
```

4\. Add controllers in exactly the same assembly where AppMvcConfiguration is defined:

```cs
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
```

5\. All MVC content like views, scripts, styles, etc. should be added to **Layouts/MvcApp** folder. This is very important to name this directory exactly the same as the name of MVC Area!

6\. Deploy everything as **Farm** solution. If you want this solution to be deployed to 'Bin' folder: follow these [instructions](https://github.com/vadimi/SharePoint-Mvc/wiki/SharePoint-Mvc-and-'Bin'-deployment).

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

License: MIT [http://www.opensource.org/licenses/mit-license.php](http://www.opensource.org/licenses/mit-license.php)