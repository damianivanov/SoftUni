using System;
using System.IO;
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
        //Actions
        public HttpServer(int port)
        {

            this.tcpListener = new TcpListener(IPAddress.Loopback, port);
            
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

            string content = "<h1>random page</h1>";
            if (request.Path=="/")
            {
               content = "<h1>home page</h1>";
            }
            else if (request.Path=="/users/login")
            {
                content = "<h1>login page</h1>";
            }

            byte[] fileContent = Encoding.UTF8.GetBytes(content);
            var response = new HttpResponse(HttpResponseCode.Ok, fileContent);
            response.Headers.Add(new Header("Server", "MyCustsomerServer / 1.0"));
            response.Headers.Add(new Header("Content-Type", "text / html"));       
            
            byte[] responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            await networkStream.WriteAsync(response.Body, 0, response.Body.Length);

            Console.WriteLine(request);
            Console.WriteLine(new string('=', 70));

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
