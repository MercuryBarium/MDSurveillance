using Emgu.CV;
using Emgu.CV.Cuda;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_detection_Surveilance
{
    public partial class Form1 : Form
    {
        
        private List<CCTV> Cameras = new List<CCTV>();
        

        public Form1()
        {
            
            InitializeComponent();
           // findCameras();
        }

        private void cameraAdder_Click(object sender, EventArgs e)
        {

        }
    }
}
