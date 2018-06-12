using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ytg.Chart.Server
{
    class Program
    {
        static void Main(string[] args)
        {


            Ytg.Chart.Comm.Web.YtgWebSocketServer server = new Comm.Web.YtgWebSocketServer();
            if (server.Start())
                Console.WriteLine("聊天服务启动成功！");
            else
                Console.WriteLine("聊天服务启动失败！");
            Console.ReadKey();
        }

      



      
    }
}
