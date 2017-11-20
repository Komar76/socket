using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public class serv
    {
        private List<Socket> clientSockets = new List<Socket>();
        static String host = System.Net.Dns.GetHostName();
        static IPAddress ipAddr = System.Net.Dns.GetHostByName(host).AddressList[0];
        public static string ip = ipAddr.ToString();
        IPEndPoint ipEndpoint = new IPEndPoint(ipAddr, 20000);
        Socket mySocket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        public void Start(object obj) // ставлю  на прослушку
        {
            mySocket.Bind(ipEndpoint);
            mySocket.Listen(10);
            while (true) // Добавляю клиентов в лист
            {
                try
                {
					var socket = mySocket.Accept();
                    clientSockets.Add(socket);
					new Thread(ReceiveMessage).Start(socket);
                }
                catch (SocketException e)
                {
                    break;
                }
            }
        }

	    private void ReceiveMessage(object obj) // Получаю сообщения
	    {
		    var socket = (Socket) obj;
			var buffer = new byte[1024];
		    while (true)
		    {
			    try
			    {
				    var byterec = socket.Receive(buffer);
				    var data = Encoding.UTF8.GetString(buffer, 0, byterec);
                    SendToAll(data);
				    if (data.IndexOf("TheEnd") > -1)
				    {
					    mySocket.Close();
					    break;
				    }
			    }
			    catch (Exception ignored)
			    {
				    clientSockets.Remove(socket);
				    break;
				}
			}
		}

		private void SendToAll(string message) // отправляю клиентам из листа
		{
			if (string.IsNullOrEmpty(message) || clientSockets == null)
			{
				return;
			}
			var buffer = Encoding.UTF8.GetBytes(message);
			for (int i = clientSockets.Count - 1; i >= 0; i--)
			{
				try
				{
					clientSockets[i].Send(buffer);
				}
				catch (Exception ignored)
				{
					clientSockets.RemoveAt(i);
				}
			}
		}
        public void Close_form()
        {
            if (mySocket != null)
                mySocket.Close();
        }
    }
}
