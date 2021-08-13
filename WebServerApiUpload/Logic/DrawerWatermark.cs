using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using WebServerApiUpload.ReturnObjects;

namespace WebServerApiUpload.Logic
{
    public class DrawerWatermark
    {
        public ResultImage addWotermark(string path, string nameFile, string textWatermark)
        {
            WorkFolder workFolder = new WorkFolder();
            ResultImage result = new ResultImage(true, "");
            Image img;
            Graphics g;

            if (!File.Exists(path + '\\' + nameFile))
            {
                result.code = false;
                result.info = "File not found.  " + path + '\\' + nameFile;
                return result;
            }

            img = Bitmap.FromFile(path + '\\' + nameFile);
            g = Graphics.FromImage(img);

            //анализировать корректность картинки пока что не делал.
            g.DrawString(textWatermark, new Font("Verdana", (float)20), new SolidBrush(Color.White), 15, img.Height / 2);

            var nameFileNew = workFolder.getFreeFileName(path, nameFile);
            if (nameFileNew == "")
            {
                result.code = false;
                result.info = "Directory have not free name";
                return result;                
            }

            MemoryStream memoryStream = new MemoryStream();
            img.Save(memoryStream, ImageFormat.Png);
            img.Save(path + "\\" + nameFileNew);            
            result.img = img;
            return result;
        }
    }
}