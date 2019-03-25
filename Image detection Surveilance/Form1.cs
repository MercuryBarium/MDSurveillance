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
        // This makes a list of all available video- and image devices.
        private FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

        // This is a list for storing the CCTV class inside. It is important for the shutdown sequence.
        private List<CCTV> securityCameras = new List<CCTV>();


        public Form1()
        {
            // The following if-statement checks if CascadePath.txt does not exist.
            if (!File.Exists(Application.StartupPath + @"\CascadePath.txt"))
            {
                // A file dialog utilizing the windows file explorer is created.
                OpenFileDialog f = new OpenFileDialog();
                f.InitialDirectory = "C:\\";
                f.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                f.FilterIndex = 2;
                f.Multiselect = true;
                f.RestoreDirectory = true;

                // The following if statement asks if the computer has any Cuda processors.
                if (CudaInvoke.HasCuda)
                {
                    /* If the statement returns "true" then the user will be presented with a pop up message 
                     * asking for the user to select one or more filters made for Cuda processing*/
                    MessageBox.Show("CUDA CAPABILITES DETETECTED! Please select a HaarCascade for CUDA");
                } else
                {
                    // If however, the statement anything but "true" then the user will be asked to select a regular filter for normal processing.
                    MessageBox.Show("Please select a regular HaarCascade");
                }

                // After checking for Cuda capabilities, the file explorer window pops up.
                f.ShowDialog();
                
                /* When the user has selected one or more filters, the program uses File.WriteAllLines to write all of the filter paths to a 
                 * TXT file. If CascadePaths.txt doesn't exist then the method will create a new file and  in this statement, 
                 * CascadePaths.txt never exists anyway, else it would not execute. */
                File.WriteAllLines(Application.StartupPath + @"\CascadePath.txt", f.FileNames);

            }

            // A required method for .NET designer support. Derived from the "Form" interface.
            InitializeComponent();


        }

        // The function below is for adding the video- and image devices.
        private void cameraAdder_Click(object sender, EventArgs e)
        {  
            try
            {
                // ImagBox is a class which is part of the Emgu CV framework. This is where the program displays all pictures.
                ImageBox box = new ImageBox
                {
                    Location = new Point(20, 10 + securityCameras.Count * 400),
                    Size = new Size(600, 400),
                    TabIndex = 2,
                    TabStop = false
                };

                /* The FlowLayoutPanel is a content box which changes dynamically when items are added to it. 
                 * The next line of code adds the ImageBox to the content box.*/
                flowLayoutPanel1.Controls.Add(box);

                CCTV cTV = new CCTV(box, videoDevices[securityCameras.Count].Name, securityCameras.Count, File.ReadAllLines(Application.StartupPath + @"\CascadePath.txt"));
                cTV.MainStart();
                securityCameras.Add(cTV);
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
