using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLoader.ReturnObjects
{
    public class Result
    {
        public Boolean code;
        public string info;
        public Result() { }

        public Result(Boolean _code, string _info)
        {
            code = _code;
            info = _info;
        }
    }
}
