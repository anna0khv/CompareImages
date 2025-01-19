using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace wfaCompare
{
    internal class ImageComparison
    {
        public List<PictureBox> pictureBoxes = new List<PictureBox>();
        public List<Label> textBoxes = new List<Label>();
        public List<Bitmap> bitmaps = new List<Bitmap>();
        public List<System.Windows.Forms.Button> buttons = new List<System.Windows.Forms.Button>();
        public bool VERTICAL = true;
        public void SetImage(PictureBox pb)
        {
            pictureBoxes.Add(pb);

        }

        public void SetLabel(string name, Bitmap bm)
        {
            textBoxes.Add(new Label());
            textBoxes.Last().Text = $"NAME: {name}: {(int)bm.Size.Width} x {(int)bm.Size.Height}";
            buttons.Add(new System.Windows.Forms.Button());


        }

        public void SetBitmaps(Bitmap bmp)
        {
            bitmaps.Add(bmp);
        }

        public (List<PictureBox>, List<Label>, List<System.Windows.Forms.Button>) 
            ShowImages(Form form)
        {

            int counter = 0;

            // start position

            const int x_start = 10;
            const int y_start = 130;

            const int btn_size = 40;

            // position for calculating

            var x = 20;
            var y = 130;

            // space between images 

            var delta_x = 10;
            var delta_y = 40;

            // size of image 

            int count_pbs = pictureBoxes.Count; // количество картинок


            int image_h, image_w, nw, tmp_w;
            
            image_h = form.Height - y_start - x_start - 50;
            nw = bitmaps[0].Width * image_h / bitmaps[0].Height;
            tmp_w = (form.Width) / count_pbs - 2 * x_start;
            image_w = count_pbs > 1 || nw > tmp_w ?
                tmp_w
                : nw;

            if (!VERTICAL) {
                if (count_pbs == 1)
                {
                    ;
                }
                else
                {
                    image_h = count_pbs <= 2 ?
                        form.Height - y_start - x_start - 50
                        : (form.Height - y_start - 50 - x_start - 30) / 2;

                    nw = bitmaps[0].Width * image_h / bitmaps[0].Height;
                    tmp_w = (form.Width) / count_pbs - 2 * x_start;

                    image_w = 
                        (form.Width) / 2 - 2 * x_start;
                }
            }
               
            

            foreach (PictureBox pb in pictureBoxes)
            {
                int index = pictureBoxes.IndexOf(pb);
                

                if (!VERTICAL)
                {
                    switch(counter)
                    {
                        case 0:
                            x = x_start;
                            y = y_start;
                            break;
                        case 1:
                            x += image_w + delta_x;
                            break;
                        case 2:
                            x = x_start;
                            y += image_h + delta_y;
                            break;
                        case 3:
                            x += image_w + delta_x;
                            break;
                    }
                } else
                {
                    x = x_start + index * (image_w + delta_x);
                }
                pb.Location = new Point(x, y);
                pb.SizeMode = PictureBoxSizeMode.Normal;
                pb.Size = new Size(image_w, image_h);

                textBoxes[counter].Location = new Point(x, y - delta_y);
                textBoxes[counter].Size = new Size(image_w - btn_size, delta_y);
                textBoxes[counter].BackColor = Color.Gray;

                buttons[counter].Location = new Point(x + image_w - btn_size, y - delta_y);
                buttons[counter].Size = new Size(btn_size, btn_size);
                buttons[counter].Text = "X";

                counter++;
            }

            return (pictureBoxes, textBoxes, buttons);

        }
       
    }
}
