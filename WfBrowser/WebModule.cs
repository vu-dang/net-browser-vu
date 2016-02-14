using Nancy;

namespace WfBrowser
{
    public class WebModule : NancyModule
    {
        public WebModule()
        {
            Get["/data"] = p=> "hello world";
        }
    }
}