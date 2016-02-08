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

        #region ctor
        public Contacts(LoginSession session)
            : base(session)
        {
            this.Timeout = TimeSpan.FromMinutes(15);
        }

        #endregion

        #region CRUD operations

        /// <summary>
        /// Add a new contact to Bronto
        /// </summary>
        /// <param name="contact">The contact to add</param>
        /// <returns>The result of the add operation <seealso cref="BrontoResult"/></returns>
        public BrontoResult Add(contactObject contact)
        {
            return Add(new contactObject[] { contact });
        }
        /// <summary>
        /// Adds a list of new contacts to Bronto
        /// </summary>
        /// <param name="contacts">the list of contacts to add</param>
        /// <returns>The result of the add operation <seealso cref="BrontoResult"/></returns>
        public BrontoResult Add(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.addContacts(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response);
            }
        }

        /// <summary>
        /// Add a new contact to Bronto
        /// </summary>
        /// <param name="contact">The contact to add</param>
        /// <returns>The result of the add operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddAsync(contactObject contact)
        {
            return await AddAsync(new contactObject[] { contact });
        }

        /// <summary>
        /// Adds a list of new contacts to Bronto
        /// </summary>
        /// <param name="contacts">the list of contacts to add</param>
        /// <returns>The result of the add operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddAsync(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                addContactsResponse response = await client.addContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }

        /// <summary>
        /// Add or updates a contact in Bronto
        /// </summary>
        /// <param name="contact">The contact to add or update</param>
        /// <returns>The result of the add or update operation <seealso cref="BrontoResult"/></returns>
        public BrontoResult AddOrUpdate(contactObject contact)
        {
            return AddOrUpdate(new contactObject[] { contact });
        }

        /// <summary>
        /// Add or updates a list of contact in Bronto
        /// </summary>
        /// <param name="contact">The contacts to add or update</param>
        /// <returns>The result of the add or update operation <seealso cref="BrontoResult"/></returns>
        public BrontoResult AddOrUpdate(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.addOrUpdateContacts(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response);
            }
        }

        /// <summary>
        /// Add or updates a contact in Bronto
        /// </summary>
        /// <param name="contact">The contact to add or update</param>
        /// <returns>The result of the add or update operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddOrUpdateAsync(contactObject contact)
        {
            return await AddOrUpdateAsync(new contactObject[] { contact });
        }

        /// <summary>
        /// Add or updates a list of contact in Bronto
        /// </summary>
        /// <param name="contact">The contacts to add or update</param>
        /// <returns>The result of the add or update operation <seealso cref="BrontoResult"/></returns>
        public async Task<BrontoResult> AddOrUpdateAsync(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                addOrUpdateContactsResponse response = await client.addOrUpdateContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }


        /// <summary>
        /// Reads all bronto contacts with the minimal number of fields returned
        /// </summary>
        /// <returns>the list of contacts</returns>
        public List<contactObject> Read()
        {
            return Read(new contactFilter());
        }

        /// <summary>
        /// Reads all contacts in bronto with the specified return fields
        /// </summary>
        /// <param name="options">The fields and information to return. Use the extension methods on the readContacts class to specify options</param>
        /// <returns>the list of contacts</returns>
        public List<contactObject> Read(readContacts options)
        {
            return Read(new contactFilter(),options);
        }



        /// <summary>
        /// Reads contacts from bronto using a filter and the specified return fields and information
        /// </summary>
        /// <param name="filter">The filter to use when reading</param>
        /// <param name="options">The fields and information to return. Use the extension methods on the readContacts class to specify options</param>
        /// <returns>the list of contacts</returns>
        public List<contactObject> Read(contactFilter filter, readContacts options = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readContacts c = options ?? new readContacts();
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

        /// <summary>
        /// Reads all bronto contacts with the minimal number of fields returned
        /// </summary>
        /// <returns>the list of contacts</returns>
        public async Task<List<contactObject>> ReadAsync()
        {
            return await ReadAsync(new contactFilter());
        }

        /// <summary>
        /// Reads all contacts in bronto with the specified return fields
        /// </summary>
        /// <param name="options">The fields and information to return. Use the extension methods on the readContacts class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<contactObject>> ReadAsync(readContacts options)
        {
            return await ReadAsync(new contactFilter(),options);
        }


        /// <summary>
        /// Reads contacts from bronto using a filter and the specified return fields and information
        /// </summary>
        /// <param name="filter">The filter to use when reading</param>
        /// <param name="options">The fields and information to return. Use the extension methods on the readContacts class to specify options</param>
        /// <returns>the list of contacts</returns>
        public async Task<List<contactObject>> ReadAsync(contactFilter filter, readContacts options = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter", "The filter must be specified. Alternatively call the Read() function");
            }
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                readContacts c = options ?? new readContacts();
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

        /// <summary>
        /// Deletes a list of contacts in bronto
        /// </summary>
        /// <param name="contacts">the contacts to delete</param>
        /// <returns>The result of the Delete operation</returns>
        public BrontoResult Delete(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                writeResult response = client.deleteContacts(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response);
            }
        }

        /// <summary>
        /// Deletes a list of contacts in bronto
        /// </summary>
        /// <param name="contacts">the contacts to delete</param>
        /// <returns>The result of the Delete operation</returns>
        public async Task<BrontoResult> DeleteAsync(IEnumerable<contactObject> contacts)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create(Timeout))
            {
                deleteContactsResponse response = await client.deleteContactsAsync(session.SessionHeader, contacts.ToArray());
                return BrontoResult.Create(response.@return);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the list of custom contact fields
        /// </summary>
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

        #endregion

        #region static properties and methods

        /// <summary>
        /// Returns a new readContacts object to be used in the Read method <see cref="Read(contactFilter, readContacts)"/>
        /// </summary>
        public static readContacts ReadOptions
        {
            get
            {
                return new readContacts();
            }
        }

        #endregion

    }
}
