using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API.Extensions
{
    public static class FieldObjectExtensions
    {
        public static string IdOf(this List<fieldObject> fields, string fieldName)
        {
            fieldObject field = fields.FirstOrDefault(x => x.name.Equals(fieldName, StringComparison.OrdinalIgnoreCase) || x.label.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
            if (field == null)
            {
                throw new ArgumentException("fieldName", string.Format("There is no field with the name or label {0}", fieldName));
            }
            return field.id;
        }

        public static IEnumerable<string> IdOfMany(this List<fieldObject> fields, params string[] fieldNames)
        {
            foreach (string name in fieldNames)
            {
                yield return fields.IdOf(name);
            }
        }

        public static bool Exists(this List<fieldObject> fields, string fieldName)
        {
            return fields.Any(x => x.name.Equals(fieldName, StringComparison.OrdinalIgnoreCase) || x.label.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
