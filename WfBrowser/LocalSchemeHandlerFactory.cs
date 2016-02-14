using CefSharp;

namespace WfBrowser
{
    class LocalSchemeHandlerFactory: ISchemeHandlerFactory
    {
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (schemeName == SchemeName && request.Url.EndsWith("CefSharp.Core.xml", System.StringComparison.OrdinalIgnoreCase))
            {
                //Display the CefSharp.Core.xml file in the browser
                return ResourceHandler.FromFileName("CefSharp.Core.xml", ".xml");
            }
            return new LocalResourceHandler();
        }

        public static string SchemeName => @"local";
    }
}