namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            pictureBox2 = new PictureBox();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            menuStrip2 = new MenuStrip();
            enterLicenseKeyToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            listBox1 = new ListBox();
            label2 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            menuStrip2.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(6, 24);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 769);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.AllowDrop = true;
            button1.AutoEllipsis = true;
            button1.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(1218, 28);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(280, 49);
            button1.TabIndex = 1;
            button1.Text = "Load File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.AllowDrop = true;
            button2.AutoEllipsis = true;
            button2.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(1218, 85);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(280, 54);
            button2.TabIndex = 1;
            button2.Text = "Camera Scan";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(613, 24);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(600, 769);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(1218, 221);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(280, 190);
            richTextBox1.TabIndex = 8;
            richTextBox1.Tag = "";
            richTextBox1.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1218, 194);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 9;
            label1.Text = "Results";
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 24);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1507, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            menuStrip2.Items.AddRange(new ToolStripItem[] { enterLicenseKeyToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(1507, 24);
            menuStrip2.TabIndex = 11;
            menuStrip2.Text = "menuStrip2";
            // 
            // enterLicenseKeyToolStripMenuItem
            // 
            enterLicenseKeyToolStripMenuItem.Name = "enterLicenseKeyToolStripMenuItem";
            enterLicenseKeyToolStripMenuItem.Size = new Size(110, 20);
            enterLicenseKeyToolStripMenuItem.Text = "Enter License Key";
            enterLicenseKeyToolStripMenuItem.Click += enterLicenseKeyToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 797);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1507, 22);
            statusStrip1.TabIndex = 12;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // listBox1
            // 
            listBox1.AllowDrop = true;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(1219, 546);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(279, 244);
            listBox1.TabIndex = 14;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            listBox1.DragDrop += listBox1_DragDrop;
            listBox1.DragEnter += listBox1_DragEnter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1218, 525);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 15;
            label2.Text = "Image Files";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(1233, 148);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(51, 19);
            checkBox1.TabIndex = 17;
            checkBox1.Text = "DDN";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Location = new Point(1316, 148);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(51, 19);
            checkBox2.TabIndex = 18;
            checkBox2.Text = "DBR ";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new Point(1404, 148);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(51, 19);
            checkBox3.TabIndex = 19;
            checkBox3.Text = "MRZ";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1500, 850);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(listBox1);
            Controls.Add(statusStrip1);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(pictureBox2);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Controls.Add(menuStrip2);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            this.MaximizeBox = false;
            Name = "Form1";
            Text = ".NET Dynamsoft Capture Vision Demo";
            WindowState = FormWindowState.Normal;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox2;
        private RichTextBox richTextBox1;
        private Label label1;
        private MenuStrip menuStrip1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem enterLicenseKeyToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ListBox listBox1;
        private Label label2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
    }
}

