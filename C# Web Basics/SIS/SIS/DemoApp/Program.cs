using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    //Actions:
    // => response IndexPage(request)
    // /favicon.ico => favicon.ico
    // GET/Contact =>response ShowContactForm(requeset)
    // POST/Contact = > response FillContactForm(request)
    // new HttpServer(80,)
    // .Start()

    class Program
    {
        static async Task Main(string[] args)
        {
            var routeTable = new List<Route>();
            routeTable.Add(new Route("/", HttpMethodType.Get, Index));
            routeTable.Add(new Route("/users/login", HttpMethodType.Get, Login));
            routeTable.Add(new Route("/users/login", HttpMethodType.Post, DoLogin));
            routeTable.Add(new Route("/contact", HttpMethodType.Get, Contact));
            routeTable.Add(new Route("/favicon,ico", HttpMethodType.Get, FavIcon));

            HttpServer httpServer = new HttpServer(1998, routeTable);
            await httpServer.StartAsync();
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            throw new NotImplementedException();
        }
        public static HttpResponse Index(HttpRequest request)
        {
            return new HtmlResponse("<h1>H O M E page</h1>");

        }

        private static HttpResponse Contact(HttpRequest request)
        {
            return new HtmlResponse("<h1>Contact page</h1>");
        }

        public static HttpResponse Login(HttpRequest request)
        {

            return new HtmlResponse("<h1>Login page</h1>");
        }

        public static HttpResponse DoLogin(HttpRequest request)
        {
            return new HtmlResponse("<h1>Dologin page</h1>");
        }
    }
}
