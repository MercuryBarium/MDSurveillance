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
using System.Security.AccessControl;

namespace Image_detection_Surveilance
{
    class SaveImage 
    {
        private FileStream stream;

        public void saveJpeg(Image<Bgr, byte> image, string saveFolder, string fileName)
        {
            string desiredDIR = Application.StartupPath + @"\\" + saveFolder;

            if (!Directory.Exists(desiredDIR))
            {
                

                Directory.CreateDirectory(desiredDIR);
            }

            stream = File.OpenWrite(desiredDIR + @"\\" + fileName + ".jpeg");
            Image SystemImage = image.ToBitmap();
            SystemImage.Save(stream, ImageFormat.Jpeg);
            stream.Close();
        }

        
    }
}
