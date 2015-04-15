using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

namespace AplicacionCliente
{
    class Program
    {
       //

        private static string Cliente(IPAddress servidor, int puerto)
        {
            try
            {
                string request = "";
                Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
                Byte[] bytesReceived = new Byte[256];

                // Crear socket ip, puerto
                IPEndPoint ipe = new IPEndPoint(servidor, puerto);
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(ipe);

                if (s == null) return "Connexion falló!";

                s.Send(bytesSent, bytesSent.Length, 0);

                try
                {

                   string cadena = "Nombre PC "+","+nombrePc()+","+"Nombre Ususario"+","+usuarioPc()+","+"Direccion MAC"+","+MAC()+","+"Fecha y Hora Acceso"+","+fechaHora()+","+ "Unidades"+","+unidades();
                    byte[] msg = Encoding.ASCII.GetBytes(cadena);
                    s.Send(msg);
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();

                

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            catch (Exception se)
            {
                Console.WriteLine("Error en conexión" + se.StackTrace);
            }
            return "";
        }


        private static string nombrePc()
        {
            string HostName = System.Net.Dns.GetHostEntry("LocalHost").HostName;
            return HostName;
        
        }

        private static string usuarioPc()
        {
            string userName = Environment.UserName;
            return userName;

        }
        private static string fechaHora() {
        
           return  DateTime.Now.ToString("G");
        
        }

        private static string MAC() {

            NetworkInterface[] lista = NetworkInterface.GetAllNetworkInterfaces();
            if (lista.Length > 0)
            {
                return lista[0].GetPhysicalAddress().ToString();
            }
            return "NO MAC";
        }


        private static string unidades() {
            string unida;
            unida = "";
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
               
                if (d.IsReady == true)
                {
                    unida+="Unidad "+ d.Name +" Espacio Libre "+
                        d.AvailableFreeSpace +" Tamaño total "+ d.TotalSize +",";
                }

            }


            return unida;
        }


        //
       
        
        
        static void Main(string[] args)
        {

            //
            
           Console.WriteLine("Introduzca la I.P. del servidor");
           IPAddress ip=IPAddress.Parse(Console.ReadLine());
           Console.WriteLine("Introduzca el Puerto");
           int port = int.Parse(Console.ReadLine());

           string resultado= Cliente(ip, port);
           

        }





    }
}
