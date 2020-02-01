using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices()
        {
            var db = new SulsDbContext();
            db.Database.EnsureCreated();    
        }

        public void Configure(IList<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethodType.Get, new HomeController().Index));
            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethodType.Get, new StaticFilesController().Bootstrap));
            routeTable.Add(new Route("/css/site.css", HttpMethodType.Get, new StaticFilesController().Site));
            routeTable.Add(new Route("/css/reset.css", HttpMethodType.Get, new StaticFilesController().Reset));
            routeTable.Add(new Route("/Users/Login", HttpMethodType.Get, new UsersController().Login));
            routeTable.Add(new Route("/Users/Register", HttpMethodType.Get, new UsersController().Register));
        }
    }
}
