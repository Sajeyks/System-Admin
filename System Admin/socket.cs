using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace System_Admin
{
    public partial class socket : Form
    {
        Socket sck;
        EndPoint epLocal, epRemote;
        byte[] buffer;
        

        public socket()
        {
            InitializeComponent();
        }

        private void socket_Load(object sender, EventArgs e)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            textLocalIp.Text = GetLocalIP();
            textRemoteIP.Text = GetLocalIP();
            connect();
        }

        private string GetLocalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "10.10.152.212";
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            try{
                byte[] ReceivedData = new byte[1500];
                ReceivedData = (byte[])aResult.AsyncState;
                ASCIIEncoding aEncoding = new ASCIIEncoding();
                string ReceivedMessage = aEncoding.GetString(ReceivedData);

                listBox1.Items.Add(">> :" + ReceivedMessage);
                buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new
                    AsyncCallback(MessageCallBack), buffer);
            }catch(Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aEncoding.GetBytes(receivertxt.Text);
            sck.Send(sendingMessage);
            listBox2.Items.Add(">> :" + receivertxt.Text);
            listBox1.Items.Add("<< :" + receivertxt.Text);
            receivertxt.Text = " ";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            ASCIIEncoding aEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = aEncoding.GetBytes(sendertxt.Text);
            sck.Send(sendingMessage);
            listBox1.Items.Add("<< :" + sendertxt.Text);
            listBox2.Items.Add(">> :" + sendertxt.Text);
            sendertxt.Text = " ";
        }

        public void connect()
        {
            epLocal = new IPEndPoint(IPAddress.Parse(textLocalIp.Text),
                Convert.ToInt32(textLocalPort.Text));
            sck.Bind(epLocal);

            epRemote = new IPEndPoint(IPAddress.Parse(textRemoteIP.Text),
                Convert.ToInt32(textRemotePort.Text));
            sck.Connect(epRemote);
            buffer = new byte[1500];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new
                AsyncCallback(MessageCallBack), buffer);
            if (textRemotePort == textLocalPort)
            {
                MessageBox.Show("connection failure");
            }
            else
            {
                MessageBox.Show("Connection Established");
            }
        }
        

       
    }
}
