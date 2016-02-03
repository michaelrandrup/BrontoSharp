using Bronto.API.BrontoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bronto.API.Extensions
{
    public static class ReadContactsExtensions
    {
        public static readContacts IncludeAll(this readContacts options)
        {
            options.includeEngagementData = options.includeEngagementDataSpecified =
                options.includeGeoIPData = options.includeGeoIPDataSpecified =
                options.includeLists = options.includeListsSpecified =
                options.includeRFMData = options.includeRFMDataSpecified =
                options.includeSegments = options.includeSegmentsSpecified =
                options.includeSMSKeywords = options.includeSMSKeywordsSpecified =
                options.includeTechnologyData = options.includeTechnologyDataSpecified = true;

            return options;
        }

        public static readContacts IncludeFields(this readContacts options, List<fieldObject> fields)
        {
            options.fields = fields.Select(x => x.id).ToArray();
            return options;
        }

        public static readContacts IncludeFields(this readContacts options, IEnumerable<string> fieldIds)
        {
            options.fields = fieldIds.ToArray();
            return options;
        }



        public static readContacts IncludeLists(this readContacts options)
        {
            options.includeLists = options.includeListsSpecified = true;
            return options;
        }

        public static readContacts IncludeSegments(this readContacts options)
        {
            options.includeSegments = options.includeSegmentsSpecified = true;
            return options;
        }

        public static readContacts IncludeListsAndSegments(this readContacts options)
        {
            return options.IncludeLists().IncludeSegments();
        }

        public static readContacts IncludeEngagementData(this readContacts options)
        {
            options.includeEngagementData = options.includeEngagementDataSpecified = true;
            return options;
        }

        public static readContacts IncludeGeoIPData(this readContacts options)
        {
            options.includeGeoIPData = options.includeGeoIPDataSpecified = true;
            return options;
        }

        public static readContacts IncludeRFMData(this readContacts options)
        {
            options.includeRFMData = options.includeRFMDataSpecified = true;
            return options;
        }

        public static readContacts IncludeSMSKeywords(this readContacts options)
        {
            options.includeSMSKeywords = options.includeSMSKeywordsSpecified = true;
            return options;
        }

        public static readContacts IncludeTechnologyData(this readContacts options)
        {
            options.includeTechnologyData = options.includeTechnologyDataSpecified = true;
            return options;
        }
    }
}
