using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public abstract class BrontoApiClass
    {
        protected LoginSession session = null;
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);

        public BrontoApiClass(LoginSession session)
        {
            this.session = session;
        }


    }
}
