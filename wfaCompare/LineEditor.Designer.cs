namespace wfaCompare
{
    partial class LineEditor
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
            groupBox1 = new GroupBox();
            textBox1 = new TextBox();
            trackBar1 = new TrackBar();
            groupBox2 = new GroupBox();
            button2 = new Button();
            button1 = new Button();
            groupBox3 = new GroupBox();
            pictureBox1 = new PictureBox();
            button3 = new Button();
            button4 = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(trackBar1);
            groupBox1.Location = new Point(12, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(289, 134);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Размер линии";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(129, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(35, 27);
            textBox1.TabIndex = 4;
            textBox1.Text = "1";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(35, 72);
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(221, 56);
            trackBar1.TabIndex = 3;
            trackBar1.Value = 1;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(button1);
            groupBox2.Location = new Point(12, 154);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(289, 83);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Цвет линий";
            // 
            // button2
            // 
            button2.Location = new Point(52, 37);
            button2.Name = "button2";
            button2.Size = new Size(204, 29);
            button2.TabIndex = 1;
            button2.Text = "Изменить текущий цвет...";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaptionText;
            button1.Enabled = false;
            button1.Location = new Point(18, 37);
            button1.Name = "button1";
            button1.Size = new Size(28, 29);
            button1.TabIndex = 0;
            button1.Text = " ";
            button1.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(pictureBox1);
            groupBox3.Location = new Point(12, 243);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(289, 101);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Результат";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(6, 26);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(277, 69);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button3
            // 
            button3.DialogResult = DialogResult.OK;
            button3.Location = new Point(141, 350);
            button3.Name = "button3";
            button3.Size = new Size(161, 29);
            button3.TabIndex = 3;
            button3.Text = "Сохранить и выйти";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.DialogResult = DialogResult.Cancel;
            button4.Location = new Point(18, 350);
            button4.Name = "button4";
            button4.Size = new Size(117, 29);
            button4.TabIndex = 4;
            button4.Text = "Отмена";
            button4.UseVisualStyleBackColor = true;
            // 
            // LineEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(314, 386);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "LineEditor";
            Text = "LineEditor";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TrackBar trackBar1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox textBox1;
        private Button button2;
        private Button button1;
        private PictureBox pictureBox1;
        private Button button3;
        private Button button4;
    }
}