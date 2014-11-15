using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using com.gargoylesoftware.htmlunit;
using com.gargoylesoftware.htmlunit.util;
using java.util;
using PortlessWebHost;
using HtmlWebRequest = com.gargoylesoftware.htmlunit.WebRequest;
using HtmlWebResponse = com.gargoylesoftware.htmlunit.WebResponse;
using HtmlWebResponseData = com.gargoylesoftware.htmlunit.WebResponseData;

namespace NHtmlUnit.PortlessWebHost
{
    internal sealed class InternalWebConnection : WebConnection
    {
        private readonly WebHost host;

        public InternalWebConnection(WebHost host)
        {
            this.host = host;
        }

        public HtmlWebResponse getResponse(HtmlWebRequest wr)
        {
            PortlessWebRequest request = host.CreateRequest(new Uri(wr.getUrl().toString()));
            request.Method = wr.getHttpMethod().name();
            using (PortlessWebResponse response = request.GetPortlessResponse())
            {
                HtmlWebResponseData responseData = new HtmlWebResponseData(
                    GetResponseBody(response),
                    /*response.StatusCode*/200,
                    /*response.StatusDescription*/"200 OK",
                    GetHeaders(response));
                return new HtmlWebResponse(responseData, wr, 1);
            }
        }

        private static byte[] GetResponseBody(PortlessWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                byte[] body = new byte[responseStream.Length];
                responseStream.Read(body, 0, (int)responseStream.Length);
                return body;
            }
        }

        private static List GetHeaders(PortlessWebResponse response)
        {
            ArrayList headers = new ArrayList();
            foreach (string headerName in response.Headers.Keys)
            {
                headers.add(new NameValuePair(headerName, response.Headers[headerName]));
            }

            return headers;
        }
    }
}
