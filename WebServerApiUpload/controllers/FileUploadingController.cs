using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebServerApiUpload.Logic;
using WebServerApiUpload.ReturnObjects;

namespace WebServerApiUpload.controllers
{
    public class FileUploadingController : ApiController
    {
        [HttpPost]
        [Route("Api/FileUploading/UploadFile")]
        public async Task<HttpResponseMessage> UploadFile()
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            WorkFolder workFolder = new WorkFolder();
            ResultImage resultImg = new ResultImage(true, "");
            DrawerWatermark drawerWatermark = new DrawerWatermark();
            Image image;
            
            try
            {
                var rootFolder = @"C:\DWl\DWQ";// брать из файла ресурсов
                var provider = new MultipartFormDataStreamProvider(rootFolder);
                
                await Request.Content.ReadAsMultipartAsync(provider);
                
                foreach (var file in provider.FileData)
                {                    
                    var name = file.Headers.ContentDisposition.FileName.Trim('"');                   

                    var localFileName = file.LocalFileName;
                    name = workFolder.getFreeFileName(rootFolder, name);
                    var filePath = Path.Combine(rootFolder, name);

                    
                    File.Move(localFileName, filePath);
                    resultImg = drawerWatermark.addWotermark(rootFolder, name, DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss"));
                    if (resultImg.code == false)
                    {                        
                        httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
                        httpResponseMessage.ReasonPhrase = resultImg.info;
                        return httpResponseMessage;
                    }
                    
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        image = resultImg.img;
                        image.Save(memoryStream, ImageFormat.Jpeg);
                        httpResponseMessage.Content = new ByteArrayContent(memoryStream.ToArray());
                        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                        httpResponseMessage.StatusCode = HttpStatusCode.OK;
                    }
                    

                    return httpResponseMessage;
                }
                return httpResponseMessage;
            }
            catch (Exception exx)
            {                
                httpResponseMessage.StatusCode = HttpStatusCode.BadRequest;
                httpResponseMessage.ReasonPhrase = exx.Message.ToString();
                return httpResponseMessage;
            }

            //return httpResponseMessage;            
        }

       

        [HttpGet]
        [Route("Api/FileUploading/startTest")]
        public string startTest(string s)
        {
            //test 
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
