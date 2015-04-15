using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace AplicacionServidor
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Introduzca el Puerto");
            int port = int.Parse(Console.ReadLine());
            Console.WriteLine("Esperando para conexión...!!");

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, port);
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            sock.Bind(localEndPoint);
            sock.Listen(10);
            Socket handler = sock.Accept();
            Console.WriteLine("Conexión recibida de " + ((IPEndPoint)handler.RemoteEndPoint).Address.ToString());
            String data = null;

            byte[] bytes;
            while (true)
            {
                bytes = new byte[1024];
                
                

                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                   escribe(data);
                   
                    Console.ReadKey();    
                break;
                


            }
        }

        private static void escribe(string cad)
        {
            char[] delimiterChars = {  ',' };
            string[] words = cad.Split(delimiterChars);
            foreach (string s in words)
            {
                System.Console.WriteLine(s);
            }

           
        }
        


    }
}
