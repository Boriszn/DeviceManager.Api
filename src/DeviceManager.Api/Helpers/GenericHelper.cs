using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.Helpers
{
    /// <summary>
    /// Helper class for static help methods
    /// </summary>
    public static class GenericHelper
    {
        static readonly HashSet<string> _allCultureCodes;

        static GenericHelper()
        {
            _allCultureCodes = new HashSet<string>(CultureInfo.GetCultures(CultureTypes.AllCultures).Select(culture => culture.Name));
        }
        /// <summary>
        /// Gets list of culture instances based on the culture code
        /// </summary>
        /// <param name="cultures"></param>
        /// <returns></returns>
        public static List<CultureInfo> GetCultureInfos(List<string> cultures)
        {
            if (cultures?.Any() != true)
                return new List<CultureInfo>();
            var cultureList = new List<CultureInfo>();
            cultures.ForEach(culture =>
            {
                var cultureInstance = GetCulture(culture);
                if (cultureInstance != null)
                {
                    cultureList.Add(cultureInstance);
                }
            });
            return cultureList;
        }

        /// <summary>
        /// Gets the culture instance based on the culture code
        /// </summary>
        /// <param name="culture">Culture code</param>
        /// <returns>Culture instance if found else null</returns>
        public static CultureInfo GetCulture(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture))
                return null;
            try
            {
                return new CultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Checks whether culture present in generic list of cultures
        /// </summary>
        /// <param name="culture">User entered culture</param>
        /// <returns>true if culture found else false</returns>
        public static bool IsGenericCulture(string culture) => _allCultureCodes.Contains(culture);

        /// <summary>
        /// Get the value from environment variable and converts it to a uri
        /// </summary>
        /// <returns>Uri object</returns>
        public static Uri GetUriFromEnvironmentVariable(string environmentVariable)
        {
            var address = Environment.GetEnvironmentVariable(environmentVariable);

            if (string.IsNullOrWhiteSpace(address))
                throw new FluentValidation.ValidationException("Variable not found");

            if(Uri.TryCreate(address,UriKind.Absolute, out Uri uri))
            {
                return uri;
            }

            throw new FluentValidation.ValidationException("Invalid uri in the environment variable");
        }

        /// <summary>
        /// Combines path to base uri
        /// </summary>
        /// <param name="baseUri">base uri</param>
        /// <param name="path">path to be combined</param>
        /// <returns></returns>
        public static Uri CombineUri(Uri baseUri, string path)
        {
            if(Uri.TryCreate(baseUri, path, out Uri uri))
            {
                return uri;
            }
            throw new FluentValidation.ValidationException("Invalid path");
        }
    }
}
