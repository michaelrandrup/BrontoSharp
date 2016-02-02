using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    public static class ContactStatus
    {
        public static readonly string Active = "active";
        public static readonly string Onboarding = "onboarding";
        public static readonly string Transactional = "transactional";
        public static readonly string Bounce = "bounce";
        public static readonly string Unconfirmed = "unconfirmed";
        public static readonly string Unsubscribed = "unsub";
    }

    public static class ListStatus
    {
        public static readonly string Active = "active";
        public static readonly string Deleted = "deleted";
        public static readonly string Temporary = "tmp";
    }
}
