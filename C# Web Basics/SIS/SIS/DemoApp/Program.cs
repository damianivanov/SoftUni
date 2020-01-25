using SIS.HTTP;
using System;
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
            HttpServer httpServer = new HttpServer(1998);
            await httpServer.StartAsync();
        }
    }
}
