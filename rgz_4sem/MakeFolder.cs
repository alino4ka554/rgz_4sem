﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rgz_4sem
{
    public partial class MakeFolder : Form
    {
        //Класс для формы создания файла/каталога
        public string Name { get; set; } //имя

        public MakeFolder()
        {
            InitializeComponent();
            Name = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                this.Name = textBox1.Text;
                this.Close();
            }
            else MessageBox.Show("Введите название!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
