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
using HtmlAgilityPack;
using System.Reflection;

namespace webview
{
    public partial class Form : System.Windows.Forms.Form
    {
        private string path;
        private Config config;
        public Form()
        {
            InitializeComponent();
            config = new Config();
            if (config.debug) split.Panel2Collapsed = false;
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
            Debug("config: " + config.file.path);
            Debug("config.command: " + config.command);
            Debug("config.action: " + config.action);
            Debug("config.debug: " + config.debug);
            Debug("config.plain: " + config.plain);
        }
        public void Debug (string message)
        {
            textboxDebug.AppendText (message+"\n");
        }
        public void Debug_Clear ()
        {
            textboxDebug.Clear();
        }
        public void Document_Path_Replace(HtmlAgilityPack.HtmlDocument source, string tag, string attribute)
        {
            var match = source.DocumentNode.SelectNodes("//" + tag + "[@" + attribute + "]");
            if (match != null)
            {
                foreach (HtmlNode link in match)
                {
                    if (!link.Attributes[attribute].Value.StartsWith("http"))
                    {
                        link.Attributes[attribute].Value = "file:///" + path + link.Attributes[attribute].Value;
                        Debug(tag + "." + attribute + ": " + link.Attributes[attribute].Value);
                    }
                }
            }
            else
            {
                Debug("no " + tag + "." + attribute + " found");
            }
        }
        public void Browser_Load()
        {
            Browser_Load(config.command, config.action, null);
        }
        public void Browser_Load (string action, string query)
        {
            Browser_Load(config.command, action, query);
        }
        public void Browser_Load (string command, string action, string query)
        {
            if (command!=null && command!="")
            {
                string parameters;
                command = command.Trim();
                if (command.Contains(" "))
                {
                    parameters = command.Substring(command.IndexOf(" "));
                    command = command.Substring(0, command.IndexOf(" "));
                }
                else
                {
                    parameters = "";
                }

                Debug("command: " + command);
                Debug("parmeters: "+parameters);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        //parse_str($_SERVER[QUERY_STRING],$_REQUEST);
                        FileName = command,
                        Arguments = parameters + (action!=""?path + action:"") + (query != null ? " " + query : ""),
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
                        result += line+ Environment.NewLine;
                    }
                }
                process.WaitForExit();

                if (result != null)
                {
                    //browser.DocumentText = result;
                    if (!config.plain)
                    {
                        Debug("HTML mode");

                        HtmlAgilityPack.HtmlDocument source = new HtmlAgilityPack.HtmlDocument();
                        source.LoadHtml(result);

                        Document_Path_Replace(source, "link", "href");
                        Document_Path_Replace(source, "img", "src");
                        Document_Path_Replace(source, "script", "src");

                        dynamic document = browser.Document.DomDocument;
                        document.open();
                        document.write(source.DocumentNode.InnerHtml);
                        document.close();
                    }
                    else
                    {
                        Debug("Plain mode");
                        dynamic document = browser.Document.DomDocument;
                        document.open();
                        document.write("<pre>" + result + "<pre>");
                        //document.write(result.Replace(Environment.NewLine,"<br>"));
                        document.close();
                    }

                }

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
            this.Text = path;
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(path+config.icon);
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
                Debug ("get: " + e.Url.AbsoluteUri + " + " + e.Url.AbsolutePath.Substring(1) + " + " + e.Url.Query.Substring(1));
                Browser_Load(e.Url.AbsolutePath.Substring(1), e.Url.Query.Substring(1));
                //loading = false;
                e.Cancel = true;
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Browser_Load();
        }
    }
}
