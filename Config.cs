using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webview
{
    class Config
    {
        public Tool.Config file;
        public string command;
        public string action;
        public bool debug;
        public bool plain;
        public bool scrollbar;
        public string icon;
        public Config ()
        {
            file = new Tool.Config (Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\config.ini");
            command = file.Value ("command");
            action = file.Value ("action");
            string value;
            value = file.Value("debug","true");
            debug = (value.ToLower()=="true" || value== "1");
            value = file.Value("plain", "false");
            plain = (value.ToLower() == "true" || value == "1");
            value = file.Value("scrollbar", "true");
            scrollbar = (value.ToLower() == "true" || value == "1");
            icon = file.Value ("icon", "favico.ico");
        }
    }
}
