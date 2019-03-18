using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_detection_Surveilance
{
    public partial class Form1 : Form
    {

        private FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        private List<CCTV> securityCameras = new List<CCTV>();
        private int Count = 0;

        public Form1()
        {
            
            InitializeComponent(); 
        }

        private void cameraAdder_Click(object sender, EventArgs e)
        {  
            try
            {
                ImageBox box = new ImageBox
                {
                    Location = new Point(20, 10 + securityCameras.Count * 400),
                    Size = new Size(600, 400),
                    TabIndex = 2,
                    TabStop = false
                };
                flowLayoutPanel1.Controls.Add(box);
                CCTV cTV = new CCTV(box, videoDevices[securityCameras.Count].Name, Count);
                cTV.MainStart();
                securityCameras.Add(cTV);
                Count++;
            }
            catch 
            {
                return;
            }
            
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            long X = 0;
            foreach(var C in securityCameras)
            {
                C.shuttingDown = true;
                Thread.Sleep(4000);
                C.T1.Abort();
                while (!C.readyToShutDown)
                {
                    C.IMGsaver();
                }
                Console.WriteLine(C.frameQueue.Count);
                X += C.totalImgsSaved;
            }
            Console.WriteLine("Total images saved!   " + X);
        }
    }
}
