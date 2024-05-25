﻿using System;
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
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace rgz_4sem
{
    public partial class Form1 : Form
    {
        public FileManager fileManager;

        public FileManager fileManagerForMove;

        public Color topic;
        public Form1()
        {
            InitializeComponent();

            this.fileManager = new FileManager();
            this.fileManagerForMove = new FileManager();
            InitializeListView();

            KeyDown += Keyboard;
        }

        private void InitializeListView()
        {
            RefreshList1View();
            RefreshList2View();

            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView2.MouseDoubleClick += ListView2_MouseDoubleClick;
            listView1.MouseClick += ListView1_MouseClick;

        }

        private void ListView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string itemToDrag = listView1.SelectedItems[0].Text;
                listView1.DoDragDrop(itemToDrag, DragDropEffects.Copy);
            }
        }

        private void ListView2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ListView2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.Windows.Forms.ListViewItem)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ListView2_DragDrop(object sender, DragEventArgs e)
        {
            string itemToDrag = (string)e.Data.GetData(DataFormats.Text);
            if(itemToDrag != null && itemToDrag != " . .")
            {
                string name = Path.Combine(fileManager.way.currentPath, itemToDrag);
                string place = Path.Combine(fileManagerForMove.way.currentPath, itemToDrag);

                fileManager.Move(name, place);

                DirectoryInfo info = Directory.GetParent(place);
                fileManagerForMove.directories = info.GetDirectories();
                fileManagerForMove.files = info.GetFiles();

                RefreshList1View();
                RefreshList2View();
            }
        }


        public void List1Item_Changed(object sender, EventArgs e)
        {
            label5.Text = Path.Combine(this.fileManager.way.currentPath, listView1.FocusedItem.Text);
            label6.Text = Directory.GetLastWriteTime(label5.Text).ToString();
            label7.Text = Directory.GetCreationTime(label5.Text).ToString();

        }

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

                if (fileManager.way.currentPath == fileManagerForMove.way.currentPath)
                {
                    DirectoryInfo info = Directory.GetParent(Path.Combine(fileManagerForMove.way.currentPath, listView1.SelectedItems[0].Text));
                    fileManagerForMove.directories = info.GetDirectories();
                    fileManagerForMove.files = info.GetFiles();
                    RefreshList2View();
                }

                RefreshList1View();
            }
            
        }

        private void zipItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    this.fileManager.ZipFolder(listView1.SelectedItems[0].Text);

                    if (fileManager.way.currentPath == fileManagerForMove.way.currentPath)
                    {
                        DirectoryInfo info = Directory.GetParent(Path.Combine(fileManagerForMove.way.currentPath, listView1.SelectedItems[0].Text));
                        fileManagerForMove.directories = info.GetDirectories();
                        fileManagerForMove.files = info.GetFiles();
                        RefreshList2View();
                    }
                }
            }
            catch
            {
                throw new Exception("Недостаточно прав для создания сжатой ZIP-папки.");
            }
            RefreshList1View();
        }

        private void reName_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string name = Path.Combine(fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                RenameForm renameForm = new RenameForm();
                renameForm.ShowDialog();
                fileManager.Rename(name, renameForm.Name);

                if (fileManager.way.currentPath == fileManagerForMove.way.currentPath)
                {
                    DirectoryInfo info = Directory.GetParent(name);
                    fileManagerForMove.directories = info.GetDirectories();
                    fileManagerForMove.files = info.GetFiles();
                    RefreshList2View();
                }

                RefreshList1View();
            }
        }

        private void Keyboard(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (listView1.SelectedItems.Count > 0 || listView2.SelectedItems.Count > 0))
            {
                HandleEnterKey();
            }
        }

        private void HandleEnterKey()
        {
            if (listView1.Focused == true)
            {
                NavigationForFirstList();
            }
            if(listView2.Focused == true)
            {
                NavigationForSecondList();
            }
        }

        private void NavigateToPreviousFolder(int number)
        {
            switch(number)
            {
                case 1:
                    {
                        this.fileManager.PreviousFolder();
                        RefreshList1View();
                        break;
                    }
               case 2:
                    {
                        this.fileManagerForMove.PreviousFolder();
                        RefreshList2View();
                        break;
                    }
            }
            
        }

        private void NavigateToNextFolder(int number, string folderName)
        {
            switch (number)
            {
                case 1:
                    {
                        this.fileManager.NextFolder(folderName);
                        RefreshList1View();
                        break;
                    }
                case 2:
                    {
                        this.fileManagerForMove.NextFolder(folderName);
                        RefreshList2View();
                        break;
                    }
            }
        }

        private void RefreshList1View()
        {
            listView1.Items.Clear();

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
            }

            foreach (FileInfo file in fileManager.files)
            {
                ListViewItem fileElement = new ListViewItem();
                fileElement.Text = file.Name;
                listView1.Items.Add(fileElement);
            }
        }

        private void RefreshList2View()
        {
            listView2.Items.Clear();

            ListViewItem backButton = new ListViewItem();
            backButton.Text = " . .";
            listView2.Items.Add(backButton);

            foreach (DirectoryInfo dirs in fileManagerForMove.directories)
            {
                ListViewItem dirElement = new ListViewItem();
                dirElement.Text = dirs.Name;
                if (!fileManagerForMove.Access(dirs))
                {
                    dirElement.ForeColor = SystemColors.ActiveBorder;
                }
                listView2.Items.Add(dirElement);
            }

            foreach (FileInfo file in fileManagerForMove.files)
            {
                ListViewItem fileElement = new ListViewItem();
                fileElement.Text = file.Name;
                listView2.Items.Add(fileElement);
            }
        }

        public void NavigationForFirstList()
        {
            string str = Path.Combine(this.fileManager.way.currentPath, listView1.SelectedItems[0].Text);
            DirectoryInfo dir = new DirectoryInfo(str);

            if (dir.Exists)
            {
                if (listView1.Items[0].Focused)
                {
                    NavigateToPreviousFolder(1);
                }
                else
                {
                    NavigateToNextFolder(1, listView1.SelectedItems[0].Text);
                }
            }
            else
            {
                Process.Start(str);
            }
        }

        public void NavigationForSecondList()
        {
            string str = Path.Combine(this.fileManagerForMove.way.currentPath, listView2.SelectedItems[0].Text);
            DirectoryInfo dir = new DirectoryInfo(str);

            if (dir.Exists)
            {
                if (listView2.Items[0].Focused)
                {
                    NavigateToPreviousFolder(2);
                }
                else
                {
                    NavigateToNextFolder(2,listView2.SelectedItems[0].Text);
                }
            }
            else
            {
                Process.Start(str);
            }
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                NavigationForFirstList();
            }
        }

        private void ListView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                NavigationForSecondList();
            }
        }

        private void View_Click(object sender, EventArgs e)
        {
            if (sender == viewToolStripMenuItem && listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0] != listView1.Items[0])
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
                if (makeFolder.Name != "")
                {
                    fileManager.MakeFolder(name);
                    NavigateToNextFolder(1, name);

                    if (fileManager.way.previousPath == fileManagerForMove.way.currentPath)
                    {
                        DirectoryInfo info = Directory.GetParent(name);
                        fileManagerForMove.directories = info.GetDirectories();
                        fileManagerForMove.files = info.GetFiles();
                        RefreshList2View();
                    }
                }
            }
        }
        

        private void makeFile_Click(object sender, EventArgs e)
        {
            if (sender == makeFileToolStripMenuItem)
            {
                MakeFolder makeFolder = new MakeFolder();
                makeFolder.ShowDialog();
                string name = Path.Combine(fileManager.way.currentPath, makeFolder.Name);
                if (makeFolder.Name != "")
                {
                    fileManager.MakeFile(name);

                    if (fileManager.way.currentPath == fileManagerForMove.way.currentPath)
                    {
                        DirectoryInfo info = Directory.GetParent(name);
                        fileManagerForMove.directories = info.GetDirectories();
                        fileManagerForMove.files = info.GetFiles();
                        RefreshList2View();
                    }

                    RefreshList1View();
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (sender == deleteToolStripMenuItem && listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0] != listView1.Items[0])
            {
                this.fileManager.Delete(listView1.SelectedItems[0].Text);

                if (fileManager.way.currentPath == fileManagerForMove.way.currentPath)
                {
                    DirectoryInfo info = Directory.GetParent(Path.Combine(fileManagerForMove.way.currentPath, listView1.SelectedItems[0].Text));
                    fileManagerForMove.directories = info.GetDirectories();
                    fileManagerForMove.files = info.GetFiles();
                    RefreshList2View();
                }

                RefreshList1View();

            }
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            if (sender == renameToolStripMenuItem && listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0] != listView1.Items[0])
            {
                string name = Path.Combine(fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                RenameForm renameForm = new RenameForm();
                renameForm.ShowDialog();
                if (renameForm.Name != "")
                {
                    fileManager.Rename(name, renameForm.Name);

                    if (fileManager.way.currentPath == fileManagerForMove.way.currentPath)
                    {
                        DirectoryInfo info = Directory.GetParent(name);
                        fileManagerForMove.directories = info.GetDirectories();
                        fileManagerForMove.files = info.GetFiles();
                        RefreshList2View();
                    }

                    RefreshList1View();
                }
            }
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            if (sender == copyToolStripMenuItem && listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0] != listView1.Items[0])
                {
                    string name = Path.Combine(fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                    string place = Path.Combine(fileManagerForMove.way.currentPath, "(Копия)" + listView1.SelectedItems[0].Text);
                    CopyForm copyForm = new CopyForm(name, place);
                    copyForm.ShowDialog();
                    if (copyForm.Place != "")
                    {
                        fileManager.Copy(name, copyForm.Place);

                        DirectoryInfo info = Directory.GetParent(place);
                        fileManagerForMove.directories = info.GetDirectories();
                        fileManagerForMove.files = info.GetFiles();

                        RefreshList1View();
                        RefreshList2View();
                    }
                }
            }
        }

        public void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.color = this.topic;
            settings.ShowDialog();
            if(settings.color == Color.Pink)
            {
                this.topic = Color.Pink;

                this.BackColor = System.Drawing.Color.HotPink;
                this.listView1.BackColor = System.Drawing.Color.DeepPink;
                this.listView2.BackColor = System.Drawing.Color.DeepPink;
                this.groupBox1.BackColor = System.Drawing.Color.HotPink;
                this.groupBox1.BackColor = System.Drawing.Color.HotPink;
                this.menuStrip1.BackColor = System.Drawing.Color.HotPink;
                this.panel1.BackColor = System.Drawing.Color.DeepPink;
                this.panel2.BackColor = System.Drawing.Color.DeepPink;
            }
            else if (settings.color == Color.Black)
            {
                this.topic = Color.Black;
                
                this.BackColor = System.Drawing.Color.Black;
                this.listView1.BackColor = System.Drawing.Color.Black;
                this.listView2.BackColor = System.Drawing.Color.Black;
                this.groupBox1.BackColor = System.Drawing.Color.Black;
                this.groupBox1.BackColor = System.Drawing.Color.Black;
                this.menuStrip1.BackColor = System.Drawing.Color.Black;
                this.panel1.BackColor = System.Drawing.Color.Black;
                this.panel2.BackColor = System.Drawing.Color.Black;
            }
        }

        private void Help_CLick(object sender, EventArgs e)
        {
            Process.Start(helpProvider1.HelpNamespace);
        }

        private void Exit_CLick(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    }
