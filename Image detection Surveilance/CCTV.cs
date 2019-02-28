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
            _cap = new VideoCapture(NCAM);
        }
        private bool detected = false;
        private int NCAM;
        private Image<Bgr, byte> lastFrame;
        private VideoCapture _cap;
        private ImageBox imageBox;
        private string destFolder;
        private SaveImage imageSaver = new SaveImage();
        private frameClass(Image<Bgr, byte> picture, string name)
        {
            img = picture;
            fName = name;
        }

        public void MainStart()
        {
            Console.WriteLine("Using Camera " + NCAM);
            Timer TN = new Timer();
            TN.Tick += new EventHandler(NormalCapture);
            TN.Interval = 1000;
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

                imageSaver.saveJpeg(currentFrame, destFolder, timeNow.ToString("yyyy-dd-M--HH-mm-ss-ms"));
                imageBox.Image = currentFrame;
                imageBox.Invalidate();
            }
        }

        private void detectionCapture(object sender, EventArgs e)
        {

            
        }
    }
}
