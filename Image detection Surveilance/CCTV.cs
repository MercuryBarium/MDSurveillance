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
    class frameClass
    {
        public frameClass(Image<Bgr, byte> image, string id)
        {
            img = image;
            name = id;
        }
        public Image<Bgr,byte> img;
        public string name;
    }
    
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
        public bool readyToShutDown = false;
        public bool shuttingDown = false;


       
        
        private List<frameClass> frameQueue = new List<frameClass>();

        public void MainStart()
        {
            Console.WriteLine("Using Camera " + NCAM);
            Timer TN = new Timer();
            TN.Tick += new EventHandler(NormalCapture);
            TN.Interval = 33;
            TN.Start();

            Timer QueueSaver = new Timer();
            QueueSaver.Tick += new EventHandler(ImageQueueSaver);
            QueueSaver.Start();
        }

        private void ImageQueueSaver(object sender, EventArgs e)
        {
            if(frameQueue.Count > 0)
            {
                imageSaver.saveJpeg(frameQueue[0].img, destFolder, frameQueue[0].name);
                Console.WriteLine("Images left to save:     " + frameQueue.Count);
                frameQueue.RemoveAt(0);

            } 
            else if (frameQueue.Count == 0)
            {
                readyToShutDown = true;
            }
            
        }
        

        private void NormalCapture(object sender, EventArgs e)
        {
            if(!shuttingDown)
            {
                if (detected == false)
                {
                    Image<Bgr, byte> currentFrame = _cap.QueryFrame().ToImage<Bgr, byte>();
                    DateTime timeNow = DateTime.Now;

                    string S = destFolder + "  " + timeNow.ToString();
                    CvInvoke.PutText(currentFrame, S, new System.Drawing.Point(10, 25), FontFace.HersheyComplex, 0.5, new Bgr(0, 0, 255).MCvScalar);
                    frameClass P = new frameClass(currentFrame, timeNow.ToString("yyyy-dd-M--HH-mm-ss-ms"));
                    frameQueue.Add(P);

                    imageBox.Image = currentFrame;
                    imageBox.Invalidate();
                }
            }
        }

        private void detectionCapture(object sender, EventArgs e)
        {
            if(!shuttingDown)
            {

            }
            
        }
    }
}
