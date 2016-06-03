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
        private bool loading = false;
        public Form()
        {
            InitializeComponent();
        }
        public void Browser_Load ()
        {
            Browser_Load ("php", "index.php", null);
        }
        public void Browser_Load (string action, string query)
        {
            Browser_Load("php", action, query);
        }
        public void Browser_Load (string host, string action, string query)
        {
            loading = true;
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    //parse_str($_SERVER[QUERY_STRING],$_REQUEST);
                    FileName = host,
                    Arguments = path + action + (query!=null?" "+query:""),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.StartInfo.EnvironmentVariables["QUERY_STRING"] = query;

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
                //browser.DocumentText = result;
                dynamic Document = browser.Document.DomDocument;
                Document.open();
                Document.write(result);
                Document.close();
            }

        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (browser.DocumentTitle!="")
            {
                this.Text = browser.DocumentTitle;
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            browser.Navigate ("about:blank");
            path = Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\";
            this.Text = path;
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(path+"favico.ico");
            }
            catch (FileNotFoundException exception)
            {
            
            }
            Browser_Load();
        }

        private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.PathAndQuery != "blank")
            {
                this.Text = e.Url.AbsoluteUri + " + " + e.Url.AbsolutePath.Substring(1) + " + " + e.Url.Query.Substring(1);
                Browser_Load(e.Url.AbsolutePath.Substring(1), e.Url.Query.Substring(1));
                //loading = false;
                e.Cancel = true;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Browser_Load ();
        }
    }
}
