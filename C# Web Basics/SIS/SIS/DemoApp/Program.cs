using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            await WebHost.StartAsync(new StartUp());
        }

        
    }
}
