using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DemoApp
{
    public class StartUp : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        { 
            routeTable.Add(new Route("/", HttpMethodType.Get, Index));
            routeTable.Add(new Route("/Tweets/Create", HttpMethodType.Post, CreateTweet));
            routeTable.Add(new Route("/favicon.ico", HttpMethodType.Get, FavIcon));
        }

        public void ConfigureServices()
        {
            var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }

        private static HttpResponse FavIcon(HttpRequest request)
        {
            var byteContent = File.ReadAllBytes("wwwroot/hashtag.ico");
            return new FileResponse(byteContent, "image/x-icon");
        }
        public static HttpResponse Index(HttpRequest request)
        {

            var db = new ApplicationDbContext();
            var tweets = db.Tweets.Select(t => new
            {
                t.CreatedOn,
                t.Creator,
                t.Content
            }).ToList();

            StringBuilder html = new StringBuilder();
            html.Append("<table><tr><th>Date</th><th>Creator</th><th>Content</th></tr>");
            foreach (var tweet in tweets)
            {
                html.Append($"<tr><td>{tweet.CreatedOn}</td><td>{tweet.Creator}</td><td>{tweet.Content}</td></tr>");
            }
            html.Append("</table>");
            html.Append(@"<form action= '/Tweets/Create' method='post'>
                                        <textarea name='tweetName'> </textarea>  
                                           <br /> 
                                        <input name='creator' />    
                                           <br />
                                        <input type='submit' />
                                      </form>");
            return new HtmlResponse(html.ToString());

        }
        public static HttpResponse CreateTweet(HttpRequest request)
        {
            var db = new ApplicationDbContext();
            db.Tweets.Add(new Tweet
            {
                CreatedOn = DateTime.UtcNow,
                Creator = request.FormData["creator"],
                Content = request.FormData["tweetName"],

            });
            db.SaveChanges();
            return new RedirectResponse("/");
        }
    }
}
