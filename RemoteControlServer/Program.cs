using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
namespace RemoteControlServer
{
    public enum CMD
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3
    };
    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://192.168.1.105:30303");
            wssv.AddWebSocketService<Controller>("/Controller");
            wssv.Start();
            Console.WriteLine("Websocket server running on port " + wssv.Port.ToString());
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
    
    public class Controller : WebSocketBehavior
    {
        CMD last = CMD.UP;
        protected override void OnMessage(MessageEventArgs e)
        {
            String[] data = e.Data.Split('|');
            if(e.Data=="req")
            {
                Send(((int)last).ToString());
            }
            switch(int.Parse(e.Data))
            {
                case (int)CMD.UP:
                    last = CMD.UP;
                    Console.WriteLine("UP"); 
                    break;
                case (int)CMD.DOWN:
                    Console.WriteLine("DOWN");
                    break;
                case (int)CMD.LEFT:
                    Console.WriteLine("LEFT");
                    break;
                case (int)CMD.RIGHT:
                    Console.WriteLine("RIGHT");
                    break;
            }
            Send(e.Data);
            base.OnMessage(e);
        }
    }
}
