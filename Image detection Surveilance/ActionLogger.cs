using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_detection_Surveilance
{
    class ActionLogger
    {
        private string filePath;

        public ActionLogger(string cameraName)
        {
            filePath = Application.StartupPath + @"\\" + cameraName + @"\\LogFile.txt";
            if(!Directory.Exists(Application.StartupPath + @"\\" + cameraName))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\\" + cameraName);
            }
            if (!File.Exists(filePath))
            {
                FileStream stream = File.Create(filePath);
                stream.Close();
                stream.Dispose();
            }
            

        }

        public void ActWrite(string message)
        {
            StreamWriter stream = File.AppendText(filePath); 
                //new FileStream(filePath, FileMode.Append);
            byte[] info = Encoding.ASCII.GetBytes(message.ToCharArray());
            stream.WriteLine("\n");
            stream.WriteLine(message);
            //stream.Write(info, 0, UnicodeEncoding.ASCII.GetByteCount(message.ToCharArray()));
            stream.Close();
            stream.Dispose();

        }
    }
}
