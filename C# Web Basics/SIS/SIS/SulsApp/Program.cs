using SIS.HTTP;
using SIS.MvcFramework;
using SulsApp.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SulsApp
{
    class Program
    {
        static async Task Main()
        {

            await WebHost.StartAsync(new Startup());
       
        }
    }
}
