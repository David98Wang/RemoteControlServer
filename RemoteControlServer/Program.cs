using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
namespace RemoteControlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://localhost:4848");
            wssv.AddWebSocketService<Controller>("/Controller");
            wssv.Start();
            Console.WriteLine("Websocket server running on port " + wssv.Port.ToString());
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
    public class Controller : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send("Message Received");
            base.OnMessage(e);
        }
    }
}
