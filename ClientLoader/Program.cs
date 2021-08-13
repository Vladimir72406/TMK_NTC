using ClientLoader.Logic;
using ClientLoader.ReturnObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ClientLoader
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string shortNameFile = string.Empty;
            string extensionFile = string.Empty;
            string directoryName = string.Empty;
            string nameNewFile = string.Empty;
            string nameFile = string.Empty;
            string nameServer = string.Empty;
            Image img;
            Loader loader = new Loader();

            string testNameFile = "";// @"C:\Users\zhukv\Postman\files\DSC100357706.png";            
            string testNameUri = "";// "http://webapi.local:85/api/FileUploading/UploadFile";
                                    //testNameUri = "https://localhost:44388/api/FileUploading/UploadFile";

            Console.WriteLine("Введите путь к файлу : ");
            nameFile = Console.ReadLine();
                        
            Console.WriteLine("Введите адрес сервера : ");
            nameServer = Console.ReadLine();            

            var resultValidInputParam = Validation.checkValid(nameFile, nameServer);

            if (resultValidInputParam.code == false)
            {
                Console.WriteLine("Указаны некорректные параметры. " + resultValidInputParam.info);
                Console.ReadLine();
                return;
            }

            //для тестирования использовал так
            testNameFile = @"C:\Users\zhukv\Postman\files\DSC100357706.png";                        
            testNameUri = "https://localhost:44388/api/FileUploading/UploadFile";
            testNameUri = "http://webapi.local:85/api/FileUploading/UploadFile";

            if (nameFile == "") nameFile = testNameFile;
            if (nameServer == "")
            {
                nameServer = testNameUri;
            }
            else { nameServer = nameServer + @"/api/FileUploading/UploadFile"; }

            shortNameFile = Path.GetFileNameWithoutExtension(nameFile);
            extensionFile = Path.GetExtension(nameFile);
            directoryName = Path.GetDirectoryName(nameFile);

            Console.WriteLine("\n\n");
            Console.WriteLine("Файл для загрузки  : " + nameFile);
            Console.WriteLine("Сервер  : " + nameServer + "\n\n");

            Task<ResultResponseWS> taskResultOfLoad = loader.startLoad(nameFile, shortNameFile, extensionFile, nameServer);
            taskResultOfLoad.Wait();

            var resultLoad = taskResultOfLoad.Result;
            if (resultLoad.code == true)
            {                
                try
                {                    
                    using (var ms = new MemoryStream(resultLoad.byteOfImage))
                    {
                        img = Image.FromStream(ms);                        
                        nameNewFile = directoryName + "\\" + shortNameFile + "_" + DateTime.Now.Minute.ToString() 
                                                                                    + DateTime.Now.Minute.ToString() 
                                                                                    + DateTime.Now.Second.ToString() 
                                                                                    + extensionFile;
                        img.Save(nameNewFile);
                        Console.WriteLine("Файл сохранен : " + nameNewFile);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Результат загрузки : " + resultLoad.info);
            }
            Console.ReadLine();
        }       
    }
}
