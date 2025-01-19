namespace wfaCompare
{
    partial class Form1
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
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            button2 = new Button();
            checkBox6 = new CheckBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(144, 29);
            button1.TabIndex = 0;
            button1.Text = "Выбрать файлы";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnChooseFile;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Enabled = false;
            checkBox1.Location = new Point(162, 12);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(153, 24);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Статичные линии";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Enabled = false;
            checkBox2.Location = new Point(162, 42);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(183, 24);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Статичные диагонали";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Enabled = false;
            checkBox3.Location = new Point(358, 12);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(182, 24);
            checkBox3.TabIndex = 3;
            checkBox3.Text = "Динамические линии";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Enabled = false;
            checkBox4.Location = new Point(358, 42);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(212, 24);
            checkBox4.TabIndex = 4;
            checkBox4.Text = "Динамические диагонали";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Enabled = false;
            checkBox5.Location = new Point(586, 12);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(106, 24);
            checkBox5.TabIndex = 5;
            checkBox5.Text = "Круг (Shift)";
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // button2
            // 
            button2.Location = new Point(732, 12);
            button2.Name = "button2";
            button2.Size = new Size(188, 54);
            button2.TabIndex = 6;
            button2.Text = "Отменить все изменения";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Enabled = false;
            checkBox6.Location = new Point(584, 43);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(142, 24);
            checkBox6.TabIndex = 7;
            checkBox6.Text = "Синхронизация";
            checkBox6.UseVisualStyleBackColor = true;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1004, 450);
            Controls.Add(checkBox6);
            Controls.Add(button2);
            Controls.Add(checkBox5);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Сравнение изображений";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private Button button2;
        private CheckBox checkBox6;
    }
}
