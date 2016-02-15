using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace WfBrowser
{
    public partial class FormMain : Form, IView
    {
        [DllImport(@"urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(
            int dwOption,
            string pBuffer,
            int dwBufferLength,
            int dwReserved);

        private const int UrlmonOptionUseragent = 0x10000001;

        private Controller Controller;
        private ChromiumWebBrowser Browser;
        private WebBrowser WebBrowser;

        public FormMain()
        {
            InitializeComponent();
            Controller = new Controller(this);
        }


        public void LoadChrome(string url)
        {
            var settings = new CefSettings();
            //settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1");
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("disable-direct-write", "1");

            settings.RegisterScheme(new CefCustomScheme()
            {
                SchemeName = LocalSchemeHandlerFactory.SchemeName,
                SchemeHandlerFactory = new LocalSchemeHandlerFactory()
            });
            Cef.Initialize(settings);

            Browser = new ChromiumWebBrowser(url.ToString()) {Dock = DockStyle.Fill};
            this.tableLayoutPanel.Controls.Add(Browser,0,0);
        }

        public void LoadIe(string url)
        {
            WebBrowser = new WebBrowser()
            {
                Dock = DockStyle.Fill,
                AllowNavigation = false,
                AllowWebBrowserDrop = false,
                ScriptErrorsSuppressed = true,
                IsWebBrowserContextMenuEnabled = false
            };
            this.tableLayoutPanel.Controls.Add(WebBrowser,1,0);
            ChangeUserAgent();
            WebBrowser.Navigate(url);
        }

        public void ChangeUserAgent()
        {
            // http://stackoverflow.com/a/12648705/107625
            const string ua = @"Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

            // http://stackoverflow.com/q/937573/107625
            UrlMkSetSessionOption(UrlmonOptionUseragent, ua, ua.Length, 0);
        }
    }
}
