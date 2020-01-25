using System;
using System.Threading.Tasks;

namespace SIS.HTTP
{ 

   public interface IHttpServer
   {
       Task StartAsync();
       Task Reset();
       void Stop();
   }
    
}
