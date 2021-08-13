using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientLoader.ReturnObjects;

namespace ClientLoader.Logic
{
    public static class Validation
    {
        public static Result checkValid(string nameFile, string nameServer)
        {
            Result result = new Result();
            result.code = true;

            //проверка наименования и др
            if (!File.Exists(nameFile))
            {
                result.code = false;
                result.info = "Файл не найден.";
                return result;
            }
            
            // и др.

            return result;
        }
    
    }
}
