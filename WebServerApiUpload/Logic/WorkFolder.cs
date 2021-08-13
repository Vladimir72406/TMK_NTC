using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebServerApiUpload.Logic
{
    public class WorkFolder
    {
        public WorkFolder()
        { 
        }
        public string getFreeFileName(string path, string nameFile)
        {
            //
            string onlyName = string.Empty;
            string onlyNameOld = string.Empty;
            string extensionFile;
            string firstname = nameFile;

            onlyName = Path.GetFileNameWithoutExtension(nameFile);
            extensionFile = Path.GetExtension(nameFile);
            onlyNameOld = onlyName;


            for (int i = 0; i < 100; i++)
            {
                if (File.Exists(path + '\\' + onlyName + extensionFile))
                {
                    onlyName = onlyNameOld + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + i.ToString() + extensionFile;
                }
                else if (i == 100)
                {
                    onlyName = "";
                }
                else
                {
                    i = 100;
                    onlyName = onlyName + extensionFile;
                }
            }

            return onlyName;
        }
    }
}