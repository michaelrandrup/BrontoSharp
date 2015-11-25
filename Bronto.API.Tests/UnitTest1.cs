using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bronto.API.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            Bronto.API.LoginSession login = Bronto.API.LoginSession.Create("API_TOKEN");
            Bronto.API.Contacts contacts = new Contacts(login);
            BrontoResult result  = await contacts.AddAsync(new BrontoService.contactObject() { email = "john@doe.com" });
            Assert.IsTrue(result.HasErrors == false);
        }
    }
}
