using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortlessWebHost;
using HtmlWebClient = com.gargoylesoftware.htmlunit.WebClient;

namespace NHtmlUnit.PortlessWebHost
{
    public sealed class PortlessWebClient : WebClient
    {
        public PortlessWebClient(WebHost host)
        {
            WebConnection = new PortlessWebConnection(host);
        }

        public PortlessWebClient(WebHost host, BrowserVersion browserVersion)
            : base(browserVersion)
        {
            WebConnection = new PortlessWebConnection(host);
        }

        public PortlessWebClient(WebHost host, HtmlWebClient webClient)
            : base(webClient)
        {
            WebConnection = new PortlessWebConnection(host);
        }
    }
}
