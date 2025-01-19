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
        
        public void SetImage(PictureBox pb)
        {
            pictureBoxes.Add(pb);

        }

        public void SetLabel(string name, Bitmap bm)
        {
            textBoxes.Add(new Label());
            textBoxes.Last().Text = $"NAME: {name}: {(int)bm.Size.Width} x {(int)bm.Size.Height}";

        }

        public void SetBitmaps(Bitmap bmp)
        {
            bitmaps.Add(bmp);
        }

        public (List<PictureBox>, List<Label>) ShowImages(Form form)
        {

            int counter = 0;

            // start position

            const int x_start = 10;
            const int y_start = 130;

            // position for calculating

            var x = 20;
            var y = 130;

            // space between images 

            var delta_x = 10;
            var delta_y = 40;

            // size of image 

            int count_pbs = pictureBoxes.Count; // количество картинок

            var image_w = (form.Width) / count_pbs - 2 * x_start;
            var image_h = form.Height - y_start - x_start - 50;

            foreach (PictureBox pb in pictureBoxes)
            {
                int index = pictureBoxes.IndexOf(pb);
                x = x_start + index * (image_w +  delta_x);

                pb.Location = new Point(x, y);
                pb.SizeMode = PictureBoxSizeMode.Normal;
                pb.Size = new Size(image_w, image_h);

                textBoxes[counter].Location = new Point(x, y - delta_y);
                textBoxes[counter].Size = new Size(image_w, delta_y);

                counter++;
            }

            return (pictureBoxes, textBoxes);

        }
       
    }
}
