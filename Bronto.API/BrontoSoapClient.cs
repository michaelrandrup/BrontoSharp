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
    public class BrontoSoapClient
    {
        public const string BrontoApiDefaultUrl = "https://api.bronto.com/v4";
        public static TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);

        public static BrontoSoapPortTypeClient Create(TimeSpan? Timeout = null)
        {
            //BasicHttpsBinding binding = new BasicHttpsBinding(BasicHttpsSecurityMode.Transport);
            //BrontoSoapPortTypeClient client = new BrontoSoapPortTypeClient();
            return Create(new BasicHttpsBinding(BasicHttpsSecurityMode.Transport) {MaxReceivedMessageSize = Int32.MaxValue, ReceiveTimeout = Timeout ?? DefaultTimeout, SendTimeout = Timeout ?? DefaultTimeout }, new EndpointAddress(BrontoApiDefaultUrl));
        }

        public static BrontoSoapPortTypeClient Create(Binding binding, EndpointAddress url)
        {
            return new BrontoSoapPortTypeClient(binding, url);
        }
    }
}
