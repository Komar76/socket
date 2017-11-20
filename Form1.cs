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
    public partial class Form1 : Form
    {
        Sendrec Sendrec = new Sendrec();

        public Form1()
        {
            InitializeComponent();
        }
        public string getip()
        {
            return textBox2.Text;
        }
        public string getmessage()
        {
            return textBox1.Text;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sendrec.SendMessageFromSocket(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (sendes != null)
            //    sendes.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Sendrec.connect(this);
            var receivServ = new Thread(Sendrec.RecServ);
			receivServ.Start(this);
        }

	    public void AppendText(string text)
	    {
			if (InvokeRequired)
			{
				Invoke(new Action<string>(AppendText), new object[] { text });
				return;
			}
			richTextBox1.Text += text + "\r\n";
		}
    }
}
