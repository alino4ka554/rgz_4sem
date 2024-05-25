using System;
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
    //Класс для подтверждения копирования
    public partial class CopyForm : Form
    {
        public string Name { get; set; } //название копируемого объекта
        public string Place { get; set; } //место для копирования

        public bool Answer { get; set; }
        public CopyForm(string name, string place)
        {
            InitializeComponent();
            Name = name;
            Place = place;
            Answer = false;
            this.label2.Text = Name;
            this.textBox1.Text = Place;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                this.Place = textBox1.Text;
                this.Answer = true;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Place = "";
            this.Close();
        }
    }
}
