using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_detection_Surveilance
{
    class CCTV 
    {
        private bool detected = false;
        private Image<Bgr, byte> lastpicture;
        private VideoCapture _cap = new VideoCapture();



        void normalCapture(object sender, EventArgs e)
        {
            if (detected == false)
            {
                Image<Bgr, byte> currentFrame = _cap.QueryFrame().ToImage<Bgr, byte>();

            }
        }

        void detectionCapture()
        {
            
        }
    }
}
