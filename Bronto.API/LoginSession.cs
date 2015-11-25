using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public class LoginSession
    {
        public string SessionId { get; private set; }
        public static LoginSession Create(string ApiToken)
        {
            using (BrontoSoapPortTypeClient client = new BrontoSoapPortTypeClient())
            {
                LoginSession login = new LoginSession();
                login.SessionId = client.login(ApiToken);
                return login;
            }
        }

        public static async Task<LoginSession> CreateAsync(string ApiToken)
        {
            using (BrontoSoapPortTypeClient client = new BrontoSoapPortTypeClient())
            {
                LoginSession login = new LoginSession();
                loginResponse response = await client.loginAsync(ApiToken);
                login.SessionId = response.@return;
                return login;
            }
        }

        internal sessionHeader SessionHeader
        {
            get {
                return new sessionHeader() { sessionId = this.SessionId };
            }
        }


    }
}
