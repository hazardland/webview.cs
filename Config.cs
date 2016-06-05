using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webview
{
    class Config
    {
        public IniFile file;
        public string command;
        public string action;
        public bool debug;
        public bool plain;
        public Config ()
        {
            file = new IniFile ();
            command = file.Read ("command","exec");
            action = file.Read ("action", "exec");
            string value;
            value = file.Read("debug","misc");
            this.debug = (value.ToLower()=="true" || value== "1");
            value = file.Read("plain", "misc");
            this.plain = (value.ToLower() == "true" || value == "1");
        }
    }
}
