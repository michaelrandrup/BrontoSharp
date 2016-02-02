using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API.Extensions
{
    public static class MailListExtensions
    {
        public static string IdOf(this List<mailListObject> lists, string listName)
        {
            mailListObject list = lists.FirstOrDefault(x => x.name.Equals(listName, StringComparison.OrdinalIgnoreCase) || x.label.Equals(listName, StringComparison.OrdinalIgnoreCase));
            if (list == null)
            {
                throw new ArgumentException("listName", string.Format("There is no mail list with the name or label {0}", listName));
            }
            return list.id;
        }

        public static IEnumerable<string> IdOfMany(this List<mailListObject> lists, params string[] listNames)
        {
            foreach (string name in listNames)
            {
                yield return lists.IdOf(name);
            }
        }

        public static bool Exists(this List<mailListObject> lists, string listName)
        {
            return lists.Any(x => x.name.Equals(listName, StringComparison.OrdinalIgnoreCase) || x.label.Equals(listName, StringComparison.OrdinalIgnoreCase));
        }

    }
}
