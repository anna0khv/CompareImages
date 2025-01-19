using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfaCompare
{
    internal class ImageComparison
    {
        public List<PictureBox> pictureBoxes = new List<PictureBox>(); // Инициализация списка
        public List<Label> textBoxes = new List<Label>();
        //public int delta_x = 10;
        //public int delta_y = 10;

        //public int x = 20;
        //public int y = 50 + 40;
        //public int size_image = 500;

        //public int image_w = 500;
        //public int image_h = 500;

        public void SetImage(PictureBox pb)
        {
            pictureBoxes.Add(pb); // Добавляем элемент в список
            //textBoxes.Add(new Label());
            //textBoxes.Last().Text = pb.Name;

        }

        public void SetLabel(string name, Bitmap bm)
        {
            textBoxes.Add(new Label());
            textBoxes.Last().Text = $"NAME: {name}: {(int)bm.Size.Width} x {(int)bm.Size.Height}";

        }

        setPanel()

        public (List<PictureBox>, List<Label>) ShowImages()
        {

            var counter = 0;

            var delta_x = 10;
            var delta_y = 10;

            var x = 20;
            var y = 50+40+40;
            var size_image = 500;

            //var image_h = (int)pictureBoxes[0].Image.PhysicalDimension.Height;
            //var image_w = (int)pictureBoxes[0].Image.PhysicalDimension.Width;

            var image_w = 500;
            var image_h = 500;

            //pb1.SizeMode = PictureBoxSizeMode.Zoom;
            //pb1.Size = new Size(500, 500 * pb1.Height / pb1.Width);
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
                        x = 20;
                        y = 50 + delta_y + image_h;
                        break;
                    case 3:
                        x += delta_x + image_w;
                        break;
                    default:
                        break;
                }
                pb.Location = new Point(x, y);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Size = new Size(500, 500 * pb.Image.Height / pb.Image.Width);


                textBoxes[counter].Location = new Point(x, y - 40);
                textBoxes[counter].Size = new Size(500, 40);
                counter++;
            }
            return (pictureBoxes, textBoxes);

        }

        public void PrintStaticLines()
        {

        }
       
    }
}
