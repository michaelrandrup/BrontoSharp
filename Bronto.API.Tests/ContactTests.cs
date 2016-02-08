using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bronto.API.BrontoService;
using Bronto.API.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Runtime.InteropServices;

namespace Bronto.API.Tests
{
    [TestClass]
    public class ContactsTests : BrontoBaseTestWithLogin
    {
        private static List<fieldObject> fields = null;
        [TestInitialize]
        public void Initialize()
        {
            if (fields == null)
            {
                Contacts contacts = new Contacts(Login);
                fields = contacts.Fields;
            }

        }

        [TestMethod] 
        public void AddContactWithListReference()
        {
            MailLists lists = new MailLists(Login);
            Contacts contacts = new Contacts(Login);

            List<mailListObject> mailLists = new List<mailListObject>()
            {
                new mailListObject()
                {
                     name = "Test list A" + DateTime.Now,
                     label = "Test list A",
                      status = Bronto.API.ListStatus.Active
                },
                new mailListObject()
                {
                     name = "Test list B" + DateTime.Now,
                     label = "Test list B",
                      status = Bronto.API.ListStatus.Active
                }
            };

            BrontoResult listResult = lists.Add(mailLists);
            Assert.IsFalse(listResult.HasErrors, "The two test lists was not created");
            mailLists[0].id = listResult.Items[0].Id;
            mailLists[1].id = listResult.Items[1].Id;


            contactObject contact = GetTestContacts(1, new Random()).First();
            contact.listIds = listResult.Items.Select(x => x.Id).ToArray();
            BrontoResult contactResult = contacts.Add(contact);
            Assert.IsFalse(contactResult.HasErrors, "The contact was not created");

            contactFilter filter = new contactFilter();
            filter.listId = listResult.Items.Select(x => x.Id).ToArray();
            filter.type = filterType.OR;
            filter.typeSpecified = true;
            List<contactObject> listContacts = contacts.Read(filter, Contacts.ReadOptions.IncludeAll().IncludeFields(fields));
            Assert.IsTrue(listContacts.Count == 1);


            contact.listIds = new string[] { listResult.Items.Select(x => x.Id).First() };
            contactResult = contacts.AddOrUpdate(contact);
            contact.id = contactResult.Items[0].Id;
            Assert.IsFalse(contactResult.HasErrors, "The contact was not updated");
            Assert.IsTrue(contactResult.Items.Count(x => x.IsNew) == 0, "The contact was created instead of being updated");



            filter = new contactFilter();
            filter.listId = new string[] { listResult.Items.Select(x => x.Id).First() };
            listContacts = contacts.Read(filter, new readContacts().IncludeAll().IncludeFields(fields));
            Assert.IsTrue(listContacts.Count == 1);

            filter.listId = new string[] { listResult.Items.Select(x => x.Id).Last() };
            listContacts = contacts.Read(filter, new readContacts().IncludeAll().IncludeFields(fields));
            Assert.IsTrue(listContacts.Count == 0);

            listResult = lists.Delete(mailLists);
            Assert.IsFalse(listResult.HasErrors, "Unable to delete the lists");
            contactResult = contacts.Delete(new List<contactObject>() { contact });
            Assert.IsFalse(contactResult.HasErrors, "Unable to delete the contact");

        }



        [TestMethod]
        public void Add5Contacts()
        {
            Random r = new Random();
            IEnumerable<contactObject> people = GetTestContacts(5, r);
            StartTimer("Login to Bronto");
            Contacts contacts = new Contacts(this.Login);
            Console.WriteLine(EndTimer().ToString());
            StartTimer("Adding contacts to bronto");
            BrontoResult result = contacts.Add(people);
            Console.WriteLine(EndTimer().ToString());
            foreach (BrontoResultItem brontoResultItem in result.Items)
            {
                Console.WriteLine("{0}: ({1}) {2} (Is new: {3}. Is Error: {4}", brontoResultItem.Id,
                    brontoResultItem.ErrorCode, brontoResultItem.ErrorString, brontoResultItem.IsNew,
                    brontoResultItem.IsError);
            }
            Assert.IsFalse(result.HasErrors);
        }

