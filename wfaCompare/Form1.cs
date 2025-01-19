using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace wfaCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            if (ic.pictureBoxes.Count > 0)
            {float nh = ic.bitmaps[0].Height;
            _defaultZoomFactor = (this.Height - 140) / nh;
            _zoomFactor = _defaultZoomFactor;
            for (int i = 0; i < 4; i++)
            {
                zoomMas[i] = _defaultZoomFactor;
            }

            ic.ShowImages(this);
                button2_Click(sender, e);
            }

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
                    //float nw = temp_bm.Width;
                    float nh = temp_bm.Height;
                    _defaultZoomFactor = (this.Height - 140) / nh;

                    //_defaultZoomFactor = 500.0f / nw;
                    _zoomFactor = _defaultZoomFactor;
                    for (int i = 0; i < 4; i++)
                    {
                        zoomMas[i] = _defaultZoomFactor;
                    }
                    isFirst = false;
                }

                pb1.Image = null; // Очищаем изображение, чтобы оно не отображалось автоматически
                pb1.SizeMode = PictureBoxSizeMode.Normal; // Устанавливаем Normal

                ic.SetImage(pb1);
                ic.SetLabel(dlg.FileName, temp_bm);
                ic.SetBitmaps(temp_bm);

                (List<PictureBox> pbs, List<Label> tbs, List<System.Windows.Forms.Button> btns) = ic.ShowImages(this);  // получение координат изображений

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
                }

                foreach (Label tb in tbs)
                {
                    if (!this.Controls.Contains(tb)) // Проверяем, добавлен ли уже PictureBox
                    {
                        this.Controls.Add(tb);
                    }
                }

                foreach (System.Windows.Forms.Button btn in btns)
                {
                    if (!this.Controls.Contains(btn)) // Проверяем, добавлен ли уже PictureBox
                    {
                        this.Controls.Add(btn);
                        btn.BringToFront();
                        btn.Click += Btn_Click;
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

        private void Btn_Click(object? sender, EventArgs e)
        {
            System.Windows.Forms.Button clickedButton = sender as System.Windows.Forms.Button;
            if (clickedButton != null)
            {
                int index = ic.buttons.IndexOf(clickedButton);

                // Удаляем PictureBox, Label и кнопку
                if (index >= 0 && index < ic.pictureBoxes.Count)
                {
                    this.Controls.Remove(ic.pictureBoxes[index]);
                    this.Controls.Remove(ic.textBoxes[index]);
                    this.Controls.Remove(ic.buttons[index]);

                    ic.pictureBoxes.RemoveAt(index);
                    ic.textBoxes.RemoveAt(index);
                    ic.buttons.RemoveAt(index);

                    // Обновляем расположение оставшихся элементов
                    Form1_Resize(this, EventArgs.Empty);
                }
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
                // Вычисляем новые размеры изображения с учетом масштаба
                int index = ic.pictureBoxes.IndexOf(sender as PictureBox);
                int newWidth = (int)(ic.bitmaps[index].Width * zoomMas[index]);
                int newHeight = (int)(ic.bitmaps[index].Height * zoomMas[index]);

                // Рисуем изображение с учетом масштаба и положения
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e.Graphics.DrawImage(ic.bitmaps[index], positionMas[index].X, positionMas[index].Y, newWidth, newHeight);

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
            PictureBoxes_Invalidate(sender);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                int deltaX = e.X - _dragStart.X;
                int deltaY = e.Y - _dragStart.Y;

                int index = ic.pictureBoxes.IndexOf(sender as PictureBox);

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

        private void Button_Click(object sender, EventArgs e)
        {
            ;
        }
    }
}
