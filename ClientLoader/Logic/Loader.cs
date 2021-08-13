using ClientLoader.ReturnObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace ClientLoader.Logic
{
    public class Loader
    {

        public async Task<ResultResponseWS> startLoad(string nameFile, string shortName, string extentsionFile, string nameServer)
        {
            byte[] picture = new byte[100];
            MultipartFormDataContent content = new MultipartFormDataContent();            
            ResultResponseWS resultResponseWS = new ResultResponseWS(true, "", "");
            HttpResponseMessage response;
            Image imgWithWatermark;
            byte[] byteArrayOfImageWithWatermark = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(nameServer);//("http:/ /localhost:55587/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    picture = File.ReadAllBytes(nameFile);
                    content.Add(new ByteArrayContent(picture, 0, picture.Length), "1", shortName + extentsionFile);

                    response = await client.PostAsync(nameServer, content);
                    Task.WaitAll();


                    if (response.StatusCode == HttpStatusCode.OK) //response.IsSuccessStatusCode
                    {
                        byteArrayOfImageWithWatermark = response.Content.ReadAsByteArrayAsync().Result;
                        Task.WaitAll();
                        using (var ms = new MemoryStream(byteArrayOfImageWithWatermark))
                        {
                            imgWithWatermark = Image.FromStream(ms);                            
                            resultResponseWS.byteOfImage = new byte[byteArrayOfImageWithWatermark.Length];
                            resultResponseWS.byteOfImage = byteArrayOfImageWithWatermark;
                            //return resultResponseWS;
                        }
                    }
                    else
                    {
                        resultResponseWS.code = false;
                        resultResponseWS.info = response.ReasonPhrase + "\n\n\n\r" + response.RequestMessage.ToString();
                    }
                }                
                return resultResponseWS;
            }
            catch (Exception e)
            {
                resultResponseWS.code = false;
                resultResponseWS.info = e.Message.ToString();
                return resultResponseWS;
            }


        }

    }
}