        [TestMethod]
        public void Add100Contacts()
        {
            Random rnd = new Random();
            StartTimer("Create 100 random contacts");
            IEnumerable<contactObject> people = GetTestContacts(100, rnd);
            Console.WriteLine(EndTimer().ToString());
            StartTimer("Login to Bronto");
            Contacts contacts = new Contacts(this.Login);
            Console.WriteLine(EndTimer().ToString());
            StartTimer("Adding contacts to bronto");
            BrontoResult result = contacts.Add(people);
            Console.WriteLine(EndTimer().ToString());
            Assert.IsFalse(result.HasErrors);
            foreach (BrontoResultItem brontoResultItem in result.Items.Take(100))
            {
                Console.WriteLine("{0}: ({1}) {2} (Is new: {3}. Is Error: {4}", brontoResultItem.Id,
                    brontoResultItem.ErrorCode, brontoResultItem.ErrorString, brontoResultItem.IsNew,
                    brontoResultItem.IsError);
            }
        }

        [TestMethod]
        public void AddAndUpdateContact()
        {
            string email = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "") + "@example.com";
            contactObject contact = GetTestContact(email, "Michael", "Randrup");
            Contacts contacts = new Contacts(Login);
            Console.WriteLine("Adding a contact with the email {0}", email);
            BrontoResult result = contacts.AddOrUpdate(contact);
            Assert.IsTrue(result.Items.Count(x => x.IsNew == true) == 1);
            contact = GetTestContact(email, "Michael", "Randrup " + DateTime.Now.ToString());
            result = contacts.AddOrUpdate(contact);
            Assert.IsTrue(result.Items.Count(x => x.IsNew == true) == 0);
            contact.email = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "") + "@example.com";
            contact.id = result.Items.First().Id;
            Console.WriteLine("Updating the email from {0} to {1} for the created contact with id {2}", email, contact.email, contact.id);
            result = contacts.AddOrUpdate(contact);
            Assert.IsTrue(result.Items.Count(x => x.IsNew == true) == 0);
        }



        private string RandomString(int length, Random rnd)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string s = "";
            for (int i = 0; i < length; i++)
            {
                s += chars[rnd.Next(chars.Length)];
            }
            return s;
        }

        private contactObject GetTestContact(string Email, string FirstName, string LastName)
        {
            contactField[] customFields = new contactField[0];
            if (fields != null)
            {
                customFields = new contactField[]
                {
                        new contactField() { content = FirstName, fieldId = fields.IdOf("firstname")  },
                        new contactField() { content = LastName, fieldId = fields.IdOf("lastname") }
                };
            }
            return new contactObject()
            {
                email = Email,
                fields = customFields
            };
        }


        private IEnumerable<contactObject> GetTestContacts(int number, Random rnd)
        {
            List<contactObject> list = new List<contactObject>();


            for (int i = 0; i < number; i++)
            {

                contactField[] customFields = new contactField[0];
                if (fields != null)
                {
                    customFields = new contactField[]
                    {
                        new contactField() { content = "First name " + Guid.NewGuid().ToString(), fieldId = fields.IdOf("firstname") },
                        new contactField() { content = "Last name " + Guid.NewGuid().ToString(), fieldId = fields.IdOf("lastname") }
                    };
                }
                list.Add(new contactObject()
                {
                    email = string.Format("{0}@example.com", RandomString(14, rnd)),
                    fields = customFields,
                    created = DateTimeOffset.Now.DateTime,
                    createdSpecified = true
                });
            }
            return list;
        }

        [TestMethod]
        public void ReadAllContacts()
        {
            Contacts contacts = new Contacts(Login);
            StartTimer("Reading all contacts with @example.com in email");
            contactFilter filter = GetEmailContactFilter();
            List<contactObject> list = ReadContactsInternal(contacts, filter, null);
            Console.WriteLine(EndTimer().ToString());
            Console.WriteLine("{0} contacts read", list.Count);
            if (list.Count > 0)
            {
                WriteContactToConsole(list.First());
            }
        }

        [TestMethod]
        public void ReadAllContactsWithAllInfoAndFields()
        {
            Contacts contacts = new Contacts(Login);
            StartTimer("Reading all contacts with @example.com in email");
            contactFilter filter = GetEmailContactFilter();
            List<contactObject> list = ReadContactsInternal(contacts, filter, Contacts.ReadOptions.IncludeAll().IncludeFields(fields));
            Console.WriteLine(EndTimer().ToString());
            Console.WriteLine("{0} contacts read", list.Count);
            if (list.Count > 0)
            {
                WriteContactToConsole(list.First());
            }
        }

        private void WriteContactToConsole(contactObject contact)
        {
            Console.WriteLine("First contact returned: {0} with ID {1} and Status {2}, created on {3}", contact.email, contact.id, contact.status, contact.created.ToBrontoString());
            if (contact.fields != null)
            {
                Console.WriteLine("Contact fields:");
                contact.fields.ToList().ForEach(field =>
                {
                    Console.WriteLine("{0} ({1}/{3}) has the value '{2}'. ", fields.LabelOf(field), fields.NameOf(field), field.content, fields.TypeOf(field));
                });
            }
        }

        private contactFilter GetEmailContactFilter()
        {
            contactFilter filter = new contactFilter();
            filter.email = new stringValue[]
            {
                new stringValue()
                {
                    @operator = filterOperator.Contains,
                     operatorSpecified = true,
                      value = "example.com"
                }
            };
            return filter;
        }

        private List<contactObject> ReadContactsInternal(Contacts contacts, contactFilter filter, readContacts options)
        {
            return contacts.Read(filter, options);
        }

        [TestMethod]
        public void DeleteAllContacts()
        {
            Contacts contacts = new Contacts(Login);
            StartTimer("Reading all contacts with @example.com in email");
            contactFilter filter = new contactFilter();
            filter.email = new stringValue[]
            {
                new stringValue()
                {
                    @operator = filterOperator.Contains,
                     operatorSpecified = true,
                      value = "example.com"
                }
            };
            List<contactObject> list = contacts.Read(filter);
            Console.WriteLine(EndTimer().ToString());
            Console.WriteLine("{0} contacts read", list.Count);
            StartTimer("Deleting all contacts with @example.com in email");
            BrontoResult result = contacts.Delete(list.Select(x => { return new contactObject() { id = x.id }; }));
            if (result.HasErrors)
            {
                Console.WriteLine("Completed with errors");
                foreach (BrontoResultItem brontoResultItem in result.Items.Where(x => x.IsError))
                {
                    Console.WriteLine("{0}: ({1}) {2} (Is new: {3}. Is Error: {4}", brontoResultItem.Id,
                        brontoResultItem.ErrorCode, brontoResultItem.ErrorString, brontoResultItem.IsNew,
                        brontoResultItem.IsError);
                }
            }
            else
            {
                Console.WriteLine("{0} contacts deleted", result.Items.Count);
            }
        }

        [TestMethod]
        public void ReadAllContactFields()
        {
            Contacts contacts = new Contacts(Login);
            StartTimer("Reading all fields");
            List<fieldObject> fields = contacts.Fields;
            Console.WriteLine(EndTimer().ToString());
            Console.WriteLine("{0} fields read:", fields.Count);
            fields.ForEach(field =>
            {
                Console.WriteLine("{0}: {1} ({2}) of type {3} (visibility: {4})", field.id, field.name, field.label, field.type, field.visibility);
                if (field.options != null)
                {
                    Console.WriteLine("Options:");
                    field.options.ToList().ForEach(option =>
                    {
                        Console.WriteLine("{0}: {1} (default: {2})", option.label, option.value, option.isDefault);
                    });
                }
            });

        }


    }
}
