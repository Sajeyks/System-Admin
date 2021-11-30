using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.IO.Ports;


namespace System_Admin
{
    public partial class ioMngnt : Form
    {
        public ioMngnt()
        {
            InitializeComponent();
        }

        RegistryKey Regkey, RegKey2;
        Int32 rValue, rsvalue, Gvalue, tvalue;
        string Regpath = "System\\CurrentControlSet\\Services\\USBSTOR";
        string ReadAndWriteRegPath2 = "System\\CurrentControlSet\\Control";
        string ReadAndWriteRegPath = "System\\CurrentControlSet\\Control\\StorageDevicePolicies";


        private void ioMngnt_Load(object sender, EventArgs e)
        {
            isAdmin = IsUserAnAdmin();
            if (isAdmin == false)
            {
                MessageBox.Show("You don't have proper privileges level to make changes, administrators privileges are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Close();
            }
            else
            {
                Regkey = Registry.LocalMachine.OpenSubKey(Regpath, true);
                Gvalue = Convert.ToInt32(Regkey.GetValue("Start"));
                //check the current state of the usb/whether is enabled or disabled
                if (Gvalue == 3)
                {
                    radioButton1.Checked = true;
                }
                else if (Gvalue == 4)
                {
                    radioButton2.Checked = true;
                }
                RegKey2 = Registry.LocalMachine.OpenSubKey(ReadAndWriteRegPath, true);
                try
                {
                    tvalue = Convert.ToInt32(RegKey2.GetValue("WriteProtect"));
                    if (tvalue == 1)
                    {
                        radioButton3.Checked = true;
                    }
                    else if (tvalue == 0)
                    {
                        radioButton4.Checked = true;
                    }
                }
                catch (NullReferenceException) { }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            rValue = 3;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            rValue = 4;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            rsvalue = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Regkey = Registry.LocalMachine.OpenSubKey(Regpath, true);
                Regkey.SetValue("Start", rValue);
                if (groupBox1.Enabled == true)
                {
                    RegKey2 = Registry.LocalMachine.OpenSubKey(ReadAndWriteRegPath2, true);
                    RegKey2.CreateSubKey("StorageDevicePolicies");
                    RegKey2 = Registry.LocalMachine.OpenSubKey(ReadAndWriteRegPath, true);
                    RegKey2.SetValue("WriteProtect", rsvalue);
                }
            }
            catch (Exception ex)
            { }
            if ((rValue == 3) && (rsvalue == 1))
            {
                MessageBox.Show("USB Port were enable and Read only is enabled");
            }
            else if ((rValue == 3) && (rsvalue == 0))
            {
                MessageBox.Show("USB Port were enable and Read and write is enabled");
            }
            else
            {
                MessageBox.Show("USB Port were disable");
            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            rsvalue = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Close();
            form1.Show();
        }


        //
        private void button2_Click(object sender, EventArgs e)
        {

        }
        bool isAdmin;
        [DllImport("shell32")]
        static extern bool IsUserAnAdmin();

    }
}
