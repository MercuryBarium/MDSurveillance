using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_detection_Surveilance
{
    class ImageDetections
    {
        CascadeClassifier filter = new CascadeClassifier(Application.StartupPath + "/HAARCASCADE/haarcascade_frontalface_alt.xml");
        public Rectangle[] filterImage(Image<Bgr, byte> currentFrame)
        {
            using(Image<Gray,byte> grayFrame = currentFrame.Convert<Gray,byte>())
            {
                Rectangle[] rectangles = filter.DetectMultiScale(grayFrame);
                return rectangles;
            }
        }
        public Image<Bgr, byte> drawRectangles(Rectangle[] rectangles, Image<Bgr, byte> image)
        {
            foreach(Rectangle A in rectangles)
            {
                image.Draw(A, new Bgr(Color.Red), 1);
            }
            return image;
        }
    }
}
