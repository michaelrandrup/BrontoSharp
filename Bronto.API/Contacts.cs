using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class Contacts : BrontoApiClass
    {

        public Contacts(LoginSession session)
            : base(session)
        {
            this.Timeout = TimeSpan.FromMinutes(15);
        }

        public BrontoResult Add(contactObject contact)
        {
            return Add(new contactObject[] { contact });
        }
        public BrontoResult Add(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.addContacts(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response);
            }
        }

        public async Task<BrontoResult> AddAsync(contactObject contact)
        {
            return await AddAsync(new contactObject[] { contact });
        }

        public async Task<BrontoResult> AddAsync(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                addContactsResponse response = await client.addContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }

        public BrontoResult AddOrUpdate(contactObject contact)
        {
            return AddOrUpdate(new contactObject[] { contact });
        }
        public BrontoResult AddOrUpdate(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.addOrUpdateContacts(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response);
            }
        }

        public async Task<BrontoResult> AddOrUpdateAsync(contactObject contact)
        {
            return await AddOrUpdateAsync(new contactObject[] { contact });
        }

        public async Task<BrontoResult> AddOrUpdateAsync(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                addOrUpdateContactsResponse response = await client.addOrUpdateContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }


        public List<contactObject> Read()
        {
            return Read(new contactFilter());
        }

        public List<contactObject> Read(contactFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readContacts c = new readContacts();
                c.filter = filter;
                c.pageNumber = 1;
                List<contactObject> list = new List<contactObject>();
                contactObject[] result = client.readContacts(session.SessionHeader, c);
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    result = client.readContacts(session.SessionHeader, c);
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                return list;
            }
        }

        public async Task<List<contactObject>> ReadAsync()
        {
            return await ReadAsync(new contactFilter());
        }
        public async Task<List<contactObject>> ReadAsync(contactFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readContacts c = new readContacts();
                c.filter = filter;
                c.pageNumber = 1;
                List<contactObject> list = new List<contactObject>();
                readContactsResponse response = await client.readContactsAsync(session.SessionHeader, c);
                contactObject[] result = response.@return;
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    response = await client.readContactsAsync(session.SessionHeader, c);
                    result = response.@return;
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                return list;
            }
        }

        public BrontoResult Delete(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.deleteContacts(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response);
            }
        }

        public async Task<BrontoResult> DeleteAsync(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                deleteContactsResponse response = await client.deleteContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }


        public List<fieldObject> Fields
        {
            get
            {
                
                using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
                {
                    readFields c = new readFields();
                    c.filter = new fieldsFilter();
                    c.pageNumber = 1;
                    List<fieldObject> list = new List<fieldObject>();
                    fieldObject[] result = client.readFields(session.SessionHeader, c);
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                    while (result != null && result.Length > 0)
                    {
                        c.pageNumber += 1;
                        result = client.readFields(session.SessionHeader, c);
                        if (result != null)
                        {
                            list.AddRange(result);
                        }
                    }
                    return list;
                }
            }
        }



    }
}
