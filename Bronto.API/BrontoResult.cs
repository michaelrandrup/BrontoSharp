using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    /// <summary>
    /// Wrapper class for the Bronco writeResult Class
    /// </summary>
    public class BrontoResult
    {
        public BrontoResult()
        {

        }

        /// <summary>
        /// True if the result contacts errors
        /// </summary>
        public bool HasErrors { get; set; }
        /// <summary>
        /// A list of indexes in the Item list that contain errors
        /// </summary>
        public IEnumerable<int> ErrorIndicies { get; set; }
        /// <summary>
        /// The result items from the writeResult
        /// </summary>
        public List<BrontoResultItem> Items { get; set; }
        
        /// <summary>
        /// Creates a BrontoResult class from a writeresult
        /// </summary>
        /// <param name="result">the writeResult from a web service call</param>
        /// <returns>A BrontoResult class</returns>
        internal static BrontoResult Create(writeResult result)
        {
            BrontoResult br = new BrontoResult();
            if (result.errors != null && result.errors.Length > 0)
            {
                br.ErrorIndicies = result.errors.Cast<int>();
                br.HasErrors = true;
            }
            br.Items = new List<BrontoResultItem>();
            foreach (resultItem itm in result.results)
            {
                br.Items.Add(new BrontoResultItem()
                {
                     Id = itm.id,
                     IsNew = itm.isNew,
                     IsError = itm.isError,
                      ErrorCode = itm.errorCode,
                      ErrorString = itm.errorString
                });
            }
            return br;
        }
    }

    /// <summary>
    /// Wrapper class for the Bronco resultItem class
    /// </summary>
    public class BrontoResultItem
    {
        /// <summary>
        /// The Id of the item that was created, updated or deleted
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// True if the item was created
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// True if the item resulted in an error
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// Contacts the error code for the item
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Contacts the error description for the item
        /// </summary>
        public string ErrorString { get; set; }
    }
}
