using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            if (!File.Exists(Application.StartupPath + @"\CascadePath.txt"))
            {
                OpenFileDialog f = new OpenFileDialog();
                f.InitialDirectory = "C:\\";
                f.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                f.FilterIndex = 2;
                f.Multiselect = true;
                f.RestoreDirectory = true;
                if (CudaInvoke.HasCuda)
                {
                    MessageBox.Show("CUDA CAPABILITES DETETECTED! Please select a HaarCascade for CUDA");
                } else
                {
                    MessageBox.Show("Please select a regular HaarCascade");
                }

                f.ShowDialog();
                File.WriteAllLines(Application.StartupPath + @"\CascadePath.txt", f.FileNames);

            }

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
                CCTV cTV = new CCTV(box, videoDevices[securityCameras.Count].Name, Count, File.ReadAllLines(Application.StartupPath + @"\CascadePath.txt"));
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
            Console.WriteLine(Application.StartupPath);
        }
    }
}
