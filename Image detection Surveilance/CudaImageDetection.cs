﻿using Emgu.CV;
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
        private List<CudaCascadeClassifier> filters = new List<CudaCascadeClassifier>();


        public CudaImageDetection(string[] filterPaths)
        {
            foreach(string S in filterPaths)
            {
                filters.Add(new CudaCascadeClassifier(S));
            }
            //Application.StartupPath + "/CUDAHAAR/haarcascade_frontalface_alt.xml"
        }


        public List<Rectangle> FilterImage(Image<Bgr, byte> currentFrame) 
        {
            using (GpuMat faceRegionMat = new GpuMat())
            {
                List<Rectangle> rectangles = new List<Rectangle>();
                CudaImage<Gray, byte> cudaIMG = new CudaImage<Gray, byte>(currentFrame.Convert<Gray, byte>());
                foreach(CudaCascadeClassifier F in filters)
                {
                    F.DetectMultiScale(cudaIMG.Convert<Gray, byte>(), faceRegionMat);
                    Rectangle[] detectedSubjects = F.Convert(faceRegionMat);
                    foreach(Rectangle R in detectedSubjects)
                    {
                        rectangles.Add(R);
                    }
                }
                
                return rectangles;
            }
            
        }
        public Image<Bgr, byte> drawRectangles(List<Rectangle> rectangles, Image<Bgr, byte> image)
        {

            foreach(Rectangle R in rectangles)
            {
                image.Draw(R, new Bgr(Color.Red), 1);
            }
            

            return image;
        }
    }
}
