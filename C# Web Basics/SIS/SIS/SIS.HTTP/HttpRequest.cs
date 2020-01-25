using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SIS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string httpRequestAsString)
        {
            this.Headers = new List<Header>();
            StringReader reader = new StringReader(httpRequestAsString);
            var lines = httpRequestAsString.Split(new string[] { HttpConstants.NewLine },
                StringSplitOptions.None).ToArray();
            var httpInfoHeader = lines[0];
            var InfoHeaderParts = httpInfoHeader.Split(' ');
            if (InfoHeaderParts.Length != 3)
            {
                throw new HttpServerException("Invalid HTTP Header line.");
            }
            var httpMethod = InfoHeaderParts[0];
            this.Method = httpMethod switch
            {
                "GET" => HttpMethodType.Get,
                "POST" => HttpMethodType.Post,
                "PUT" => HttpMethodType.Put,
                "DELETE" => HttpMethodType.Delete,
                 _ => HttpMethodType.Unknown,
            };  
        }



        public HttpMethodType Method { get; set; }
        public string Path { get; set; }
        public HttpVersionType Version { get; set; }
        public IEnumerable<Header> Headers { get; set; }
        public string Body { get; set; }
    }
}

