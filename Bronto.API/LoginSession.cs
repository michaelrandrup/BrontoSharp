using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Bronto.API.api;
using sessionHeader = Bronto.API.BrontoService.sessionHeader;

namespace Bronto.API
{
    /// <summary>
    /// Wrapper class for the bronto login session. All API Wrapper classes are initialized with this class
    /// </summary>
    public class LoginSession
    {
        /// <summary>
        ///  Returns the current login session Id
        /// </summary>
        public string SessionId { get; private set; }
        /// <summary>
        /// Creates a new Login session
        /// </summary>
        /// <param name="ApiToken">A valid API token. See dev.bronto.com for details</param>
        /// <returns>The Login session</returns>
        public static LoginSession Create(string ApiToken)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create())
            {
                LoginSession login = new LoginSession()
                {
                    SessionId = client.login(ApiToken)
                };
                return login;
            }
        }
        /// <summary>
        /// Creates a new Login session
        /// </summary>
        /// <param name="ApiToken">A valid API token. See dev.bronto.com for details</param>
        /// <returns>The Login session</returns>
        public static async Task<LoginSession> CreateAsync(string ApiToken)
        {
            using (BrontoSoapPortTypeClient client = BrontoSoapClient.Create())
            {
                LoginSession login = new LoginSession();
                loginResponse response = await client.loginAsync(ApiToken);
                login.SessionId = response.@return;
                return login;
            }
        }

        /// <summary>
        /// Returns the sessionHeader used in all API calls to the Bronto Web service
        /// </summary>
        internal sessionHeader SessionHeader
        {
            get {
                return new sessionHeader() { sessionId = this.SessionId };
            }
        }


    }
}
