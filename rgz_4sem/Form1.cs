using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO.Compression;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace rgz_4sem
{
    public partial class Form1 : Form
    {
        public FileManager fileManager;
        public Form1()
        {
            InitializeComponent();

            this.fileManager = new FileManager();
            InitializeListView();
            //InitializeMenuStrip();

            KeyDown += Keyboard;
        }

        private void InitializeListView()
        {
            ListViewItem backButton = new ListViewItem();
            backButton.Text = " . .";
            listView1.Items.Add(backButton);

            foreach (DirectoryInfo dirs in fileManager.directories)
            {
                ListViewItem dirElement = new ListViewItem();
                dirElement.Text = dirs.Name;
                if (fileManager.Access(dirs) == false)
                {
                    dirElement.ForeColor = SystemColors.ActiveBorder;
                }
                listView1.Items.Add(dirElement);
            }

            foreach (FileInfo file in fileManager.files)
            {
                ListViewItem fileElement = new ListViewItem();
                fileElement.Text = file.Name;
                listView1.Items.Add(fileElement);
            }

            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView1.MouseClick += ListView1_MouseClick;
        }



        /*private void InitializeMenuStrip()
        {
            viewToolStripMenuItem.Click += MenuStrip1_Click;
        }*/

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && listView1.FocusedItem != null && listView1.FocusedItem.Bounds.Contains(e.Location))
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void OpenItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string name = Path.Combine(this.fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                Process.Start(name);
            }
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                this.fileManager.Delete(listView1.SelectedItems[0].Text);
                
            }
            RefreshListView();
        }

        private void zipItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    this.fileManager.ZipFolder(listView1.SelectedItems[0].Text);
                    
                }
            }
            catch
            {
                throw new Exception("Недостаточно прав для создания сжатой ZIP-папки.");
            }
            RefreshListView();
        }

        private void reName_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string name = Path.Combine(fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                RenameForm renameForm = new RenameForm();
                renameForm.ShowDialog();
                fileManager.Rename(name, renameForm.Name);

                RefreshListView();
            }
        }

        private void Keyboard(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && listView1.SelectedItems.Count > 0)
            {
                HandleEnterKey();
            }
        }

        private void HandleEnterKey()
        {
            string str = Path.Combine(this.fileManager.way.currentPath, listView1.SelectedItems[0].Text);
            DirectoryInfo dir = new DirectoryInfo(str);

            if (dir.Exists)
            {
                if (listView1.Items[0].Focused)
                {
                    NavigateToPreviousFolder();
                }
                else
                {
                    NavigateToNextFolder(listView1.SelectedItems[0].Text);
                }
            }
            else
            {
                Process.Start(str);
            }
        }

        private void NavigateToPreviousFolder()
        {
            this.fileManager.PreviousFolder();
            RefreshListView();
        }

        private void NavigateToNextFolder(string folderName)
        {
            this.fileManager.NextFolder(folderName);
            RefreshListView();
        }

        private void RefreshListView()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();

            ListViewItem backButton = new ListViewItem();
            backButton.Text = " . .";
            listView1.Items.Add(backButton);

            foreach (DirectoryInfo dirs in fileManager.directories)
            {
                ListViewItem dirElement = new ListViewItem();
                dirElement.Text = dirs.Name;
                if (!fileManager.Access(dirs))
                {
                    dirElement.ForeColor = SystemColors.ActiveBorder;
                }
                listView1.Items.Add(dirElement);
                listView2.Items.Add((ListViewItem)dirElement.Clone());
            }

            foreach (FileInfo file in fileManager.files)
            {
                ListViewItem fileElement = new ListViewItem();
                fileElement.Text = file.Name;
                listView1.Items.Add(fileElement);
                listView2.Items.Add((ListViewItem)fileElement.Clone());
            }
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string str = Path.Combine(this.fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                DirectoryInfo dir = new DirectoryInfo(str);

                if (dir.Exists)
                {
                    if (listView1.Items[0].Focused)
                    {
                        NavigateToPreviousFolder();
                    }
                    else
                    {
                        NavigateToNextFolder(listView1.SelectedItems[0].Text);
                    }
                }
                else
                {
                    Process.Start(str);
                }
            }
        }

        private void View_Click(object sender, EventArgs e)
        {
            if (sender == viewToolStripMenuItem && listView1.SelectedItems.Count > 0)
            {
                string str = Path.Combine(this.fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                Process.Start(str);
            }
        }

        private void makeFolder_Click(object sender, EventArgs e)
        {
            if(sender == makeFolderToolStripMenuItem)
            {
                MakeFolder makeFolder = new MakeFolder();
                makeFolder.ShowDialog();
                string name = Path.Combine(fileManager.way.currentPath, makeFolder.Name);
                fileManager.MakeFolder(name);
                NavigateToNextFolder(name);
            }
        }
        

        private void makeFile_Click(object sender, EventArgs e)
        {
            if (sender == makeFileToolStripMenuItem)
            {
                MakeFolder makeFolder = new MakeFolder();
                makeFolder.ShowDialog();
                string name = Path.Combine(fileManager.way.currentPath, makeFolder.Name);
                fileManager.MakeFile(name);
                
                RefreshListView();
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (sender == deleteToolStripMenuItem && listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    this.fileManager.Delete(listView1.SelectedItems[0].Text);

                }
                RefreshListView();
            }
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            if (sender == renameToolStripMenuItem && listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string name = Path.Combine(fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                    RenameForm renameForm = new RenameForm();
                    renameForm.ShowDialog();
                    fileManager.Rename(name, renameForm.Name);
                    
                    RefreshListView();
                }
            }
        }

    }

    }
