using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CefSharp;

namespace WfBrowser
{
    public class LocalResourceHandler : IResourceHandler
    {
        private FileInfo FileInfo;


        //New: 
        //To upgrade: store the response stream in a class field, then call callback.Continue() instead of the old `requestCompletedCallback`.
        //See here for example of new usage: https://github.com/cefsharp/CefSharp/blob/cefsharp/43/CefSharp.Example/CefSharpSchemeHandler.cs
        public bool ProcessRequestAsync(IRequest request, ICallback callback)
        {
            try
            {
                Task.Run(delegate
                {
                    // Base to dispose of callback as it wraps a managed resource
                    using (callback)
                    {
                        var u = new Uri(request.Url);
                        var filepath = u.Authority + u.AbsolutePath;
                        FileInfo = new FileInfo(filepath);

                        // When processing complete call continue
                        callback.Continue();
                    }
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Stream GetResponse(IResponse response, out long responseLength, out string redirectUrl)
        {
            //Set to null if not redirecting to a different url
            redirectUrl = null;


            //Set response related stuff here
            response.MimeType = MimeMapping.GetMimeMapping(FileInfo.FullName);
            //How long is your stream?
            responseLength = FileInfo.Length;

            response.StatusCode = (int)HttpStatusCode.OK;
            response.StatusText = "OK";

            //Return your populated stream
            return new FileStream(FileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            
        }
    }
}
