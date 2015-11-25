using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class BrontoResult
    {
        public BrontoResult()
        {

        }

        public bool HasErrors { get; set; }
        public IEnumerable<int> ErrorIndicies { get; set; }
        public List<BrontoResultItem> Items { get; set; }
        
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

    public class BrontoResultItem
    {
        public string Id { get; set; }
        public bool IsNew { get; set; }
        public bool IsError { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorString { get; set; }
    }
}
