using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_detection_Surveilance
{
    public interface IcameraInterface
    {
        void normalCapture();
        void detectionCapture();
    }
}
