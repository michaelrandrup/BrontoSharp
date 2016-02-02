using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bronto.API.Tests
{
    [TestClass]
    public class TestLoginSession : BrontoBaseTestClass
    {
        [TestMethod]
        public void LoginUsingApiToken()
        {
            Bronto.API.LoginSession login = Bronto.API.LoginSession.Create(ApiToken);
            Assert.IsFalse(string.IsNullOrEmpty(login.SessionId));
            Console.WriteLine("Ready to use bronto api with session id {0}",login.SessionId);
        }

        [TestMethod]
        public async Task LoginUsingApiTokenAsync()
        {
            Bronto.API.LoginSession login = await Bronto.API.LoginSession.CreateAsync(ApiToken);
            Assert.IsFalse(string.IsNullOrEmpty(login.SessionId));
            Console.WriteLine("Ready to use bronto api with session id {0}", login.SessionId);
        }
    }
}
