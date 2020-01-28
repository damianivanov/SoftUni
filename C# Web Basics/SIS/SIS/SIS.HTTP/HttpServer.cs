using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SIS.HTTP;
namespace SIS.HTTP
{
    //Actions:
    // => response IndexPage(request)
    // /favicon.ico => favicon.ico
    // GET/Contact =>response ShowContactForm(requeset)
    // POST/Contact = > response FillContactForm(request)
    // new HttpServer(80,)
    // .Start()

    public class HttpServer : IHttpServer
    {
        private readonly TcpListener tcpListener;
        private readonly IList<Route> routeTable;
        private readonly IDictionary<string, IDictionary<string, string>> sessions;
  
      
        public HttpServer(int port, IList<Route> routeTable)
        {
            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
            this.routeTable = routeTable;
            this.sessions = new Dictionary<string, IDictionary<string, string>>();
        }

        public async Task StartAsync()
        {
            this.tcpListener.Start();
            Console.WriteLine("Starting server at port: "+tcpListener.LocalEndpoint);
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                Task.Run(() => ProcessClientAsync(tcpClient));
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using NetworkStream networkStream = tcpClient.GetStream();
            byte[] requestBytes = new byte[1000000];
            int readBytes = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);
            string requestString = Encoding.UTF8.GetString(requestBytes, 0, readBytes);

            var request = new HttpRequest(requestString);
            var SessionCookie = request.Cookies.FirstOrDefault(x => x.Name == HttpConstants.SessionCookieName);

            if (SessionCookie!=null &&this.sessions.ContainsKey(SessionCookie.Value))
            {
                request.SessionData = this.sessions[SessionCookie.Value];
            }
            Console.WriteLine($"{request.Method} {request.Path}");
            Console.WriteLine(new string('=', 70));

            var route = this.routeTable.FirstOrDefault(x => x.HttpMethod == request.Method &&
            x.Path == request.Path);
            HttpResponse response;
            if (route==null)
            {
                response = new HttpResponse(HttpResponseCode.NotFound, new byte[0]); 
            }
            else
            {
                response=route.Action(request);
            }

            response.Headers.Add(new Header("Server", "MyCustsomerServer / 1.0"));
            response.Headers.Add(new Header("Content-Type", "text / html"));

            var SessionCookie = request.Cookies.FirstOrDefault(x => x.Name == HttpConstants.SessionCookieName);

            if(SessionCookie==null || !this.sessions.ContainsKey(SessionCookie.Value))
            {
                var newSessionId = Guid.NewGuid().ToString();
                this.sessions.Add(newSessionId, new Dictionary<string,string>());
                response.Cookies.Add(new ResponseCookie(HttpConstants.SessionCookieName, newSessionId)
                {HttpOnly=true,MaxAge = 30*3600 });
            }
            
            byte[] responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            await networkStream.WriteAsync(response.Body, 0, response.Body.Length);

         

        }

        public void Stop()
        {
            this.tcpListener.Stop();
        }

        public async Task Reset()
        {
            this.Stop();
            await this.StartAsync();
        }
    }

}
