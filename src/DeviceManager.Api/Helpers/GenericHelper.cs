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
    }
}
