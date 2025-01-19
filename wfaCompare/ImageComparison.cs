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

        public (List<PictureBox>, List<Label>, List<System.Windows.Forms.Button>) ShowImages(Form form)
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

            
            var image_h = form.Height - y_start - x_start - 50;
            var nw = bitmaps[0].Width * image_h / bitmaps[0].Height;
            var tmp_w = (form.Width) / count_pbs - 2 * x_start;

            var image_w = count_pbs > 1 || nw > tmp_w ?
                tmp_w
                : nw;

            foreach (PictureBox pb in pictureBoxes)
            {
                int index = pictureBoxes.IndexOf(pb);
                x = x_start + index * (image_w +  delta_x);

                pb.Location = new Point(x, y);
                pb.SizeMode = PictureBoxSizeMode.Normal;
                pb.Size = new Size(image_w, image_h);

                textBoxes[counter].Location = new Point(x, y - delta_y);
                textBoxes[counter].Size = new Size(image_w - btn_size, delta_y);

                buttons[counter].Location = new Point(x + image_w - btn_size, y - delta_y);
                buttons[counter].Size = new Size(btn_size, btn_size);
                buttons[counter].Text = "X";

                counter++;
            }

            return (pictureBoxes, textBoxes, buttons);

        }
       
    }
}
