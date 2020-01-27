using SIS.HTTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP
{
    public class Route
    {
        public Route(string path, HttpMethodType httpMethod, Func<HttpRequest, HttpResponse> action)
        {
            this.Path = path;
            this.HttpMethod = httpMethod;
            this.Action = action;
        }
        public string Path { get; set; }

        public HttpMethodType HttpMethod { get; set; }

        public Func<HttpRequest,HttpResponse> Action { get; set; }




    }
}
