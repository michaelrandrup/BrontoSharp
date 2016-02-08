using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    /// <summary>
    /// All API Classes must enherit this class
    /// </summary>
    public abstract class BrontoApiClass
    {
        /// <summary>
        /// The current login session
        /// </summary>
        protected LoginSession session = null;
        /// <summary>
        /// The default timeout for the Bronto Web Service
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Initialize the API with a valid login session
        /// </summary>
        /// <param name="session"></param>
        public BrontoApiClass(LoginSession session)
        {
            this.session = session;
        }


    }
}
