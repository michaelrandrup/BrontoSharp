using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bronto.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bronto.API.BrontoService;

namespace Bronto.API.Tests
{
    [TestClass()]
    public class MailListsTests : BrontoBaseTestWithLogin
    {
        [TestMethod()]
        public void ReadTest()
        {
            MailLists mailLists = new MailLists(Login);
            List<mailListObject> lists = mailLists.Read();
        }

        [TestMethod()]
        public async Task ReadAsyncTest()
        {
            MailLists mailLists = new MailLists(Login);
            List<mailListObject> lists = await mailLists.ReadAsync();
        }


        [TestMethod()]
        public void AddListTest()
        {
            BrontoResult result = AddList();
            Assert.IsFalse(result.HasErrors);
        }

        private BrontoResult AddList(string name = null)
        {
            MailLists mailLists = new MailLists(Login);
            mailListObject list = new mailListObject()
            {
                name = "Test list " + DateTime.Now.ToString(),
                label = "Test list " + DateTime.Now.ToString()
            };
            return mailLists.Add(list);
        }

        private async Task<BrontoResult> AddListAsync()
        {
            MailLists mailLists = new MailLists(Login);
            mailListObject list = new mailListObject()
            {
                name = "Test list " + DateTime.Now.ToString(),
                label = "Test list " + DateTime.Now.ToString()
            };
            return await mailLists.AddAsync(list);
        }

        [TestMethod()]
        public async Task AddAsyncTest()
        {
            BrontoResult result = await AddListAsync();
            Assert.IsFalse(result.HasErrors);
        }


        [TestMethod()]
        public void UpdateListTest()
        {
            MailLists mailLists = new MailLists(Login);
            BrontoResult result = AddList();
            Assert.IsFalse(result.HasErrors, "The list for update was not added");
            mailListObject list = new mailListObject()
            {
                id = result.Items.First().Id,
                label = "Test list " + DateTime.Now.ToString()
            };
            result = mailLists.Update(list);
            Assert.IsFalse(result.HasErrors, "The update returned errors");
            Assert.IsFalse(result.Items.Any(x => x.IsNew), "the records was not updated");
        }

        [TestMethod()]
        public async Task UpdateAsyncTest()
        {
            MailLists mailLists = new MailLists(Login);
            BrontoResult result = await AddListAsync();
            Assert.IsFalse(result.HasErrors);
            mailListObject list = new mailListObject()
            {
                id = result.Items.First().Id,
                label = "Test list " + DateTime.Now.ToString()
            };
            result = await mailLists.UpdateAsync(list);
            Assert.IsFalse(result.HasErrors, "The update returned errors");
            Assert.IsFalse(result.Items.Any(x => x.IsNew), "the records was not updated");
        }


        [TestMethod()]
        public void DeleteTest()
        {
            MailLists mailLists = new MailLists(Login);
            BrontoResult result = AddList();
            Assert.IsFalse(result.HasErrors);
            mailListFilter filter = new mailListFilter()
            {
                name = new stringValue[]
                {
                    new stringValue()
                    {
                         @operator = filterOperator.StartsWith,
                          value = "Test list",
                           operatorSpecified = true
                    }
                }
            };
            List<mailListObject> lists = mailLists.Read(filter);
            Assert.IsTrue(lists.Count > 0);
            result = mailLists.Delete(lists);
            Assert.IsFalse(result.HasErrors);
            Assert.IsTrue(result.Items.Count >= lists.Count);
        }

        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            MailLists mailLists = new MailLists(Login);
            BrontoResult result = await AddListAsync();
            Assert.IsFalse(result.HasErrors);
            mailListFilter filter = new mailListFilter()
            {
                name = new stringValue[]
                {
                    new stringValue()
                    {
                         @operator = filterOperator.StartsWith,
                          value = "Test List",
                           operatorSpecified = true
                    }
                }
            };
            List<mailListObject> lists = await mailLists.ReadAsync(filter);
            Assert.IsTrue(lists.Count > 0);
            result = await mailLists.DeleteAsync(lists);
            Assert.IsFalse(result.HasErrors);
            Assert.IsTrue(result.Items.Count >= lists.Count);
        }

        
        

        
    }
}