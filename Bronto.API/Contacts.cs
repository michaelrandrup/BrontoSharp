using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class Contacts
    {
        private LoginSession session = null;
        public Contacts(LoginSession session)
        {
            this.session = session;
        }

        public BrontoResult Add(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = new BrontoSoapPortTypeClient())
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
            using (BrontoSoapPortTypeClient client = new BrontoSoapPortTypeClient())
            {
                addContactsResponse response = await client.addContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }

        
        
        
        

    }
}
