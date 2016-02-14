using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace WfBrowser
{
    class WebServer
    {
        public void Close()
        {
            try
            {
                Host.Stop();
            }
            catch (IOException)
            {
                //ignore
            }
        }

        internal string StartWebService()
        {
            var config = new HostConfiguration();
            config.UrlReservations.CreateAutomatically = false;
            config.RewriteLocalhost = false;

            var freeTcpPort = FreeTcpPort(90210);

            var networkAddress = InterNetworkAddress();

            HostBase = String.Format("http://{0}:{1}", networkAddress, freeTcpPort);

            var baseUri = new Uri(HostBase);
            Host = new NancyHost(baseUri, new WebBootstrapper(), config);

            try
            {

                Host.Start();

            }
            catch (Exception)
            {
                networkAddress = "localhost";
                HostBase = String.Format("http://{0}:{1}", networkAddress, freeTcpPort);
                baseUri = new Uri(HostBase);
                Host = new NancyHost(baseUri, new WebBootstrapper(), config);
                Host.Start();
            }

            Debug.Print("Web server running...");

            return HostBase;
        }

        protected NancyHost Host { get; set; }

        public string HostBase { get; set; }


        public static int FreeTcpPort(int suggestion)
        {
            try
            {
                var l = new TcpListener(IPAddress.Loopback, suggestion);
                l.Start();
                var port = ((IPEndPoint)l.LocalEndpoint).Port;
                l.Stop();
                return port;
            }
            catch (Exception)
            {

                var l = new TcpListener(IPAddress.Loopback, 0);
                l.Start();
                int port = ((IPEndPoint)l.LocalEndpoint).Port;
                l.Stop();
                return port;
            }
        }

        internal static string InterNetworkAddress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

    }
}