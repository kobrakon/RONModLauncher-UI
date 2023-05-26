namespace RONML_UI
{
    partial class UI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            label2 = new Label();
            label3 = new Label();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaptionText;
            button1.Dock = DockStyle.Bottom;
            button1.Font = new Font("Cascadia Mono SemiBold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(0, 307);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(732, 82);
            button1.TabIndex = 0;
            button1.Text = "Start Modded";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaptionText;
            button2.Dock = DockStyle.Bottom;
            button2.Font = new Font("Cascadia Mono SemiBold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = SystemColors.ButtonFace;
            button2.Location = new Point(0, 225);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(732, 82);
            button2.TabIndex = 1;
            button2.Text = "Start Vanilla";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.BackColor = SystemColors.ActiveCaptionText;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Dock = DockStyle.Bottom;
            checkBox1.Font = new Font("Cascadia Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox1.ForeColor = SystemColors.ButtonFace;
            checkBox1.Location = new Point(0, 200);
            checkBox1.Margin = new Padding(2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(732, 25);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Load VO";
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.BackColor = SystemColors.ActiveCaptionText;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Dock = DockStyle.Bottom;
            checkBox2.Font = new Font("Cascadia Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox2.ForeColor = SystemColors.ButtonFace;
            checkBox2.Location = new Point(0, 175);
            checkBox2.Margin = new Padding(2);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(732, 25);
            checkBox2.TabIndex = 3;
            checkBox2.Text = "Load PAK";
            checkBox2.UseVisualStyleBackColor = false;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.BackColor = SystemColors.ActiveCaptionText;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Dock = DockStyle.Bottom;
            checkBox3.Font = new Font("Cascadia Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox3.ForeColor = SystemColors.ButtonFace;
            checkBox3.Location = new Point(0, 150);
            checkBox3.Margin = new Padding(2);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(732, 25);
            checkBox3.TabIndex = 4;
            checkBox3.Text = "Load FMOD";
            checkBox3.UseVisualStyleBackColor = false;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Cascadia Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(91, 21);
            label1.TabIndex = 5;
            label1.Text = "Game Path";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.ActiveCaptionText;
            richTextBox1.Dock = DockStyle.Top;
            richTextBox1.Font = new Font("Cascadia Mono SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            richTextBox1.ForeColor = SystemColors.ButtonHighlight;
            richTextBox1.Location = new Point(0, 21);
            richTextBox1.Margin = new Padding(2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(732, 21);
            richTextBox1.TabIndex = 6;
            richTextBox1.Text = "No path!";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ActiveCaptionText;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("Cascadia Mono", 36F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(0, 42);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(475, 63);
            label2.TabIndex = 7;
            label2.Text = "RON Mod Launcher";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ActiveCaptionText;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Cascadia Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(0, 105);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.MaximumSize = new Size(630, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 21);
            label3.TabIndex = 8;
            label3.Text = "randomText";
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.BackColor = SystemColors.ActiveCaptionText;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Dock = DockStyle.Bottom;
            checkBox4.Font = new Font("Cascadia Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox4.ForeColor = SystemColors.ButtonFace;
            checkBox4.Location = new Point(0, 125);
            checkBox4.Margin = new Padding(2);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(732, 25);
            checkBox4.TabIndex = 9;
            checkBox4.Text = "Start In DX12";
            checkBox4.UseVisualStyleBackColor = false;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.BackColor = SystemColors.ActiveCaptionText;
            checkBox5.Checked = true;
            checkBox5.CheckState = CheckState.Checked;
            checkBox5.Dock = DockStyle.Bottom;
            checkBox5.Font = new Font("Cascadia Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBox5.ForeColor = SystemColors.ButtonFace;
            checkBox5.Location = new Point(0, 100);
            checkBox5.Margin = new Padding(2);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(732, 25);
            checkBox5.TabIndex = 10;
            checkBox5.Text = "Override All VO";
            checkBox5.UseVisualStyleBackColor = false;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.BackColor = SystemColors.ActiveCaptionText;
            button3.Dock = DockStyle.Bottom;
            button3.Font = new Font("Cascadia Mono SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button3.ForeColor = SystemColors.ButtonFace;
            button3.Location = new Point(0, 389);
            button3.Name = "button3";
            button3.Size = new Size(732, 28);
            button3.TabIndex = 11;
            button3.Text = "Select Game Folder";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.AutoSize = true;
            button4.BackColor = SystemColors.ActiveCaptionText;
            button4.Dock = DockStyle.Bottom;
            button4.Font = new Font("Cascadia Mono SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button4.ForeColor = SystemColors.ButtonFace;
            button4.Location = new Point(0, 417);
            button4.Name = "button4";
            button4.Size = new Size(732, 26);
            button4.TabIndex = 12;
            button4.Text = "Backup Lossable Files";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.AutoSize = true;
            button5.BackColor = SystemColors.ActiveCaptionText;
            button5.Dock = DockStyle.Bottom;
            button5.Font = new Font("Cascadia Mono SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button5.ForeColor = SystemColors.ButtonFace;
            button5.Location = new Point(0, 443);
            button5.Name = "button5";
            button5.Size = new Size(732, 26);
            button5.TabIndex = 13;
            button5.Text = "Import Backup";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.AutoSize = true;
            button6.BackColor = SystemColors.ActiveCaptionText;
            button6.Dock = DockStyle.Bottom;
            button6.Font = new Font("Cascadia Mono SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button6.ForeColor = SystemColors.ButtonFace;
            button6.Location = new Point(0, 471);
            button6.Name = "button6";
            button6.Size = new Size(732, 26);
            button6.TabIndex = 14;
            button6.Text = "Import Backup";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // UI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(732, 469);
            Controls.Add(checkBox5);
            Controls.Add(checkBox4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button5);
            Margin = new Padding(2);
            Name = "UI";
            Text = "RON Mod Launcher";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal Button button1;
        internal Button button2;
        internal Button button3;
        internal Button button4;
        internal Button button5;
        internal Button button6;
        internal CheckBox checkBox1;
        internal CheckBox checkBox2;
        internal CheckBox checkBox3;
        internal CheckBox checkBox4;
        internal CheckBox checkBox5;
        internal Label label1;
        internal Label label2;
        internal Label label3;
        internal RichTextBox richTextBox1;
    }
}