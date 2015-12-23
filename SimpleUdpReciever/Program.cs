using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace UpgSyslogServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int localPort = 514;
            IPEndPoint remoteSender = new IPEndPoint(IPAddress.Any, 0);
            bool flag = false;
         
            if (flag)
                return;
            Console.WriteLine("*** upg syslog server ***\n");
            Console.WriteLine("*** Syslog entries are collected in syslog-dd-MM-yyy.txt ***\n");
            Console.WriteLine("*** You are running version 1.0  ***\n");
            Console.WriteLine("*** Visit http://www.upg.gr to check for newer versions  ***\n");
            Console.WriteLine("*** email me at info@upg.gr for questions  ***\n");
            Console.WriteLine("*** Ioannis Kokkinis 2013  ***\n");
            Console.WriteLine("*** Currently collecting syslog-" + DateTime.Now.ToString("dd-MM-yyy") + ".txt ***\n");
            Console.WriteLine("*** If you stop this console, collection stops. ***\n");
            Console.WriteLine("*** If you restart collection resumes ***\n");
            Console.WriteLine("*** Maybe it will be a good idea to compress this folder.. ***\n");
            UdpClient client = new UdpClient(localPort);
            UdpState state = new UdpState(client, remoteSender);
            client.BeginReceive(new AsyncCallback(DataReceived), state);
            Console.ReadKey();
            client.Close();
        }


        private static void DataReceived(IAsyncResult ar)
        {
            UdpClient c = (UdpClient)((UdpState)ar.AsyncState).c;
            IPEndPoint wantedIpEndPoint = (IPEndPoint)((UdpState)(ar.AsyncState)).e;
            IPEndPoint receivedIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Byte[] receiveBytes = c.EndReceive(ar, ref receivedIpEndPoint);


            bool isRightHost = (wantedIpEndPoint.Address.Equals(receivedIpEndPoint.Address)) || wantedIpEndPoint.Address.Equals(IPAddress.Any);
            bool isRightPort = (wantedIpEndPoint.Port == receivedIpEndPoint.Port) || wantedIpEndPoint.Port == 0;
            if (isRightHost && isRightPort)
            {
                string receivedText = ASCIIEncoding.ASCII.GetString(receiveBytes);
//write to file
                string filename = "syslog-" + DateTime.Now.ToString("dd-MM-yyy") + ".txt";
                string path = @filename;
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(receivedText);
                    }	

             //   Console.Write(receivedText);
             //       Console.Write("collecting logs at :" + filename + " ...);
            }

            c.BeginReceive(new AsyncCallback(DataReceived), ar.AsyncState);

        }


        private static string GetValue(string[] args, ref int i)
        {
            string value = String.Empty;
            if (args.Length >= i + 2)
            {
                i++;
                value = args[i];
            }
            return value;
        }

      
    }
}
