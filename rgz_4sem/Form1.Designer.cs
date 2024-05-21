using System.Windows.Forms;

namespace rgz_4sem
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.header = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.header2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьВСжатуюZIPпапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.HotPink;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.viewToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.makeFolderToolStripMenuItem,
            this.makeFileToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 460);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(855, 31);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 27);
            this.toolStripMenuItem1.Text = "Help";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(61, 27);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.View_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(64, 27);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(87, 27);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.Rename_Click);
            // 
            // makeFolderToolStripMenuItem
            // 
            this.makeFolderToolStripMenuItem.Name = "makeFolderToolStripMenuItem";
            this.makeFolderToolStripMenuItem.Size = new System.Drawing.Size(115, 27);
            this.makeFolderToolStripMenuItem.Text = "MakeFolder";
            this.makeFolderToolStripMenuItem.Click += new System.EventHandler(this.makeFolder_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(73, 27);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.Delete_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(52, 27);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 408);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DeepPink;
            this.panel2.Location = new System.Drawing.Point(0, 376);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(405, 24);
            this.panel2.TabIndex = 6;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.DeepPink;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header,
            this.header2});
            this.listView1.ForeColor = System.Drawing.SystemColors.Window;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 32);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(407, 370);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // header
            // 
            this.header.Width = 150;
            // 
            // header2
            // 
            this.header2.Width = 150;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepPink;
            this.panel1.Location = new System.Drawing.Point(2, 378);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 24);
            this.panel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.listView2);
            this.groupBox2.Location = new System.Drawing.Point(431, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(413, 408);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.Color.DeepPink;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView2.ForeColor = System.Drawing.SystemColors.Window;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 32);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(407, 370);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 150;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.добавитьВСжатуюZIPпапкуToolStripMenuItem,
            this.переименоватьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(284, 100);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(283, 24);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.OpenItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(283, 24);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.DeleteItem_Click);
            // 
            // добавитьВСжатуюZIPпапкуToolStripMenuItem
            // 
            this.добавитьВСжатуюZIPпапкуToolStripMenuItem.Name = "добавитьВСжатуюZIPпапкуToolStripMenuItem";
            this.добавитьВСжатуюZIPпапкуToolStripMenuItem.Size = new System.Drawing.Size(283, 24);
            this.добавитьВСжатуюZIPпапкуToolStripMenuItem.Text = "Добавить в сжатую ZIP-папку";
            this.добавитьВСжатуюZIPпапкуToolStripMenuItem.Click += new System.EventHandler(this.zipItem_Click);
            // 
            // переименоватьToolStripMenuItem
            // 
            this.переименоватьToolStripMenuItem.Name = "переименоватьToolStripMenuItem";
            this.переименоватьToolStripMenuItem.Size = new System.Drawing.Size(283, 24);
            this.переименоватьToolStripMenuItem.Text = "Переименовать";
            this.переименоватьToolStripMenuItem.Click += new System.EventHandler(this.reName_Click);
            // 
            // makeFileToolStripMenuItem
            // 
            this.makeFileToolStripMenuItem.Name = "makeFileToolStripMenuItem";
            this.makeFileToolStripMenuItem.Size = new System.Drawing.Size(93, 27);
            this.makeFileToolStripMenuItem.Text = "MakeFile";
            this.makeFileToolStripMenuItem.Click += new System.EventHandler(this.makeFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.HotPink;
            this.ClientSize = new System.Drawing.Size(855, 491);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem makeFolderToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private GroupBox groupBox1;
        private ListView listView1;
        private ColumnHeader header;
        private ColumnHeader header2;
        private Panel panel1;
        private GroupBox groupBox2;
        private ListView listView2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Panel panel2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem добавитьВСжатуюZIPпапкуToolStripMenuItem;
        private HelpProvider helpProvider1;
        private ToolStripMenuItem переименоватьToolStripMenuItem;
        private ToolStripMenuItem makeFileToolStripMenuItem;
    }
}

