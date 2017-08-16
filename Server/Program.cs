using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 20001);
            server.Start();
            while (true)
            {
                Socket klient = server.AcceptSocket();

                NetworkStream stream = new NetworkStream(klient);
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                IPEndPoint remoteIpEndPoint = klient.RemoteEndPoint as IPEndPoint;
                IPEndPoint localIpEndPoint = klient.LocalEndPoint as IPEndPoint;

                if (remoteIpEndPoint != null)
                {
                    Console.WriteLine("I am connected to " + remoteIpEndPoint.Address + " on port number " + remoteIpEndPoint.Port);
                }


                writer.WriteLine("Ready");
                while (true)
                {
                    string input = reader.ReadLine();
                    switch (input)
                    {
                        case "Hello Server":
                            writer.WriteLine("Hello Client");
                            break;
                        case "Time?":
                            writer.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
                            break;
                        case "Date?":
                            writer.WriteLine(DateTime.Now.Date.ToString("dd/MM/yyyy"));
                            break;
                        case input.Substring(0, 2):

                            break;
                        default:
                            writer.WriteLine("Unknown input");
                            break;
                    }
                }
                klient.Close();
            }

            
        }
    }
}
