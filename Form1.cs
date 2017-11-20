using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        serv Serv_sock = new serv();
        public Form1()
        {
           InitializeComponent();
           label1.Text = serv.ip;
        }
        public void starserv_Click(object sender, EventArgs e)
        {
            Thread Thread_sock = new Thread(Serv_sock.Start);
            Thread_sock.Start();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Serv_sock.Close_form();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
