using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebServerApiUpload.ReturnObjects
{
    public class ResultImage : Result
    {
        public Image img;
        public byte[] arrayByteOfImage;

        public ResultImage(Boolean _code, string _info, Image _img) : base(_code, _info)
        {
            img = _img;
        }

        public ResultImage(Boolean _code, string _info) : base(_code, _info)
        {
        }
    }
}