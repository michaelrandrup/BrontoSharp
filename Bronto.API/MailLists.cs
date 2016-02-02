using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class MailLists : BrontoApiClass
    {
        public MailLists(LoginSession session)
            : base(session)
        {
            
        }

        public BrontoResult Add(mailListObject mailList)
        {
            return Add(new mailListObject[] { mailList });
        }
        public BrontoResult Add(IEnumerable<mailListObject> mailLists)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.addLists(session.SessionHeader, mailLists.ToArray());
                return BrontoResult.Create(response);
            }
        }

        public async Task<BrontoResult> AddAsync(mailListObject mailList)
        {
            return await AddAsync(new mailListObject[] { mailList });
        }

        public async Task<BrontoResult> AddAsync(IEnumerable<mailListObject> mailLists)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                addListsResponse response = await client.addListsAsync(session.SessionHeader, mailLists.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }

        public BrontoResult Update(mailListObject mailList)
        {
            return Update(new mailListObject[] { mailList });
        }
        public BrontoResult Update(IEnumerable<mailListObject> mailLists)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.updateLists(session.SessionHeader, mailLists.ToArray());
                return BrontoResult.Create(response);
            }
        }

        public async Task<BrontoResult> UpdateAsync(mailListObject mailList)
        {
            return await UpdateAsync(new mailListObject[] { mailList });
        }

        public async Task<BrontoResult> UpdateAsync(IEnumerable<mailListObject> mailLists)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                updateListsResponse response = await client.updateListsAsync(session.SessionHeader, mailLists.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }


        public BrontoResult Delete(mailListObject mailList)
        {
            return Delete(new mailListObject[] { mailList });
        }
        public BrontoResult Delete(IEnumerable<mailListObject> mailLists)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.deleteLists(session.SessionHeader, mailLists.ToArray());
                return BrontoResult.Create(response);
            }
        }

        public async Task<BrontoResult> DeleteAsync(mailListObject mailList)
        {
            return await DeleteAsync(new mailListObject[] { mailList });
        }

        public async Task<BrontoResult> DeleteAsync(IEnumerable<mailListObject> mailLists)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                deleteListsResponse response = await client.deleteListsAsync(session.SessionHeader, mailLists.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }



        public List<mailListObject> Read()
        {
            return Read(new mailListFilter());
        }

        public List<mailListObject> Read(mailListFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readLists c = new readLists();
                c.filter = filter;
                c.pageNumber = 1;
                List<mailListObject> list = new List<mailListObject>();
                mailListObject[] result = client.readLists(session.SessionHeader, c);
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    result = client.readLists(session.SessionHeader, c);
                    if (result != null)
                    {
                        list.AddRange(result);
                    }
                }
                return list;
            }
        }

        public async Task<List<mailListObject>> ReadAsync()
        {
            return await ReadAsync(new mailListFilter());
        }
        public async Task<List<mailListObject>> ReadAsync(mailListFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readLists c = new readLists();
                c.filter = filter;
                c.pageNumber = 1;
                List<mailListObject> list = new List<mailListObject>();
                readListsResponse response = await client.readListsAsync(session.SessionHeader, c);
                mailListObject[] result = response.@return;
                if (result != null)
                {
                    list.AddRange(result);
                }
                while (result != null && result.Length > 0)
                {
                    c.pageNumber += 1;
                    response = await client.readListsAsync(session.SessionHeader, c);
                    result = response.@return;
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
