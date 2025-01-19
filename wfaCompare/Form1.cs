using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace wfaCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ImageComparison ic = new ImageComparison();
        public Point cursorPosition;
        public int rad = 10;

        private float _zoomFactor = 1.0f; 
        private float _defaultZoomFactor = 1.0f; 
        private Point _imagePosition = new Point(0, 0);
        private bool _isDragging = false;
        private Point _dragStart;
        private bool isFirst = true;
        private bool Sync = false;

        private float[] zoomMas = { 1.0f, 1.0f, 1.0f, 1.0f };
        private Point[] positionMas = { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0) };
        private void btnChooseFile(object sender, EventArgs e)  // Загрузка и добавление картинок
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Выберите файл: ";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                PictureBox pb1 = new PictureBox();
                var temp_bm = new Bitmap(dlg.FileName);

                if (isFirst)
                {
                    float nw = temp_bm.Width;
                    _defaultZoomFactor = 500.0f / nw;
                    _zoomFactor = _defaultZoomFactor;
                    for (int i = 0; i < 4;  i++) {
                        zoomMas[i] = _defaultZoomFactor;
                    }
                    isFirst = false;
                }

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

                    // Подписываемся на события для зума и перемещения
                    pb.MouseWheel += PictureBox_MouseWheel;
                    pb.MouseDown += PictureBox_MouseDown;
                    pb.MouseMove += PictureBox_MouseMove;
                    pb.MouseUp += PictureBox_MouseUp;
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
                checkBox6.Enabled = true;
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate();
            //PictureBoxes_Invalidate(sender);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            if (pb != null && pb.Image != null)
            {
                // Вычисляем новые размеры изображения с учетом масштаба
                //int index = Array.IndexOf(ic.pictureBoxes, sender as PictureBox);
                int index = ic.pictureBoxes.IndexOf(sender as PictureBox);
                //int newWidth = (int)(pb.Image.Width * _zoomFactor);
                int newWidth = (int)(pb.Image.Width * zoomMas[index]);
                //int newHeight = (int)(pb.Image.Height * _zoomFactor);
                int newHeight = (int)(pb.Image.Height * zoomMas[index]);

                // Рисуем изображение с учетом масштаба и положения
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //e.Graphics.DrawImage(pb.Image, _imagePosition.X, _imagePosition.Y, newWidth, newHeight);
                e.Graphics.DrawImage(pb.Image, positionMas[index].X, positionMas[index].Y, newWidth, newHeight);

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
            //PictureBoxes_Invalidate(sender);
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
            //PictureBoxes_Invalidate(sender);

        }

        private void ChangeRad(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
                rad += e.Delta > 0 ? 2 : -2;

            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate();
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                int index = ic.pictureBoxes.IndexOf(sender as PictureBox);
                if (e.Delta > 0)
                {
                    zoomMas[index] *= 1.1f;
                    //_zoomFactor *= 1.1f;
                }
                else
                {
                    zoomMas[index] /= 1.1f;
                    //_zoomFactor /= 1.1f;
                }

                //_zoomFactor = Math.Max(0.1f, Math.Min(5.0f, _zoomFactor));
                zoomMas[index] = Math.Max(0.1f, Math.Min(5.0f, zoomMas[index]));
                if (Sync) {
                    _zoomFactor = zoomMas[index];
                    for (int i = 0; i < 4; i++)
                    {
                        zoomMas[i] = _zoomFactor;
                    }
                }

                // Перерисовываем PictureBox
                //(sender as PictureBox).Invalidate();
                //foreach (PictureBox pb in ic.pictureBoxes)
                //    pb.Invalidate();
                PictureBoxes_Invalidate(sender);
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStart = e.Location;
            }
            //foreach (PictureBox pb in ic.pictureBoxes)
            //    pb.Invalidate();
            PictureBoxes_Invalidate(sender);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                int deltaX = e.X - _dragStart.X;
                int deltaY = e.Y - _dragStart.Y;

                int index = ic.pictureBoxes.IndexOf(sender as PictureBox);
                //_imagePosition.X += deltaX;
                //_imagePosition.Y += deltaY;
                positionMas[index].X += deltaX;
                positionMas[index].Y += deltaY;
                if (Sync)
                {
                    _imagePosition = positionMas[index];
                    for (int i = 0; i < 4; i++)
                    {
                        positionMas[i] = _imagePosition;
                    }
                }

                // Перерисовываем PictureBox
                (sender as PictureBox).Invalidate();

                // Обновляем начальную точку
                _dragStart = e.Location;
            }
        }

        // Обработчик события MouseUp для завершения перетаскивания
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _imagePosition = new Point(0, 0);
            _zoomFactor = _defaultZoomFactor;
            _imagePosition.X = 0;
            _imagePosition.Y = 0;

            for (int i = 0; i < 4; i++)
            {
                zoomMas[i] = _defaultZoomFactor;
                positionMas[i] = _imagePosition;
            }

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate();
            //PictureBoxes_Invalidate(sender);

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Sync = checkBox6.Checked;
            for (int i = 1; i < 4; i++)
            {
                zoomMas[i] = zoomMas[0];
                positionMas[i] = positionMas[0];
            }
            //PictureBoxes_Invalidate(sender);
            foreach (PictureBox pb in ic.pictureBoxes)
                pb.Invalidate();
        }

        private void PictureBoxes_Invalidate(object sender)
        {
            if (Sync)
            {
                foreach (PictureBox pb in ic.pictureBoxes)
                    pb.Invalidate();
            }
            else
            {
                if (sender is PictureBox tmp_box)
                {
                    tmp_box.Invalidate();
                }
            }
        }
    }
}
