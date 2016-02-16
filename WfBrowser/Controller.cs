using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms.VisualStyles;

namespace WfBrowser
{
    internal class Controller
    {
        private IView View;
        private WebServer WebServer;
        private Uri HostBase;

        public Controller(IView view)
        {
            View = view;    
            
            //load ie-based control
            var appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var localPage = new Uri(Path.Combine(appDir, @"Static\index.html"));
            view.LoadIe(localPage.ToString());
            //view.LoadIe("http://html5test.com/");

            //load chrome-based control
            view.LoadChrome(@"local://Static/index.html");
            //view.LoadChrome("http://html5test.com/");

            //load OS browser
            WebServer = new WebServer();
            var root = WebServer.StartWebService();
            var indexPage = root + "/static/index.html";
            System.Diagnostics.Process.Start(indexPage);

        }

    }
}