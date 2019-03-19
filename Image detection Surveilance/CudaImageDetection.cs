using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_detection_Surveilance
{
    class CudaImageDetection
    {
        private CudaCascadeClassifier filter;


        public CudaImageDetection(/*string filterPath*/)
        {
            filter = new CudaCascadeClassifier(Application.StartupPath + "/haarcascade_frontalface_alt_tree.xml");
        }


        public Rectangle[] FilterImage(Image<Bgr, byte> currentFrame) 
        {
            using (GpuMat faceRegionMat = new GpuMat())
            {
                CudaImage<Gray, byte> cudaIMG = new CudaImage<Gray, byte>(currentFrame);
                filter.DetectMultiScale(cudaIMG.Convert<Gray, byte>(), faceRegionMat);
                Rectangle[] detectedSubjects = filter.Convert(faceRegionMat);
                return detectedSubjects;
            }
            
        }
        public Image<Bgr, byte> drawRectangles(Rectangle[] rectangles, Image<Bgr, byte> image)
        {

            foreach(Rectangle R in rectangles)
            {
                image.Draw(R, new Bgr(Color.Red), 1);
            }
            

            return image;
        }
    }
}
