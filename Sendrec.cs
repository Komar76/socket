using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace client
{
    class Sendrec
    {
        IPEndPoint ipEndPoint;
        Socket sendes;

        public void connect(object obj)
        {
            Form1 form1 = (Form1)obj;
            string ip = form1.getip();
            IPAddress ipAddr = IPAddress.Parse(form1.getip());
            ipEndPoint = new IPEndPoint(ipAddr, 20000);
            sendes = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sendes.Connect(ipEndPoint);
        }
        public void RecServ(object obj)
        {
            Form1 form1 = (Form1)obj;
            var buffer = new byte[1024];
            while (true)
            {
                try
                {
                    var byterec = sendes.Receive(buffer);
                    var data = Encoding.UTF8.GetString(buffer, 0, byterec);
                    if (data != null)
                    {
                        form1.AppendText(data);
                    }
                }
                catch (Exception ignored)
                {
                    break;
                }
            }
        }
        public void SendMessageFromSocket(Object obj)
        {
            Form1 form1 = (Form1)obj;
            byte[] msg;
            try
            {
                string message = form1.getmessage();
                msg = Encoding.UTF8.GetBytes(message);
                sendes.Send(msg);

                //  sendes.Shutdown(SocketShutdown.Both);
                //   sendes.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
