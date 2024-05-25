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
    public enum Color
    {
        Pink, Black
    }
    public partial class Settings : Form
    {
        public Color color { get; set; }
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                if (radioButton1.Checked) color = Color.Pink;
                else if (radioButton2.Checked) color = Color.Black;
                this.Close();
            }
            else MessageBox.Show("Выберите тему приложения!");
        }
    }
}
