using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webview
{
    public partial class Form : System.Windows.Forms.Form
    {
        private string path;
        public Form()
        {
            InitializeComponent();
        }
        public void Browser_Load()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "d:\\web\\php\\php.exe",
                    Arguments = "d:\\web\\www\\webview\\index.php",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();

            string result = null;

            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                if (line != null)
                {
                    result += line;
                }
            }
            process.WaitForExit();

            if (result != null)
            {
                browser.DocumentText = result;
            }

        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //this.Text = browser.DocumentTitle;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            path = Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.Text = path;
            this.Icon = null;
            Browser_Load();
        }
    }
}
