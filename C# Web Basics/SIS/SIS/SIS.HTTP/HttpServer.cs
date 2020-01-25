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
            string request = Encoding.UTF8.GetString(requestBytes, 0, readBytes);
            byte[] fileContent = Encoding.UTF8.GetBytes(@"
                                                        <form method='post'> 
                                                        <input name='username'/>
                                                        <input type='submit'/>
                                                        </form>
                                                        <h1> Hello World</h1>");
            string headers =   "HTTP/1.0 200 OK" + HttpConstants.NewLine +
                               "Server: MyCustsomerServer/1.0" + HttpConstants.NewLine +
                               "Content-Type: text/html" + HttpConstants.NewLine +
                               $"Content-Length: {fileContent.Length}" + HttpConstants.NewLine + HttpConstants.NewLine;
            byte[] headersBytes = Encoding.UTF8.GetBytes(headers);
            await networkStream.WriteAsync(headersBytes, 0, headersBytes.Length);
            await networkStream.WriteAsync(fileContent, 0, fileContent.Length);
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
