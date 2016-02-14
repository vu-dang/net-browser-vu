using System;

namespace WfBrowser
{
    internal interface IView
    {
        void LoadChrome(string url);
        void LoadIe(string url);
    }
}