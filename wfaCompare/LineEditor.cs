using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfaCompare
{
    public partial class LineEditor : Form
    {
        public int mySize = 1;
        public Color myColor = Color.Black;
        public LineEditor(int size, Color color)
        {
            InitializeComponent();
            pictureBox1.Paint += EditLine;

            mySize = size;
            myColor = color;
            trackBar1.Value = size;
            textBox1.Text = size.ToString();
            button1.BackColor = color;
            pictureBox1.Invalidate();
        }

        private void EditLine(object sender, PaintEventArgs e)
        {
            int left = 0;
            int top = 0;
            int right = pictureBox1.Width;
            int bottom = pictureBox1.Height;

            e.Graphics.DrawLine(new Pen(myColor, mySize), left, top, right, bottom);
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            mySize = trackBar1.Value;
            textBox1.Text = mySize.ToString();
            pictureBox1.Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mySize = int.Parse(textBox1.Text);
                trackBar1.Value = mySize;
            }
            catch (Exception ex)
            {
                textBox1.Text = mySize.ToString();
                trackBar1.Value = mySize;
            }
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.ShowDialog();
            myColor = dlg.Color;
            button1.BackColor = myColor;
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
