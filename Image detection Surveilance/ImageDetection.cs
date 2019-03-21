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
    class ImageDetection
    {
        List<CascadeClassifier> filters = new List<CascadeClassifier>();

        public ImageDetection(string[] cascadePaths)
        {
            foreach(string S in cascadePaths)
            {
                filters.Add(new CascadeClassifier(S));
            }
            
        }

        public List<Rectangle> filterImage(Image<Bgr, byte> currentFrame)
        {
            using(Image<Gray,byte> grayFrame = currentFrame.Convert<Gray,byte>())
            {
                List<Rectangle> rectangles = new List<Rectangle>();
                foreach(CascadeClassifier F in filters)
                {
                    Rectangle[] rect = F.DetectMultiScale(grayFrame);
                    foreach(Rectangle R in rect)
                    {
                        rectangles.Add(R);
                    }
                }

                return rectangles;
            }
        }
        public Image<Bgr, byte> drawRectangles(List<Rectangle> rectangles, Image<Bgr, byte> image)
        {
            foreach(Rectangle A in rectangles)
            {
                image.Draw(A, new Bgr(Color.Red), 1);
            }
            return image;
        }
    }
}
