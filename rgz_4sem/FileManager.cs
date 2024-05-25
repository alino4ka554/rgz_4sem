using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace rgz_4sem
{
    public class FileManager
    {
        public DriveInfo[] drives;
        public DirectoryInfo[] directories;
        public FileInfo[] files;
        public Way way;

        public FileManager()
        {
            this.way = new Way();
            this.drives = DriveInfo.GetDrives();
            DirectoryInfo dir = new DirectoryInfo(drives[0].Name);
            this.way.previousPath = Path.Combine(drives[0].Name);
            this.way.currentPath = Path.Combine(drives[0].Name, dir.Name);
            this.directories = dir.GetDirectories();
            this.files = dir.GetFiles();
        }


        public void NextFolder(string selectedItem)
        {
            this.way.previousPath = this.way.currentPath;
            this.way.currentPath = Path.Combine(this.way.previousPath, selectedItem);

            DirectoryInfo dir = new DirectoryInfo(way.currentPath);
            if (Access(dir) == true)
            {
                this.directories = dir.GetDirectories();
                this.files = dir.GetFiles();
            }
            else
            {
                this.way.currentPath = this.way.previousPath;
                this.way.previousPath = Directory.GetParent(this.way.currentPath)?.FullName;
            }
        }

        public void PreviousFolder()
        {
            if (this.way.currentPath != Path.Combine(drives[0].Name))
            {
                this.way.currentPath = this.way.previousPath;
                this.way.previousPath = Directory.GetParent(this.way.currentPath)?.FullName;

                DirectoryInfo dir = new DirectoryInfo(way.currentPath);
                this.directories = dir.GetDirectories();
                this.files = dir.GetFiles();
            }
        }

        public void ZipFolder(string path)
        {
            string name = Path.Combine(way.currentPath, path);
            string zipFile = Path.Combine(way.currentPath, Path.GetFileNameWithoutExtension(name) + ".zip");

            if (File.Exists(name))
            {
                using (FileStream zipToOpen = new FileStream(zipFile, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                    {
                        archive.CreateEntryFromFile(name, Path.GetFileName(name));
                    }
                }
                DirectoryInfo info = Directory.GetParent(name);
                this.directories = info.GetDirectories();
                this.files = info.GetFiles();
            }
            else if (Directory.Exists(name))
            {
                ZipFile.CreateFromDirectory(name, zipFile);
                DirectoryInfo info = Directory.GetParent(name);
                this.directories = info.GetDirectories();
                this.files = info.GetFiles();
            }
        }

        public void Delete(string path)
        {
            string name = Path.Combine(way.currentPath, path);
            try
            {
                if (File.Exists(name))
                {
                    File.Delete(name);
                    DirectoryInfo info = Directory.GetParent(name);
                    this.files = info.GetFiles();
                }
                else if (Directory.Exists(name))
                {
                    Directory.Delete(name, true);
                    DirectoryInfo info = Directory.GetParent(name);
                    this.directories = info.GetDirectories();
                }
            }
            catch
            {
                throw new Exception("Не удалось удалить");
            }
        }

        public void Rename(string name, string newName)
        {
            if (Directory.Exists(name))
            {
                string reName = Path.Combine(way.currentPath, newName);
                if (!Directory.Exists(reName))
                {
                    Directory.Move(name, reName);
                    DirectoryInfo info = Directory.GetParent(reName);
                    this.directories = info.GetDirectories();
                }
                else throw new Exception("Каталог с таким именем уже существует!");
            }
            else if (File.Exists(name))
            {
                string reName = Path.Combine(way.currentPath, newName + Path.GetExtension(name));
                if (!File.Exists(reName))
                {
                    File.Move(name, reName);
                    DirectoryInfo info = Directory.GetParent(reName);
                    this.files = info.GetFiles();
                }
                else throw new Exception("Файл с таким именем уже существует!");
            }
        }

        public void MakeFolder(string name)
        {
            if (!Directory.Exists(name))
            {
                Directory.CreateDirectory(name);
                directories.Append(Directory.CreateDirectory(name));
            }
            else throw new Exception("Каталог с таким именем уже существует!");
        }

        public void MakeFile(string name)
        {
            if (Path.GetExtension(name) == "") throw new Exception("Введите расширение файла!");
            else if (!File.Exists(name))
            {
                File.Create(name).Close();
                DirectoryInfo info = Directory.GetParent(name);
                this.files = info.GetFiles();
            }
            else throw new Exception("Файл с таким именем уже существует!");

        }

        public void Move(string name, string place)
        {
            if(Directory.Exists(name))
            {
                Directory.Move(name, place);
                DirectoryInfo info = Directory.GetParent(name);
                this.directories = info.GetDirectories();
            }
            else if (File.Exists(name))
            {
                File.Move(name, place);
                DirectoryInfo info = Directory.GetParent(name);
                this.files = info.GetFiles();
            }
        }

        public void Copy(string name, string place)
        {
            if (Directory.Exists(name))
            {
                CopyDir(name, place);
                DirectoryInfo info = Directory.GetParent(name);
                this.directories = info.GetDirectories();
            }
            else if (File.Exists(name))
            {
                File.Copy(name, place);
                DirectoryInfo info = Directory.GetParent(name);
                this.files = info.GetFiles();
            }
            else throw new Exception("Не удалось скопировать!");
        }

        public void CopyDir(string name, string place)
        {
            Directory.CreateDirectory(place);
            foreach (string s1 in Directory.GetFiles(name))
            {
                string s2 = place + "\\" + Path.GetFileName(s1);
                File.Copy(s1, s2);
            }
            foreach (string s in Directory.GetDirectories(name))
            {
                CopyDir(s, place + "\\" + Path.GetFileName(s));
            }
        }

        public bool Access(DirectoryInfo dir)
        {
            bool status = true;
            try
            {
                dir.GetDirectories();
            }
            catch
            {
                status = false;
            }
            return status;
        }


    }
}
