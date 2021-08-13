using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLoader.ReturnObjects
{
    public class ResultResponseWS : Result
    {
        public string nameFileWithWatermark;
        //public Image img;
        public byte[] byteOfImage;
        public ResultResponseWS(Boolean _code, string _info, string _nameFileWithWatermark) : base(_code, _info)
        {
            nameFileWithWatermark = _nameFileWithWatermark;
        }
    }
}
