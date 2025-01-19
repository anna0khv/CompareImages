using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfaCompare
{
    internal class ImageComparison
    {
        public List<PictureBox> pictureBoxes = new List<PictureBox>();
        public List<Label> textBoxes = new List<Label>();
        
        public void SetImage(PictureBox pb)
        {
            pictureBoxes.Add(pb);

        }

        public void SetLabel(string name, Bitmap bm)
        {
            textBoxes.Add(new Label());
            textBoxes.Last().Text = $"NAME: {name}: {(int)bm.Size.Width} x {(int)bm.Size.Height}";

        }

        public (List<PictureBox>, List<Label>) ShowImages()
        {

            int counter = 0;

            // start position

            const int x_start = 20;
            const int y_start = 130;

            // position for calculating

            var x = 20;
            var y = 130;

            // space between images 

            var delta_x = 10;
            var delta_y = 40;

            // size of image 

            var image_w = 500;
            var image_h = 500;

            foreach (PictureBox pb in pictureBoxes)
            {
                switch (counter)
                {
                    case 0:
                        break;
                    case 1:
                        x += delta_x + image_w; 
                        break;
                    case 2:
                        x = x_start;
                        y = y_start + delta_y + (image_w * pb.Image.Height / pb.Image.Width);
                        break;
                    case 3:
                        x += delta_x + image_w;
                        break;
                    default:
                        break;
                }
                pb.Location = new Point(x, y);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Size = new Size(image_w, image_w * pb.Image.Height / pb.Image.Width);


                textBoxes[counter].Location = new Point(x, y - delta_y);
                textBoxes[counter].Size = new Size(image_w, delta_y);
                counter++;
            }
            return (pictureBoxes, textBoxes);

        }
       
    }
}
