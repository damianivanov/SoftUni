using SIS.HTTP;
using SIS.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace SIS.MvcFramework
{
    public abstract class Controller
    {
        protected HttpResponse View([CallerMemberName]string viewName=null)
        {
            var layout = File.ReadAllText("Views/Shared/_Layout.html");
            var ControllerName = this.GetType().Name.Replace("Controller",string.Empty);
            var html = File.ReadAllText("Views/"+ ControllerName+"/"+viewName+".html"); 
            var BodyWithLayout = layout.Replace("@RenderBody()", html);
            return new HtmlResponse(BodyWithLayout);
        }
    }
}
