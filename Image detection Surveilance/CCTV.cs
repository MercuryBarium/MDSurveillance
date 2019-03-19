using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Image_detection_Surveilance
{
    class frameClass
    {
        public frameClass(Image<Bgr, byte> image, string id)
        {
            img = image;
            name = id;
        }
        public Image<Bgr, byte> img;
        public string name;
    }
    
    class CCTV 
    {
        public CCTV(ImageBox imBox, string dest, int cameraInt)
        {
            imageBox = imBox;
            destFolder = dest;
            log = new ActionLogger(dest);
            NCAM = cameraInt;
            _cap = new VideoCapture(NCAM);
        }
        private ActionLogger log;
        private bool detected = false;
        private int NCAM;
        private Image<Bgr, byte> lastFrame;
        private VideoCapture _cap;
        private MotionHistory history = new MotionHistory(1, 0.05, 0.5);
        private ImageBox imageBox;
        private string destFolder;
        private SaveImage imageSaver = new SaveImage();
        public bool readyToShutDown = false;
        public bool shuttingDown = false;
        public List<frameClass> frameQueue = new List<frameClass>();
        public long totalImgsSaved = 0;
        public Thread T1;

        

        public void MainStart()
        {
            Console.WriteLine("Using Camera " + NCAM);
            Timer TN = new Timer();
            TN.Tick += new EventHandler(NormalCapture);
            TN.Interval = 1000;
            TN.Start();

            T1 = new Thread(new ThreadStart(ImageQueueSaver));
            T1.Priority = ThreadPriority.Highest;
            T1.Start();
        }

        public void IMGsaver()
        {
            if (frameQueue.Count > 0)
            {
                try
                {
                    imageSaver.saveJpeg(frameQueue[0].img, destFolder, frameQueue[0].name);
                    Console.WriteLine("Images left to save:     " + frameQueue.Count);
                    frameQueue.RemoveAt(0);
                    if(frameQueue.Count == 0)
                    {
                        readyToShutDown = true;
                    } else
                    {
                        readyToShutDown = false;
                    }
                    totalImgsSaved++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            
        }

        

        private void ImageQueueSaver()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                IMGsaver();
            }
        }
        

        private void NormalCapture(object sender, EventArgs e)
        {
            if(!shuttingDown)
            {
                if (!detected)
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
