using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API
{
    /// <summary>
    /// Valid Contact status codes
    /// </summary>
    public static class ContactStatus
    {
        public static readonly string Active = "active";
        public static readonly string Onboarding = "onboarding";
        public static readonly string Transactional = "transactional";
        public static readonly string Bounce = "bounce";
        public static readonly string Unconfirmed = "unconfirmed";
        public static readonly string Unsubscribed = "unsub";
    }

    /// <summary>
    /// Valid List status codes
    /// </summary>
    public static class ListStatus
    {
        public static readonly string Active = "active";
        public static readonly string Deleted = "deleted";
        public static readonly string Temporary = "tmp";
    }

    /// <summary>
    /// Valid custom field types
    /// </summary>
    public static class FieldTypes
    {
        public static readonly string Text = "text";
        public static readonly string MultilineText = "textarea";
        public static readonly string Password = "password";
        public static readonly string Checkbox = "checkbox";
        public static readonly string RadioButtons = "radio";
        public static readonly string SelectList = "select";
        public static readonly string WholeNumber = "integer";
        public static readonly string DecimalNumber = "float";
        public static readonly string Currency = "currency";
        public static readonly string Date = "date";
    }
}
