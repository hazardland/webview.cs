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
using CefSharp;
using CefSharp.WinForms;

namespace webview
{
    public partial class Form : System.Windows.Forms.Form, IRequestHandler
    {
        private string path;
        private Config config;
        ChromiumWebBrowser browser;
        public Form()
        {
            InitializeComponent();
            config = new Config();
            split.Panel2Collapsed = !config.debug;
            CefSettings settings = new CefSettings();
            settings.CachePath = null;
            settings.CefCommandLineArgs.Add ("disable-application-cache", "1");
            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser ("file:///"+path);
            browser.IsBrowserInitializedChanged += this.OnBrowserInitialized;
            browser.BrowserSettings.ApplicationCache = 0;
            browser.RequestHandler = this;
            split.Panel1.Controls.Add (browser);
            //browser.ScrollBarsEnabled = config.scrollbar;
            //browser.ScriptErrorsSuppressed = false;
            path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
            Debug("config: " + config.file.path);
            Debug("config.command: " + config.command);
            Debug("config.action: " + config.action);
            Debug("config.debug: " + config.debug);
            Debug("config.plain: " + config.plain);
        }
        public void OnBrowserInitialized (object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if (e.IsBrowserInitialized)
            {
                Invoke(new Action(() => Browser_Load()));
            }
        }
        public void Debug (string message)
        {

            if (Application.OpenForms.Count>0 && Application.OpenForms[0].InvokeRequired)
            {
                Invoke(new Action(() => textboxDebug.AppendText(message + "\n")));
                //we're on the main thread
            }
            else
            {
                textboxDebug.AppendText(message + "\n");
            }
            //textboxDebug.AppendText (message+"\n");
        }
        public void Debug_Clear ()
        {
            textboxDebug.Clear();
        }
        public void Document_Path_Replace(HtmlAgilityPack.HtmlDocument source, string tag, string attribute, string with)
        {
            var match = source.DocumentNode.SelectNodes("//" + tag + "[@" + attribute + "]");
            if (match != null)
            {
                foreach (HtmlNode link in match)
                {
                    if (!link.Attributes[attribute].Value.StartsWith("http"))
                    {
                        link.Attributes[attribute].Value = with + link.Attributes[attribute].Value;
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

                        Document_Path_Replace(source, "link", "href", "file:///" + path);
                        Document_Path_Replace(source, "img", "src", "file:///" + path);
                        Document_Path_Replace(source, "script", "src", "file:///" + path);
                        Document_Path_Replace(source, "form", "action", "http://localhost/");
    
                        browser.LoadString(source.DocumentNode.InnerHtml, "file:///"+path);

                        //browser.LoadString (source.DocumentNode.InnerHtml, "about:blank"); 
                        //dynamic document = browser.Document.DomDocument;
                        //document.open();
                        //document.write(source.DocumentNode.InnerHtml);
                        //document.close();
                    }
                    else
                    {
                        Debug("Plain mode");
                        browser.LoadString("<style>html{background:black}pre{color:silver;font-family:monospaced}</style><pre>" + result + "<pre>", "");
                        
                        //dynamic document = browser.Document.DomDocument;
                        //document.open();
                        //document.write("<style>html{background:black}pre{color:silver;font-family:monospaced}</style><pre>" + result + "<pre>");
                        //document.write(result.Replace(Environment.NewLine,"<br>"));
                        //document.close();
                    }

                }

            }


        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            /*
            if (browser.DocumentTitle!="")
            {
                this.Text = browser.DocumentTitle;
            }
            */
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //browser.Navigate ("about:blank");
            this.Text = path;
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(path+config.icon);
            }
            catch (FileNotFoundException exception)
            {
            
            }
            
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
            Debug_Clear();
            //browser.Refresh();
            Browser_Load();
        }

        private void buttonToggle_Click(object sender, EventArgs e)
        {
            if (buttonToggle.Text=="▼")
            {
                split.SplitterDistance = this.Height - 29;
                buttonToggle.Text = "▲";
            }
            else
            {
                split.SplitterDistance = this.Height-(this.Height/3);
                buttonToggle.Text = "▼";
            }
        }

        private void buttonDebug_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }

        public bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
        {
            Invoke(new Action(() => Debug("OnBeforeBrowse " + request.Url)));
            if (request.Url== "chrome-devtools://devtools/inspector.html" || request.Url=="file:///")
            {
                return false;
            }
            int from = request.Url.IndexOf("://");
            from = request.Url.IndexOf("/", from + 4);
            string url = request.Url.Substring(from + 1);
            string action = "";
            string query = "";
            if (url.Contains("?"))
            {
                from = url.IndexOf("?");
                action = url.Substring(0,from);
                query = url.Substring(from+1);
            }
            else
            {
                action = url;
            }
            Invoke(new Action(() => Debug("url=" + url+",  action="+action+" query="+query)));
            Browser_Load(action, query);
            return true;
        }

        public bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            Invoke(new Action(() => Debug("OnOpenUrlFromTab " + targetUrl)));
            return false;
        }

        public bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            Invoke(new Action(() => Debug("OnOpenUrlFromTab " + requestUrl)));
            return false;
        }

        public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
        {
            Invoke(new Action(() => Debug("OnPluginCrashed " + pluginPath)));
        }

        public CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            Invoke(new Action(() => Debug("CefReturnValue " + request.Url)));
            return CefReturnValue.Continue;
        }

        public bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            Invoke(new Action(() => Debug("GetAuthCredentials")));
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
        {
            Invoke(new Action(() => Debug("OnRenderProcessTerminated")));
        }

        public bool OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            Invoke(new Action(() => Debug("OnQuotaRequest")));
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    //Accept Request to raise Quota
                    //callback.Continue(true);
                    //return true;
                }
            }

            return false;
        }

        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, ref string newUrl)
        {
            Invoke(new Action(() => Debug("OnResourceRedirect " + request.Url)));
        }

        public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
        {
            Invoke(new Action(() => Debug("OnProtocolExecution")));
            return false;
        }

        public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
        {
            Invoke(new Action(() => Debug("OnRenderViewReady")));
        }

        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            Invoke(new Action(() => Debug("OnResourceResponse")));
            return false;
        }

        public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            Invoke(new Action(() => Debug("IResponseFilter")));
            return null;
        }

        public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            Invoke(new Action(() => Debug("OnResourceLoadComplete")));
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
