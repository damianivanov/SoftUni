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
            this.Path = InfoHeaderParts[1];
            var httpVersion = InfoHeaderParts[2];
            this.Version = httpVersion switch
            {
                "HTTP/1.0"=>HttpVersionType.Http10,
                "HTTP/1.1" => HttpVersionType.Http11,
                "HTTP/2.0" => HttpVersionType.Http20,
                _=>HttpVersionType.Http11,
            };
            bool isInHeader = true;
            StringBuilder bodyBuilder = new StringBuilder();
            for (int i = 1; i < lines.Length; i++)
            {

                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeader = false;
                    continue;
                }

                if(isInHeader)
                {
                    var headerParts = line.Split(new string[] { ": " }, 2
                        , StringSplitOptions.None).ToArray();
                    if (headerParts.Length!=2)
                    {
                        throw new HttpServerException($"Invalid header: {line}");
                    }
                    Header header = new Header(headerParts[0], headerParts[1]);     
                    this.Headers.Add(header);
                    
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }
            }

        }



        public HttpMethodType Method { get; set; }
        public string Path { get; set; }
        public HttpVersionType Version { get; set; }
        public IList<Header> Headers { get; set; }
        public string Body { get; set; }

    }
}

