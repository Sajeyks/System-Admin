using System;
using System.Windows.Forms;

using System.IO;

namespace System_Admin
{
    public partial class FM : Form
    {
        public FM()
        {
            InitializeComponent();
            
        }

        private void PopulateTreeView()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(comboBox1.SelectedValue.ToString());  
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }

        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            try {
                foreach (DirectoryInfo subDir in subDirs)
                {
                    aNode = new TreeNode(subDir.Name, 0, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode);
                    }
                    nodeToAddTo.Nodes.Add(aNode);
                }
            }catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                
            }
        }

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(item, "Directory"),
                    new ListViewItem.ListViewSubItem(item, dir.LastAccessTime.ToShortDateString())
                };
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(item, "File"),
                    new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                };

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(textBox2.Text + "\\" + textBox1.Text))
                {
                    Directory.CreateDirectory(textBox2.Text + "\\" + textBox1.Text);
                    MessageBox.Show("Directory Created Succesfully!");
                    textBox2.Text = "";
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Directory Already Exists!", "Error creating directory");
                }
            }
            catch
            {
                MessageBox.Show("Error creating directory. Cannot access location");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Directory.Exists(textBox2.Text + "\\" + textBox1.Text))
                {
                    Directory.Delete(textBox2.Text + "\\" + textBox1.Text);
                    MessageBox.Show("Directory Succesfully Deleted!");
                    textBox2.Text = "";
                    textBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Directory Doesn't Exists!");
                }
            }
            catch
            {
                MessageBox.Show("Error deleting directory. Cannot access location");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FM_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = DriveInfo.GetDrives();
            //.Where(d => d.DriveType == DriveType.) ;
            //comboBox1.DisplayMember = "Name";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            PopulateTreeView();
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fd1 = new FolderBrowserDialog();
                fd1.ShowDialog();
                if(fd1.ShowDialog()== DialogResult.OK)
                {
                    textBox3.Text = fd1.SelectedPath;
                }
            }
            catch
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try { 
            if (!File.Exists(textBox3.Text +"\\"+ textBox4.Text))
            {
                File.Create(textBox3.Text + "\\" + textBox4.Text);
                MessageBox.Show("File Created Succesfully!");
                textBox3.Text = "";
                textBox4.Text = "";
                }
            else
            {
                MessageBox.Show("File Already Exists!", "Error creating file");
            }
            }
            catch
            {
                MessageBox.Show("Error creating file. Cannot access location");
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fd2 = new FolderBrowserDialog();
                fd2.ShowDialog();
                if (fd2.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = fd2.SelectedPath;
                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (File.Exists(textBox3.Text + "\\" + textBox4.Text))
                {
                    File.Delete(textBox3.Text + "\\" + textBox4.Text);
                    MessageBox.Show("File Deleted Succesfully");
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else
                {
                    MessageBox.Show("File Doesn't Exists!");
                }
            }
            catch
            {
                MessageBox.Show("Error deleting file. Cannot access location");
            }
        }
    }
}
