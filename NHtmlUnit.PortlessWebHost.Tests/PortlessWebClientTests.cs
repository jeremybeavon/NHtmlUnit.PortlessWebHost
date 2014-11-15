using System;
using System.IO;
using System.Reflection;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

using System.Web;
using System.Web.Hosting;
using AppDomainAspects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHtmlUnit.Html;
using PortlessWebHost;

namespace NHtmlUnit.PortlessWebHost.Tests
{
    [TestClass]
    public class PortlessWebClientTests
    {
        private WebHost host;

        [TestInitialize]
        public void SetUp()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string physicalPath = Path.Combine(baseDirectory, @"..\..\..\NHtmlUnit.PortlessWebHost.TestWebSite");
            host = new WebHost("/", Path.GetFullPath(physicalPath));
            DefaultAppDomainProvider.AppDomain = host.Domain;
        }

        [TestCleanup]
        public void TearDown()
        {
            DefaultAppDomainProvider.AppDomain = null;
            host.Dispose();
        }

        [TestMethod]
        [RunInDifferentAppDomain]
        public void TestSimpleCall()
        {
            PortlessWebClient webClient = new PortlessWebClient(WebHost.Current);
            HtmlPage page = (HtmlPage)webClient.GetPage("http://localhost.test/Account/Login");
            string html = page.AsText();
            html.Should().Contain("ASP.NET");
        }
    }
}
