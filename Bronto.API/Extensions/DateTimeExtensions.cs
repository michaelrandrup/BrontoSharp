using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToBrontoString(this DateTime date)
        {
            return date.Kind == DateTimeKind.Utc ? date.ToLocalTime().ToString("o") : date.ToString("o");
        }
    }
}
