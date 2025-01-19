using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace wfaCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            //this.Load += Form1_Load;
        }

        ImageComparison ic = new ImageComparison();
        public Point cursorPosition;
        public int rad = 10;

        private void btnChooseFile(object sender, EventArgs e)  // Загрузка и добавление картинок
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Выберите файл: ";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PictureBox pb1 = new PictureBox();
                var temp_bm = new Bitmap(dlg.FileName);
                pb1.Image = temp_bm;

                ic.SetImage(pb1);
                ic.SetLabel(dlg.FileName, temp_bm);

                (List<PictureBox> pbs, List<Label> tbs) = ic.ShowImages();  // получение координат изображений

                foreach (PictureBox pb in pbs)
                {
                    if (!this.Controls.Contains(pb)) // Проверяем, добавлен ли уже PictureBox
                    {
                        this.Controls.Add(pb);
                    }
                    pb.Paint += PictureBox_Paint;
                    //pb.MouseWheel += PictureBox_MouseWheel;
                }

                foreach (Label tb in tbs)
                {
                    if (!this.Controls.Contains(tb)) // Проверяем, добавлен ли уже PictureBox
                    {
                        this.Controls.Add(tb);
                    }
                }
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
            }

            
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate();
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb != null)
            {
                Pen myPen = new Pen(Color.Black, 1);
                if (checkBox1.Checked)
                {
                    e.Graphics.DrawLine(myPen, pb.Width / 3, 0, pb.Width / 3, pb.Height);
                    e.Graphics.DrawLine(myPen, 2 * pb.Width / 3, 0, 2 * pb.Width / 3, pb.Height);
                    e.Graphics.DrawLine(myPen, 0, pb.Height / 3, pb.Width, pb.Height / 3);
                    e.Graphics.DrawLine(myPen, 0, 2 * pb.Height / 3, pb.Width, 2 * pb.Height / 3);
                }
                if (checkBox2.Checked)
                {
                    e.Graphics.DrawLine(myPen, 0, 0, pb.Width, pb.Height);
                    e.Graphics.DrawLine(myPen, pb.Width, 0, 0, pb.Height);
                }
                if (checkBox3.Checked)  // Диагональные линии 
                {

                    e.Graphics.DrawLine(myPen, 0, 0, cursorPosition.X, cursorPosition.Y);
                    e.Graphics.DrawLine(myPen, pb.Width, 0, cursorPosition.X, cursorPosition.Y);
                    e.Graphics.DrawLine(myPen, 0, pb.Height, cursorPosition.X, cursorPosition.Y);
                    e.Graphics.DrawLine(myPen, pb.Width, pb.Height, cursorPosition.X, cursorPosition.Y);

                }
                if (checkBox4.Checked)  // Статичные линии
                {

                    e.Graphics.DrawLine(myPen, cursorPosition.X, 0, cursorPosition.X, pb.Height);
                    e.Graphics.DrawLine(myPen, 0, cursorPosition.Y, pb.Width, cursorPosition.Y);

                }
                if (checkBox5.Checked)
                {
                    e.Graphics.DrawEllipse(myPen, cursorPosition.X - rad / 2, cursorPosition.Y - rad / 2,
                        rad, rad);
                }


            }
        }



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate(); // Принудительно 
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
            foreach (PictureBox pb in ic.pictureBoxes)
            {
                if (checkBox3.Checked)
                    pb.MouseMove += PictureBox_PaintMove;
                else if (!checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked)
                    pb.MouseMove -= PictureBox_PaintMove;

            }
            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            foreach (PictureBox pb in ic.pictureBoxes)
            {
                if (checkBox4.Checked)
                    pb.MouseMove += PictureBox_PaintMove;
                else if (!checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked)
                    pb.MouseMove -= PictureBox_PaintMove;

            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            foreach (PictureBox pb in ic.pictureBoxes)
            {
                if (checkBox5.Checked)
                {
                    pb.MouseMove += PictureBox_PaintMove;
                    pb.MouseWheel += ChangeRad;
                }
                else if (!checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked)
                { 
                    pb.MouseMove -= PictureBox_PaintMove;
                    pb.MouseWheel -= ChangeRad;
                }

            }
        }

        private void PictureBox_PaintMove(object sender, MouseEventArgs e)
        {
            cursorPosition = e.Location;

            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate();

        }

        private void ChangeRad(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                rad += e.Delta > 0 ? 2 : -2; 

            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate();
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    // Подписываемся на событие MouseWheel для всех PictureBox
        //    foreach (PictureBox pb in ic.pictureBoxes)
        //    {
        //        pb.MouseWheel += PictureBox_MouseWheel;
        //    }
        //}

        //private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    if (Control.ModifierKeys == Keys.Control)
        //    {
        //        PictureBox pb = sender as PictureBox;
        //        if (pb != null && pb.Image != null)
        //        {
        //            // Определяем направление прокрутки
        //            int delta = e.Delta > 0 ? 10 : -10;

        //            // Масштабируем изображение
        //            float scale = 1.0f + delta / 100.0f;
        //            pb.SizeMode = PictureBoxSizeMode.Zoom;
        //            pb.Width = (int)(pb.Width * scale);
        //            pb.Height = (int)(pb.Height * scale);

        //            // Ограничиваем размеры PictureBox, чтобы изображение не выходило за пределы контейнера
        //            if (pb.Width > 500) pb.Width = 500;
        //            if (pb.Height > 500) pb.Height = 500;
        //            if (pb.Width < 100) pb.Width = 100;
        //            if (pb.Height < 100) pb.Height = 100;
        //        }
        //    }
        //}
    }
}
