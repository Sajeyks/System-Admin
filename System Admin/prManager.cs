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
using System.Dynamic;



namespace System_Admin
{
    public partial class prManager : Form
    {
        public prManager()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            Process process = new Process();
            process.StartInfo.FileName = text;
            process.Start();
            loadProcessList();
            textBox1.Text = "";

        }



        private void button3_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];
            Process process = (Process)item.Tag;
            process.Kill();
            loadProcessList();

        }

        private void prManager_Load(object sender, EventArgs e)
        {
            loadProcessList();
            
        }

        private void loadProcessList()
        {
            listView1.Items.Clear();
            Process[] processesList = Process.GetProcesses();

            foreach (Process process in processesList)
            {
                ListViewItem item = new ListViewItem(process.ProcessName);
                item.Tag = process;
                listView1.Items.Add(item);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("calc");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("notepad");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start("skype");
        }
    }
}
