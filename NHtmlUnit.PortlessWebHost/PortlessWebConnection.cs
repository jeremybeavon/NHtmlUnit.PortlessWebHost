using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.gargoylesoftware.htmlunit;
using PortlessWebHost;
using HtmlWebRequest = com.gargoylesoftware.htmlunit.WebRequest;
using HtmlWebResponse = com.gargoylesoftware.htmlunit.WebResponse;

namespace NHtmlUnit.PortlessWebHost
{
    internal sealed class PortlessWebConnection : IWebConnection
    {
        private readonly WebConnection connection;

        public PortlessWebConnection(WebHost host)
        {
            connection = new InternalWebConnection(host);
        }

        public object WrappedObject
        {
            get { return connection; }
        }

        public WebResponse GetResponse(WebRequest wr)
        {
            return new WebResponse(connection.getResponse(wr.WObj));
        }
    }
}
