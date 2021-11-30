using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace System_Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // FileManager fM = new FileManager();
            FM F = new FM();
            F.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prManager Pr = new prManager();
            Pr.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //systemInfo sysInfo = new systemInfo();
            //sysInfo.Show();
            //this.Hide();

            Process.Start("msinfo32");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ioMngnt ioMn = new ioMngnt();
            ioMn.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            socket sk = new socket();
            sk.Show();
            this.Hide();
        }
    }
}
