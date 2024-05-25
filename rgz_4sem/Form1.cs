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
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace rgz_4sem
{
    //Класс для приложения файлового менеджера
    public partial class Form1 : Form
    {
        public FileManager fileManager; //экземпляр файлового списка

        public FileManager fileManagerForMove; //экземпляр файлового списка для перемещения и копирования

        public Color topic; //тема приложения
        public Form1()
        {
            InitializeComponent();

            this.fileManager = new FileManager();
            this.fileManagerForMove = new FileManager();
            InitializeListView();

            KeyDown += Keyboard;
        }

        private void InitializeListView() //заполнение списков
        {
            RefreshList1View();
            RefreshList2View();

            listView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            listView2.MouseDoubleClick += ListView2_MouseDoubleClick;
            listView1.MouseClick += ListView1_MouseClick;

        }

        //перемещение элемента из первого списка во второй
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

        public void List1Item_Changed(object sender, EventArgs e) //обновление пути, времени изменения и создания из первого списка
        {
            label5.Text = Path.Combine(this.fileManager.way.currentPath, listView1.FocusedItem.Text);
            label6.Text = Directory.GetLastWriteTime(label5.Text).ToString();
            label7.Text = Directory.GetCreationTime(label5.Text).ToString();

        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e) //открытие контекстного меню
        {
            if (e.Button == MouseButtons.Right && listView1.FocusedItem != null && listView1.FocusedItem.Bounds.Contains(e.Location))
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void Keyboard(object sender, KeyEventArgs e) //обработчик нажатия клавиш
        {
            if (e.KeyCode == Keys.Enter && (listView1.SelectedItems.Count > 0 || listView2.SelectedItems.Count > 0))
            {
                HandleEnterKey();
            }
        }

        private void HandleEnterKey() //перемещение по клавиатуре
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

        private void NavigateToPreviousFolder(int number) //перемещение назад
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

        private void NavigateToNextFolder(int number, string folderName) //перемещение вперед
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

        private void RefreshList1View() //обновление первого списка
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

        private void RefreshList2View() //обновление второго списка
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

        public void NavigationForFirstList() //навигация для первого списка
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

        public void NavigationForSecondList() //навигация для второго списка
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

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e) //перемещение с помощью мыши для первого списка
        {
            if (listView1.SelectedItems.Count > 0)
            {
                NavigationForFirstList();
            }
        }

        private void ListView2_MouseDoubleClick(object sender, MouseEventArgs e) //перемещение с помощью мыши для второго списка
        {
            if (listView2.SelectedItems.Count > 0)
            {
                NavigationForSecondList();
            }
        }


        private void OpenItem_Click(object sender, EventArgs e) //открыть файл/каталог из контекстного меню
        {
            try
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    string name = Path.Combine(this.fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                    Process.Start(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteItem_Click(object sender, EventArgs e) //удалить файл/каталог из контекстного меню
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void zipItem_Click(object sender, EventArgs e) //добавить в сжатую зип-папку
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshList1View();
        }

        private void reName_Click(object sender, EventArgs e) //переименовать из контекстного меню
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void View_Click(object sender, EventArgs e) //кнопка просмотр из меню
        {
            try
            {
                if (sender == viewToolStripMenuItem && listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0] != listView1.Items[0])
                {
                    string str = Path.Combine(this.fileManager.way.currentPath, listView1.SelectedItems[0].Text);
                    Process.Start(str);
                }
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void makeFolder_Click(object sender, EventArgs e) //создание папки
        {
            try
            {
                if (sender == makeFolderToolStripMenuItem)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void makeFile_Click(object sender, EventArgs e) //создание файла
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, EventArgs e) //удаление по кнопке из меню
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Rename_Click(object sender, EventArgs e) //переименование файла/каталога
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Copy_Click(object sender, EventArgs e) //копирование файла/каталога
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Settings_Click(object sender, EventArgs e) //настройки для выбора темы приложения
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

        private void Help_CLick(object sender, EventArgs e) //открытие справки
        {
            Process.Start(helpProvider1.HelpNamespace);
        }

        private void Exit_CLick(object sender, EventArgs e) //выход 
        {
            this.Close();
        }
    }

    }
