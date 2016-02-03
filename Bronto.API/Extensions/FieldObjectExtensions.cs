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

        public static string NameOf(this List<fieldObject> fields, string fieldId)
        {
            fieldObject field = fields.FirstOrDefault(x => x.id.Equals(fieldId, StringComparison.OrdinalIgnoreCase));
            if (field == null)
            {
                throw new ArgumentException("fieldId", string.Format("There is no field with the Id {0}", fieldId));
            }
            return field.name;
        }

        public static string NameOf(this List<fieldObject> fields, contactField field)
        {
            return NameOf(fields, field.fieldId);
            
        }

        public static string LabelOf(this List<fieldObject> fields, string fieldId)
        {
            fieldObject field = fields.FirstOrDefault(x => x.id.Equals(fieldId, StringComparison.OrdinalIgnoreCase));
            if (field == null)
            {
                throw new ArgumentException("fieldId", string.Format("There is no field with the Id {0}", fieldId));
            }
            return field.label;
        }

        public static string LabelOf(this List<fieldObject> fields, contactField field)
        {
            return LabelOf(fields, field.fieldId);
        }

        public static string TypeOf(this List<fieldObject> fields, contactField field)
        {
            fieldObject f = fields.FirstOrDefault(x => x.id.Equals(field.fieldId, StringComparison.OrdinalIgnoreCase));
            if (f == null)
            {
                throw new ArgumentException("field", string.Format("There is no field with the Id {0}", field.fieldId));
            }
            return f.type;
        }

        public static fieldOptionObject[] OptionsOf(this List<fieldObject> fields, contactField field)
        {
            fieldObject f = fields.FirstOrDefault(x => x.id.Equals(field.fieldId, StringComparison.OrdinalIgnoreCase));
            if (f == null)
            {
                throw new ArgumentException("field", string.Format("There is no field with the Id {0}", field.fieldId));
            }
            if (!f.type.Equals(FieldTypes.RadioButtons) || !f.type.Equals(FieldTypes.SelectList))
            {
                throw new ArgumentException("field", string.Format("The field type is '{0}'. Must be either '{1}' or '{2}'", f.type,FieldTypes.RadioButtons,FieldTypes.SelectList));
            }
            return f.options;
        }

        public static fieldOptionObject[] OptionsOf(this List<fieldObject> fields, string fieldName)
        {
            fieldObject field = fields.FirstOrDefault(x => x.name.Equals(fieldName, StringComparison.OrdinalIgnoreCase) || x.label.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
            if (field == null)
            {
                throw new ArgumentException("fieldName", string.Format("There is no field with the name or label {0}", fieldName));
            }
            if (!field.type.Equals(FieldTypes.RadioButtons) || !field.type.Equals(FieldTypes.SelectList))
            {
                throw new ArgumentException("fieldId", string.Format("The field type is '{0}'. Must be either '{1}' or '{2}'", field.type, FieldTypes.RadioButtons, FieldTypes.SelectList));
            }
            return field.options;
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
