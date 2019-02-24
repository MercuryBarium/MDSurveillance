using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;

namespace Image_detection_Surveilance
{
    class SaveImage
    {
        public SaveImage(Image<Bgr, byte> image, string saveFolder, string fileName)
        {
            string desiredDIR = Application.StartupPath + "/" + saveFolder;
            if (Directory.Exists(desiredDIR)) 
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    
                    
                }
            }
            else
            {
                Directory.CreateDirectory(desiredDIR);
            }
        }
    }
}
