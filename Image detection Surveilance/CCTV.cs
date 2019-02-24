using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_detection_Surveilance
{
    class CCTV 
    {
        public CCTV(ImageBox imBox, string dest, int cameraInt)
        {
            imageBox = imBox;
            destFolder = dest;
            NCAM = cameraInt;
        }
        private bool detected = false;
        private static int NCAM;
        private Image<Bgr, byte> lastFrame;
        private VideoCapture _cap = new VideoCapture(NCAM);
        private ImageBox imageBox;
        private string destFolder;
        

        public void MainStart()
        {
            Timer TN = new Timer();
            TN.Tick += new EventHandler(NormalCapture);
            TN.Interval = 33;
            TN.Start();
        }
        

        private void NormalCapture(object sender, EventArgs e)
        {
            if (detected == false)
            {
                Image<Bgr, byte> currentFrame = _cap.QueryFrame().ToImage<Bgr, byte>();
                DateTime timeNow = DateTime.Now;

                string S = destFolder + "  " + timeNow.ToString();
                CvInvoke.PutText(currentFrame, S, new System.Drawing.Point(10, 25), FontFace.HersheyComplex, 0.5, new Bgr(0,0,255).MCvScalar);
                
                imageBox.Image = currentFrame;
                imageBox.Invalidate();
            }
        }

        private void detectionCapture(object sender, EventArgs e)
        {
            
        }
    }
}
