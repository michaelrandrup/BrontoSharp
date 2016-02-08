using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    /// <summary>
    /// Binder class for the Bronto Web Service client
    /// </summary>
    public class BrontoSoapClient
    {
        /// <summary>
        /// The Bronto API Url
        /// </summary>
        public const string BrontoApiDefaultUrl = "https://api.bronto.com/v4";
        /// <summary>
        /// The default timeout setting for the Web Service
        /// </summary>
        public static TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);
        /// <summary>
        /// The max capacity that Bronto will return in one read session
        /// </summary>
        public static int MaxWriteCapacity = 5000;
        /// <summary>
        /// Returns an instance of the Bronto SOAP client with the specified timeout
        /// </summary>
        /// <param name="Timeout">The timeout setting for the Bronto Web Service client. If not specified, the DefaultTimeout field is used <see cref="DefaultTimeout"/></param>
        /// <returns>A Bronto Web Service client</returns>
        public static BrontoSoapPortTypeClient Create(TimeSpan? Timeout = null)
        {
            return Create(new BasicHttpsBinding(BasicHttpsSecurityMode.Transport) {MaxReceivedMessageSize = Int32.MaxValue, ReceiveTimeout = Timeout ?? DefaultTimeout, SendTimeout = Timeout ?? DefaultTimeout }, new EndpointAddress(BrontoApiDefaultUrl));
        }

        /// <summary>
        /// Returns an instance of the Bronto SOAP client with the specified binding and endpoint address
        /// </summary>
        /// <param name="binding">A valid binding for the web service. Normally the overload of this method should be used <see cref="Create(TimeSpan?)"/> </param>
        /// <param name="url">A valid Url for the Bronto web service. Normally the overload of this method should be used <see cref="Create(TimeSpan?)"/> </param>
        /// <returns>A Bronto Web Service client</returns>
        public static BrontoSoapPortTypeClient Create(Binding binding, EndpointAddress url)
        {
            return new BrontoSoapPortTypeClient(binding, url);
        }
    }
}
